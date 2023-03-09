import { ErrorResponse } from "./types";
import { useMutation } from "@tanstack/react-query";
import Axios from "axios";
import { toast } from "react-toastify";
import { useAuth } from "../auth/AuthContext";

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

    const { login } = useAuth();
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
        login(data.token);
      },
      onError: ({ response}) => {
        toast.error(response?.data?.message ?? "Unknown Error.");
      },
    }
  );
};
