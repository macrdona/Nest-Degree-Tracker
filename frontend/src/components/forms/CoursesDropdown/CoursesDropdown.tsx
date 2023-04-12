import { Typeahead, TypeaheadInputMulti } from "react-bootstrap-typeahead";
import { TypeaheadProps } from "react-bootstrap-typeahead/types/types";
import { useCourses } from "../../../lib/api/useCourses";
import { useMemo } from "react";

type CoursesDropdownProps = Partial<TypeaheadProps>;

function CoursesDropdown({ multiple, ...props }: CoursesDropdownProps) {
  const { data } = useCourses();
  const options = useMemo(() => {
    return (
      data?.map((course) => {
        return {
          label: `${course.courseId}: ${course.courseName}`,
        };
      }) ?? []
    );
  }, [data]);

  return <Typeahead {...props} multiple options={options} labelKey={"label"} />;
}

export default CoursesDropdown;
