import { BaseResponse, LoginRequest, LoginResponse } from "@/common.types";
import useAuth from "./useAuth";

const useIdentity = () => {
  const { postAsync } = useAuth();

  const loginAsync = async (
    request: LoginRequest
  ): Promise<BaseResponse<LoginResponse>> => {
    return await postAsync("api/identity/login", request);
  };

  return { loginAsync };
};

export default useIdentity;
