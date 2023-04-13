import React, { useContext, useState } from "react";
import { Course } from "../../lib/api/useCourses";
import { toast } from "react-toastify";
import { Major, Minor } from "../../lib/api/types";
import { useEnrollmentForm } from "../../lib/api/useEnrollmentForm";
import { useAuth } from "../../lib/auth/AuthContext";
import { useNavigate } from "react-router";

export interface RegisteringFormsContextValue {
  currentStep: number;
  nextStep: () => void;
  prevStep: () => void;
  major?: Major;
  setMajor: (major: Major) => void;
  minor?: { name: string };
  setMinor: (minor: Minor) => void;
  courses?: Course[];
  setCourses: (courses: Course[]) => void;
  submit: () => Promise<boolean>;
}

export const RegisteringFormsContext =
  React.createContext<RegisteringFormsContextValue>({
    currentStep: 0,
    nextStep: () => {},
    prevStep: () => {},
    setCourses: () => {},
    setMajor: () => {},
    setMinor: () => {},
    submit: async () => false,
  });

export const RegisteringFormsContextProvider = ({
  children,
}: React.PropsWithChildren) => {
  const [step, setStep] = useState(0);
  const [major, setMajor] = useState<Major>();
  const [minor, setMinor] = useState<Minor>();
  const [courses, setCourses] = useState<Course[]>([]);

  const { user } = useAuth();
  const { mutate } = useEnrollmentForm();
  const navigate = useNavigate();

  const submitOnboardingForm = async () => {
    if (user && major && minor)
      mutate({
        courses: courses.map((course) => course.courseId),
        userId: user.id ?? 0,
        major: major.majorName,
        minor: minor.name,
      });

      navigate("/tracker");
    return true;
  };

  console.log(step, major, minor, courses);

  return (
    <RegisteringFormsContext.Provider
      value={{
        currentStep: step,
        nextStep: () => {
          setStep((s) => s + 1);
        },
        prevStep: () => {
          setStep((s) => s - 1);
        },
        major: major,
        courses: courses,
        minor: minor,
        setCourses: setCourses,
        setMajor: setMajor,
        setMinor: setMinor,
        submit: submitOnboardingForm,
      }}
    >
      {children}
    </RegisteringFormsContext.Provider>
  );
};
