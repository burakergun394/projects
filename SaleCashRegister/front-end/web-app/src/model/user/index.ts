import { Language } from "@/localization/types";
import { BaseModel, CreateBaseDto, UpdateBaseDto, DeleteBaseDto, DeleteManyBaseDto } from "../base";

export interface User extends BaseModel {
  name: string;
  surname: string;
  username: string;
  email: string;
  password: string;
  language: Language;
  isDeleted?: boolean;
}

export type CreateUser = CreateBaseDto<User>;

export type UpdateUser = UpdateBaseDto<User>;

export type DeleteUser = DeleteBaseDto;

export type DeleteManyUsers = DeleteManyBaseDto;

export type UserResponse = Omit<User, "password">;
