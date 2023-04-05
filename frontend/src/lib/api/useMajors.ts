import { ErrorResponse } from "./types";
import { useQuery } from "@tanstack/react-query";
import { toast } from "react-toastify";
import { useAuthenticatedAxios } from "./authenticatedAxios";

export interface Major {
  name: string;
}

export const useMajors = () => {
  const Axios = useAuthenticatedAxios();

  return useQuery<void, ErrorResponse, Major[]>(
    ["/Majors"],
    async () => {
      const { data } = await Axios({
        url: "/Majors",
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
