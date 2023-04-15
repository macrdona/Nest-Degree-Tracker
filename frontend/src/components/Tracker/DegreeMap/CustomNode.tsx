import { Handle, Position } from "reactflow";
import "./CustomNode.scss";
import { Course } from "../../../lib/api/useCourses";

function CustomNode({ data: course }: { data: Course }) {
  return (
    <>
      <Handle type="target" position={Position.Top} />
      <div className="custom-node-body card rounded-4">
        <p className="fw-bold fs-5">{course.courseId}</p>
        <p className="">{course.courseName}</p>
        <p className="">({course.credits} credits)</p>
      </div>
      <Handle type="source" position={Position.Bottom} />
    </>
  );
}

export default CustomNode;
