import React, { useContext, useState } from "react";
import { Course } from "../../lib/api/useCourses";
import { toast } from "react-toastify";

export interface RegisteringFormsContextValue {
  currentStep: number;
  nextStep: () => void;
  major?: string;
  setMajor: (major: string) => void;
  minor?: string;
  setMinor: (minor: string) => void;
  courses?: Course[];
  setCourses: (courses: Course[]) => void;
  submit: () => Promise<boolean>;
}

export const RegisteringFormsContext =
  React.createContext<RegisteringFormsContextValue>({
    currentStep: 0,
    nextStep: () => {},
    setCourses: () => {},
    setMajor: () => {},
    setMinor: () => {},
    submit: async () => false,
  });

export const RegisteringFormsContextProvider = ({
  children,
}: React.PropsWithChildren) => {
  const [step, setStep] = useState(0);
  const [major, setMajor] = useState<string>();
  const [minor, setMinor] = useState<string>();
  const [courses, setCourses] = useState<Course[]>([]);

  const submitOnboardingForm = async () => {
    toast.info("Not implemented yet");
    return true;
  };

  return (
    <RegisteringFormsContext.Provider
      value={{
        currentStep: step,
        nextStep: () => {
          setStep((s) => s + 1);
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
