import { BaseModel, CreateBaseDto, UpdateBaseDto, DeleteBaseDto, DeleteManyBaseDto } from "../base";

export enum Permission {
  CREATE_USER = "CREATE_USER",
  READ_USER = "READ_USER",
  UPDATE_USER = "UPDATE_USER",
  DELETE_USER = "DELETE_USER",
  
  CREATE_ROLE = "CREATE_ROLE",
  READ_ROLE = "READ_ROLE",
  UPDATE_ROLE = "UPDATE_ROLE",
  DELETE_ROLE = "DELETE_ROLE",
}

export interface Role extends BaseModel {
  name: string;
  description: string;
  permissions: Permission[];
  isDeleted?: boolean;
}

export type CreateRole = CreateBaseDto<Role>;

export type UpdateRole = UpdateBaseDto<Role>;

export type DeleteRole = DeleteBaseDto;

export type DeleteManyRoles = DeleteManyBaseDto;

export type RoleResponse = Role; 