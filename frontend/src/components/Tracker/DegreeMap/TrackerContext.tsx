import React, { useEffect, useState } from "react";
import { Course } from "../../../lib/api/useCourses";
import { CreditRequirement } from "../../../lib/api/useRequirements";

export interface TrackerContextValue {
  selectedCourse?: Course;
  selectedRequirement?: CreditRequirement;
  setSelectedCourse: (course: Course | undefined) => void;
  setSelectedRequirement: (req: CreditRequirement | undefined) => void;
}

export const TrackerContext = React.createContext<TrackerContextValue>({
  setSelectedCourse: () => {},
  setSelectedRequirement: () => {},
});

export const TrackerContextProvider = ({
  children,
}: React.PropsWithChildren) => {
  const [selectedCourse, setSelectedCourse] = useState<Course>();
  const [selectedRequirement, setSelectedRequirement] =
    useState<CreditRequirement>();

  useEffect(() => console.log(selectedCourse), [selectedCourse]);
  return (
    <TrackerContext.Provider
      value={{
        selectedCourse,
        selectedRequirement,
        setSelectedCourse,
        setSelectedRequirement,
      }}
    >
      {children}
    </TrackerContext.Provider>
  );
};
