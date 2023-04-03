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
  token: string;
}

export const useLogin = () => {
  // TODO add auth context, logic here for setting token on successful login

  const { login, user } = useAuth();
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
        login(data.token);
        toast.success("Logged in.");
      },
      onError: ({ response }) => {
        toast.error(response?.data?.message ?? "Unknown Error.");
      },
    }
  );
};
