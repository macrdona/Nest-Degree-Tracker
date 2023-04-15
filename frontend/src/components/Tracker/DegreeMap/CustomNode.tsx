import { Handle, Position } from "reactflow";
import "./CustomNode.scss";
import { Course } from "../../../lib/api/useCourses";
import { useCompletedCourses } from "../../../lib/api/useCompletedCourses";
import { useMemo } from "react";

function CustomNode({ data: course }: { data: Course }) {
  const { data: completedCourses } = useCompletedCourses();
  const isCompleted = useMemo(() => {
    return (
      completedCourses?.find((c) => c.courseId === course.courseId)
        ?.completed ?? false
    );
  }, [completedCourses]);

  const canBeTaken = useMemo(() => {
    if (!course) return false;
    if (isCompleted) return false;
    if (!completedCourses) return false;
    if (course.prerequisites == "N/A") return true;

    const prereqs = course.prerequisites.split(",");

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
          isCompleted ? "completed" : ""
        } ${canBeTaken ? "canTake" : ""}`}
      >
        <p className="fw-bold fs-5">{course.courseId}</p>
        <p className="">{course.courseName}</p>
        <p className="">({course.credits} credits)</p>
        {isCompleted ? <em>Completed</em> : null}
      </div>
      <Handle type="source" position={Position.Bottom} />
    </>
  );
}

export default CustomNode;
