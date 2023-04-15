import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useAuthenticatedAxios } from "./authenticatedAxios";
import { ErrorResponse } from "./types";
import { toast } from "react-toastify";

export interface RemoveCoursePayload {
  userId: number;
  courseId: string;
}

export const useRemoveCourse = () => {
  const Axios = useAuthenticatedAxios();
  const queryClient = useQueryClient();
  return useMutation<unknown, ErrorResponse, RemoveCoursePayload>(
    ["/Courses/remove"],
    async (payload) => {
      const { data } = await Axios({
        url: "/Courses/remove",
        method: "post",
        data: payload,
      });
      return data;
    },
    {
      onSuccess: (data) => {
        toast.success("Course removed.");
        queryClient.refetchQueries(["/Courses/completed"]);
      },
      onError: ({ response }) => {
        toast.error(response?.data?.message ?? "Unknown Error.");
      },
    }
  );
};
