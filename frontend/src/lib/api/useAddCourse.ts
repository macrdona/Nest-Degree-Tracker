import { ErrorResponse } from "./types";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";
import { useAuthenticatedAxios } from "./authenticatedAxios";

export interface AddCoursePayload {
  userId: number;
  courseId: string;
}

export const useAddCourse = () => {
  const Axios = useAuthenticatedAxios();
  const queryClient = useQueryClient();
  return useMutation<unknown, ErrorResponse, AddCoursePayload>(
    ["/Courses/add"],
    async (payload) => {
      const { data } = await Axios({
        url: "/Courses/add",
        method: "post",
        data: payload,
      });
      return data;
    },
    {
      onSuccess: (data) => {
        toast.success("Course added.");
        queryClient.refetchQueries(["/Courses"]);
      },
      onError: ({ response }) => {
        toast.error(response?.data?.message ?? "Unknown Error.");
      },
    }
  );
};
