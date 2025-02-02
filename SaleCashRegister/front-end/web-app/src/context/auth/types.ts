export interface AuthState {
  isAuth: boolean;
  username?: string;
  tenant?: string;
  token?: string;
}

export interface AuthContextType extends AuthState {
  login: (username: string, tenant: string) => Promise<void>;
  logout: () => void;
}
