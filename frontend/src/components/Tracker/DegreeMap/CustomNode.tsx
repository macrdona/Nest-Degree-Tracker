import { Handle, Position } from "reactflow";
import "./CustomNode.scss";
import { Course, useCourses } from "../../../lib/api/useCourses";
import { useContext, useMemo } from "react";
import { TrackerContext } from "./TrackerContext";

function CustomNode({ data: course }: { data: Course }) {
  const { setSelectedCourse } = useContext(TrackerContext);

  const { data: courses } = useCourses();

  const completedCourses = useMemo(() => {
    return courses?.filter((c) => c.completed) ?? [];
  }, [courses]);

  const canBeTaken = useMemo(() => {
    if (!course) return false;
    if (course.completed) return false;
    if (!completedCourses) return false;
    if (!course.prerequisites?.length) return true;

    const prereqs = course.prerequisites;

    if (
      prereqs.every(
        (req) =>
          req.includes("MAC1") || // Exclude 1000 level math classes from requirements
          completedCourses.some((c) => c.courseId == req)
      )
    )
      return true;

    return false;
  }, [course, completedCourses]);

  return (
    <>
      <Handle type="target" position={Position.Top} />
      <div
        className={`custom-node-body card rounded-4 ${
          course.completed ? "completed" : ""
        } ${canBeTaken ? "canTake" : ""}`}
        onClick={() => {
          console.log("hello");
          setSelectedCourse(course);
        }}
      >
        <p className="fw-bold fs-5">{course.courseId}</p>
        <p className="">{course.courseName}</p>
        <p className="">({course.credits} credits)</p>
        {course.completed ? <em>Completed</em> : null}
      </div>
      <Handle type="source" position={Position.Bottom} />
    </>
  );
}

export default CustomNode;
