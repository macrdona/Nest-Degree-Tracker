import { Typeahead, TypeaheadInputMulti } from "react-bootstrap-typeahead";
import { TypeaheadProps } from "react-bootstrap-typeahead/types/types";
import { Course, useCourses } from "../../../lib/api/useCourses";
import { useMemo } from "react";

type CoursesDropdownProps = Partial<TypeaheadProps>;

function CoursesDropdown({ multiple, ...props }: CoursesDropdownProps) {
  const { data } = useCourses();
  const options = useMemo(() => {
    return data ?? [];
  }, [data]);

  return (
    <Typeahead
      {...props}
      multiple
      options={options}
      labelKey={(option) =>
        `${(option as Course).courseId}: ${(option as Course).courseName}`
      }
    />
  );
}

export default CoursesDropdown;
