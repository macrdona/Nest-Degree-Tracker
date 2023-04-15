import React, { useContext, useEffect, useState } from "react";
import { toast } from "react-toastify";
import { useNavigate } from "react-router";
import { Course } from "../../../lib/api/useCourses";

export interface TrackerContextValue {
  selectedCourse?: Course;
  setSelectedCourse: (course: Course | undefined) => void;
}

export const TrackerContext = React.createContext<TrackerContextValue>({
  setSelectedCourse: () => {},
});

export const TrackerContextProvider = ({
  children,
}: React.PropsWithChildren) => {
  const [selectedCourse, setSelectedCourse] = useState<Course>();

  useEffect(() => console.log(selectedCourse), [selectedCourse]);
  return (
    <TrackerContext.Provider
      value={{
        selectedCourse,
        setSelectedCourse,
      }}
    >
      {children}
    </TrackerContext.Provider>
  );
};
