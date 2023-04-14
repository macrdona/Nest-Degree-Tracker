import { AxiosError } from "axios";

export type ErrorResponse = AxiosError<{
  statusCode?: number;
  errors?: ErrorDescription[];
  message?: string;
}>;

export interface ErrorDescription {
  field?: string;
  message?: string;
}

export interface UserToken {
  id: number;
  username: string;
  firstName: string;
  lastName: string;
  completed: string;
}
export interface User {
  id: number;
  username: string;
  firstName: string;
  lastName: string;
  completed: boolean;
}

export interface Major {
  majorId: 1;
  majorName: string;
  degree: string;
  description: string;
}
export interface Minor {
  name: string;
}
