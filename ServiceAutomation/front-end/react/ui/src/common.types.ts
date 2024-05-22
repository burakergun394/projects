export interface LoginRequest {

  tenantCode: string;
  userName: string;
  password: string;
}

export interface BaseResponse<T> {

  isSuccess: boolean;
  data: T | undefined;
  message: string;
}

export interface LoginResponse {
  authToken: string;
  accessTokenExpireDate: string;
}

export interface Authentication {
  token: string | undefined;
  isAuthenticated: boolean;
  loginAsync: () => Promise<void>;
  logoutAsync: () => Promise<void>;
}