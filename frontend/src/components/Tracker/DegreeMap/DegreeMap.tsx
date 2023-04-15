import "./DegreeMap.scss";

import ReactFlow, { Node, Background, Edge, MarkerType } from "reactflow";
import "reactflow/dist/style.css";
import CustomNode from "./CustomNode";
import { useMemo } from "react";
import { Course, useCourses } from "../../../lib/api/useCourses";
interface DegreeMapProps {}

function DegreeMap(props: DegreeMapProps) {
  const { data: courses } = useCourses();

  const nodes: Node[] = useMemo(
    () =>
      courses
        ?.filter((c) =>
          ["COP", "CNT", "CEN", "COT", "CIS"].some((prefix) =>
            c.courseId.includes(prefix)
          )
        )
        ?.map((course, i) => {
          return {
            id: course.courseId,
            position: {
              y: 300 * Math.floor(i / 10),
              x: 300 * (i % 10),
            },
            data: course,
            type: "custom",
          };
        }) ?? [],
    [courses]
  );

  const edges: Edge[] = useMemo(() => {
    if (!courses) return [];
    const e: Edge[] = [];
    for (const course of courses) {
      e.push({
        id: `${course.prerequisites}-to-${course.courseId}`,
        source: course.prerequisites,
        target: course.courseId,
        markerEnd: {
          type: MarkerType.Arrow,
        },
      });
    }
    return e;
  }, [courses]);

  console.log(edges);

  const nodeTypes = useMemo(
    () => ({
      custom: CustomNode,
    }),
    []
  );

  return (
    <ReactFlow nodes={nodes} edges={edges} nodeTypes={nodeTypes}>
      <Background />
    </ReactFlow>
  );
}

export default DegreeMap;
