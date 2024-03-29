import { ErrorResponse, User } from "./types";
import { useMutation } from "@tanstack/react-query";
import Axios from "axios";
import { toast } from "react-toastify";

export interface RegisterPayload {
  firstName: string;
  lastName: string;
  username: string;
  password: string;
}

export interface RegisterResponse {
  userId: number;
  username: string;
  firstName: string;
  lastName: string;
  token: string;
}

export const useRegister = () => {
  // The useMutation hook takes 3 type parameters:
  // 1. The response type (this route doesn't respond with any data on success, so it's void)
  // 2. The error type
  // 3. The payload type (if we are sending data)
  return useMutation<RegisterResponse, ErrorResponse, RegisterPayload>(
    ["/Users/register"],
    async (payload) => {
      const { data } = await Axios({
        url: "/Users/register",
        method: "post",
        data: payload,
      });
      return data;
    },
    {
      onSuccess: (data) => {
        toast.success("Registered successfully!");
      },
      onError: ({ response }) => {
        toast.error(response?.data?.message ?? "Unknown Error.");
      },
    }
  );
};
