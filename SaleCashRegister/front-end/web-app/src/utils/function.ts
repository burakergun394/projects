import { BaseResponse } from "@/model/base";

const handleApiResponse = <T>(response: Response): Promise<BaseResponse<T>> => {
  if (!response.ok) {
    throw new Error(`Failed to fetch: ${response.statusText}`);
  }
  return response.json();
};

export const baseFunction = async <T>(
  action: () => Promise<BaseResponse<T>>
): Promise<BaseResponse<T>> => {
  try {
    const response = await action();
    return response;
  } catch (error) {
    return {
      isSuccess: false,
      message:
        error instanceof Error ? error.message : "An unknown error occurred",
      data: null as T,
    };
  }
};

export const baseFetchFunction = async <T>(
  url: string,
  options: RequestInit = {}
): Promise<BaseResponse<T>> => {
  try {
    const response = await fetch(url, options);
    const result = await handleApiResponse<T>(response);
    return {
      isSuccess: true,
      message: result.message || "Operation successful",
      data: result.data,
    };
  } catch (error) {
    return {
      isSuccess: false,
      message:
        error instanceof Error ? error.message : "An unknown error occurred",
      data: null as T,
    };
  }
};
