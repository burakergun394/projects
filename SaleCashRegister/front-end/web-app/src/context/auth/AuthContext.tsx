"use client";

import {
  createContext,
  useContext,
  useState,
  useCallback,
  useMemo,
  useEffect,
} from "react";
import { useRouter, usePathname } from "next/navigation";
import { AuthContextType, AuthState } from "./types";
import { createDummyJwt, isTokenExpired } from "@/utils/jwt";

const initialState: AuthState = {
  isAuth: false,
  username: undefined,
  tenant: undefined,
  token: undefined,
};

const AUTH_STORAGE_KEY = "auth";

const AuthContext = createContext<AuthContextType>({
  ...initialState,
  login: async () => {},
  logout: () => {},
});

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [auth, setAuth] = useState<AuthState>(() => {
    if (typeof window === "undefined") return initialState;

    const storedAuth = localStorage.getItem(AUTH_STORAGE_KEY);
    if (!storedAuth) return initialState;

    const parsedAuth = JSON.parse(storedAuth);
    if (!parsedAuth.token || isTokenExpired(parsedAuth.token)) {
      localStorage.removeItem(AUTH_STORAGE_KEY);
      return initialState;
    }

    return parsedAuth;
  });

  const router = useRouter();
  const pathname = usePathname();

  const login = useCallback(async (username: string, tenant: string) => {
    const token = createDummyJwt(username, tenant);
    const newAuth = { isAuth: true, username, tenant, token };
    setAuth(newAuth);
    localStorage.setItem(AUTH_STORAGE_KEY, JSON.stringify(newAuth));
  }, []);

  const logout = useCallback(() => {
    setAuth(initialState);
    localStorage.removeItem(AUTH_STORAGE_KEY);
    router.push("/login");
  }, [router]);

  useEffect(() => {
    if (auth.token && isTokenExpired(auth.token)) {
      logout();
    }
  }, [auth.token, logout]);

  useEffect(() => {
    if (!auth.isAuth && pathname !== "/login") {
      router.push("/login");
    }
  }, [auth.isAuth, pathname, router]);

  const value = useMemo(
    () => ({ ...auth, login, logout }),
    [auth, login, logout]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export const useAuth = () => useContext(AuthContext);
