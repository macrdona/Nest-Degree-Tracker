import "./DegreeMap.scss";

import ReactFlow, { Node, Background, Edge, MarkerType } from "reactflow";
import "reactflow/dist/style.css";
import CustomNode from "./CustomNode";
import { useEffect, useMemo, useState } from "react";
import { Course, useCourses } from "../../../lib/api/useCourses";
import ELK, { ElkNode } from "elkjs/lib/elk.bundled";
const elk = new ELK();
interface DegreeMapProps {}

function DegreeMap(props: DegreeMapProps) {
  const { data } = useCourses();

  const [nodesWithLayout, setNodes] = useState<Node[]>([]);

  // TEST DATA
  const majorClasses = [
    "COP2220",
    "MAC2311",
    "MAC2312",
    "PHY2049",
    "PHY2048C",
    "PHY2049",
    "COT3100",
    "COP3503",
    "COP3530",
    "CIS3253",
    "COP3703",
    "CNT4504",
    "CDA3100",
    "COT3210",
    "COP3404",
    "CEN4010",
    "COP4610",
    "COP4620",
    "CAP4630",
    "COT4400",
    "MAS3105",
    "STA3032", // Major reqs
    "SPC4064",
    // "CAP3",
    // "CDA3",
    // "CEN3",
    // "CIS3",
    // "CNT3",
    // "COP3",
    // "COT3",
    // "CAP4",
    // "CDA4",
    // "CEN4",
    // "CIS4",
    // "CNT4",
    // "COP4",
    // "COT4",
  ]; // Major electives

  const courses = useMemo(
    () =>
      data?.filter((c) =>
        majorClasses.some((prefix) => c.courseId == prefix)
      ) ?? [],
    [data]
  );

  const nodes: Node[] = useMemo(
    () =>
      courses.map((course, i) => {
        const number = parseInt(course.courseId.slice(3));

        return {
          id: course.courseId,
          position: {
            y: 300 * Math.floor(number / 1000),
            x: 300 * (i % 10),
          },
          data: course,
          type: "custom",
        };
      }),
    [courses]
  );

  const edges: Edge[] = useMemo(() => {
    if (!courses) return [];
    const e: Edge[] = [];
    for (const course of courses) {
      if (course.prerequisites == "N/A") continue;
      const prereqs = course.prerequisites.split(",");
      for (const prereq of prereqs) {
        if (courses.some((c) => c.courseId === prereq))
          e.push({
            id: `${prereq}-to-${course.courseId}`,
            source: prereq,
            target: course.courseId,
            markerEnd: {
              type: MarkerType.Arrow,
            },
          });
        else console.log(prereq + " not in courses");
      }
    }
    return e;
  }, [courses]);

  const nodeTypes = useMemo(
    () => ({
      custom: CustomNode,
    }),
    []
  );

  // ELKJS stuff
  useEffect(() => {
    const calcLayout = async () => {
      const graph: ElkNode = {
        id: "root",
        layoutOptions: {
          "elk.algorithm": "mrtree",
          "elk.direction": "DOWN",
        },
        children: nodes.map((n) => ({
          id: n.id,
          width: 200,
          height: 200,
        })),
        edges: edges.map((e) => ({
          id: e.id,
          sources: [e.source],
          targets: [e.target],
        })),
      };
      const layout = await elk.layout(graph);
      console.log(layout);
      const newNodes: Node[] =
        layout.children?.map((n) => ({
          id: n.id,
          data: courses.find((c) => c.courseId === n.id),
          position: {
            x: n.x ?? 0,
            y: n.y ?? 0,
          },
          type: "custom",
        })) ?? [];

      setNodes(newNodes);
    };

    calcLayout();
  }, [nodes, edges]);

  // end ELKJS stuff

  return (
    <div className="degree-map h-100 w-100 position-relative">
      <h2 className="display-5 position-absolute p-3 px-5 opacity-50">
        Degree Map
      </h2>
      <ReactFlow nodes={nodesWithLayout} edges={edges} nodeTypes={nodeTypes}>
        <Background />
      </ReactFlow>
    </div>
  );
}

export default DegreeMap;
