import { ErrorResponse } from "./types";
import { useQuery } from "@tanstack/react-query";
import { toast } from "react-toastify";
import { useAuthenticatedAxios } from "./authenticatedAxios";

export interface Course {
  availability: string;
  coRequisites: string[] | null;
  courseId: string;
  courseName: string;
  credits: number;
  description: string;
  prerequisites: string[] | null;
  completed: boolean;
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
