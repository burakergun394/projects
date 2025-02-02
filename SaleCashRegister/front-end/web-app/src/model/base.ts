export interface BaseModel {
  id: string;
  createdAt: Date;
  updatedAt: Date;
}

export type CreateBaseDto<T> = Omit<T, keyof BaseModel>;

export type UpdateBaseDto<T> = Partial<CreateBaseDto<T>>;

export type DeleteBaseDto = {
  id: string;
  softDelete?: boolean;
};

export type DeleteManyBaseDto = {
  ids: string[];
  softDelete?: boolean;
}; 

export type BaseResponse<T> = {
  isSuccess: boolean;
  message: string;
  data: T;
};