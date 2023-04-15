import { ErrorResponse } from "./types";
import { useQuery } from "@tanstack/react-query";
import { toast } from "react-toastify";
import { useAuthenticatedAxios } from "./authenticatedAxios";

export interface Course {
  availability: string;
  coRequisites: unknown;
  courseId: string;
  courseName: string;
  credits: number;
  description: string;
  prerequisites: string;
}

export const useCourses = () => {
  const Axios = useAuthenticatedAxios();

  return useQuery<void, ErrorResponse, Course[]>(
    ["/Courses"],
    async () => {
      const { data } = await Axios({
        url: "/Courses",
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
