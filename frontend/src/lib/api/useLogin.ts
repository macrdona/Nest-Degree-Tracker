import { ErrorResponse } from "./types";
import { useMutation } from "@tanstack/react-query";
import Axios from "axios";
import { toast } from "react-toastify";

export interface LoginPayload {
  username: string;
  password: string;
}

export interface LoginResponse {
    id: number,
    firstName: string,
    lastName: string,
    username: string,
    token: string
}

export const useLogin = () => {

    // TODO add auth context, logic here for setting token on successful login

  return useMutation<LoginResponse, ErrorResponse, LoginPayload>(
    ["/Users/login"],
    async (payload) => {
      const { data } = await Axios({
        url: "/Users/login",
        method: "post",
        data: payload,
      });
      return data;
    },
    {
      onSuccess: (data) => {
        toast.success(`Welcome, ${data.firstName}!`);
      },
      onError: ({ response}) => {
        toast.error(response?.data?.message ?? "Unknown Error.");
      },
    }
  );
};
