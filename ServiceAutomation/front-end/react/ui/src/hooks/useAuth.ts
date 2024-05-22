import { BaseResponse } from "@/common.types";
import axios from "axios";

const useAuth = () => {
  const url = process.env.BASE_URL;

  const postAsync = async <T>(
    path: string,
    request: any
  ): Promise<BaseResponse<T>> => {
    try {
      const axiosResponse = await axios.post<BaseResponse<T>>(
        `${url}${path}`,
        request
      );
      return axiosResponse.data;
    } catch (error: any) {
      const response: BaseResponse<T> = {
        isSuccess: false,
        data: undefined,
        message: error.message,
      };
      return response;
    }
  };

  return { postAsync };
};

export default useAuth;
