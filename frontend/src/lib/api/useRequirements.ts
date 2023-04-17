import { ErrorResponse } from "./types";
import { useQuery } from "@tanstack/react-query";
import { toast } from "react-toastify";
import { useAuthenticatedAxios } from "./authenticatedAxios";

export interface CreditRequirement {
  name: string;
  completedCredits: number;
  totalCredits: number;
  satisfied: false;
}

export const useRequirements = () => {
  const Axios = useAuthenticatedAxios();

  return useQuery<void, ErrorResponse, CreditRequirement[]>(
    ["/Majors/check-requirements"],
    async () => {
      const { data } = await Axios({
        url: "/Majors/check-requirements",
      });
      return data;
    },
    {
      onError: ({ response }) => {
        toast.error(response?.data?.message ?? "Unknown Error.");
      },
    }
  );
};
