import { ErrorResponse } from "./types";
import { useQuery } from "@tanstack/react-query";
import { toast } from "react-toastify";
import { useAuthenticatedAxios } from "./authenticatedAxios";
import { Course } from "./useCourses";

interface CourseWithCompletion extends Course {
  completed: boolean;
}

export const useCompletedCourses = () => {
  const Axios = useAuthenticatedAxios();

  return useQuery<void, ErrorResponse, CourseWithCompletion[]>(
    ["/Courses/completed"],
    async () => {
      const { data } = await Axios({
        url: "/Courses/completed",
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
