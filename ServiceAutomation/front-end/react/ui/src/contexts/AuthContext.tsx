"use client";

import { Authentication } from "@/common.types";
import { createContext, useState } from "react";

export const AuthContext = createContext<Authentication | undefined>(undefined);

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [token, setToken] = useState<string | undefined>();

  const loginAsync = async () => {};

  const logoutAsync = async () => {};

  const model: Authentication = {
    isAuthenticated: true,
    token: token,
    loginAsync: loginAsync,
    logoutAsync: logoutAsync,
  };
  console.log(model);

  return (
    <>
      <AuthContext.Provider value={model}>{children}</AuthContext.Provider>
    </>
  );
};
