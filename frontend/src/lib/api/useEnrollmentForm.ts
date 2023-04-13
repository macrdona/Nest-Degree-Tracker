import { ErrorResponse } from "./types";
import { useMutation } from "@tanstack/react-query";
import { toast } from "react-toastify";
import { useAuthenticatedAxios } from "./authenticatedAxios";

export interface EnrollmentFormPayload {
  userId: number;
  major: string;
  minor: string;
  courses: string[]; // Array of Course IDs, e.g. COP3404
}

export interface EnrollmentFormResponse {
  message: string;
}

export const useEnrollmentForm = () => {
  const Axios = useAuthenticatedAxios();

  return useMutation<
    EnrollmentFormResponse,
    ErrorResponse,
    EnrollmentFormPayload
  >(
    ["/Users/register"],
    async (payload) => {
      const { data } = await Axios({
        url: "/Users/enrollment-form",
        method: "post",
        data: payload,
      });
      return data;
    },
    {
      onSuccess: (data) => {
        toast.success("Onboarding completed!");
      },
      onError: ({ response }) => {
        toast.error(response?.data?.message ?? "Unknown Error.");
      },
    }
  );
};
