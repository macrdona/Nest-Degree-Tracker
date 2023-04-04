import "./CoursesDropdown.scss";

interface CoursesDropdownProps {
  label: string;
}

const Courses = [
  {
    id: "CDA3100",
    name: "Computer Architecture",
  },
];

function CoursesDropdown(props: CoursesDropdownProps) {
  return <div>This is the CoursesDropdown component!</div>;
}

export default CoursesDropdown;
