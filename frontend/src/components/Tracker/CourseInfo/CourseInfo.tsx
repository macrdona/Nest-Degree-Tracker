import { useContext, useEffect, useMemo, useRef, useState } from "react";
import { Course } from "../../../lib/api/useCourses";
import "./CourseInfo.scss";
import { TrackerContext } from "../DegreeMap/TrackerContext";
import { Modal } from "bootstrap";
import { useCompletedCourses } from "../../../lib/api/useCompletedCourses";
import { useAddCourse } from "../../../lib/api/useAddCourse";
import { useAuth } from "../../../lib/auth/AuthContext";
import { useRemoveCourse } from "../../../lib/api/useRemoveCourse";

function CourseInfo() {
  const { selectedCourse, setSelectedCourse } = useContext(TrackerContext);

  const { user } = useAuth();

  const { mutate: markComplete } = useAddCourse();
  const { mutate: markIncomplete } = useRemoveCourse();

  const { data: completedCourses } = useCompletedCourses();
  const isCompleted = useMemo(() => {
    return (
      completedCourses?.find((c) => c.courseId === selectedCourse?.courseId)
        ?.completed ?? false
    );
  }, [completedCourses, selectedCourse]);

  const [modal, setModal] = useState<Modal>();

  const modalRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (!modalRef.current) return;

    const modal = new Modal(modalRef.current);
    setModal(modal);

    modalRef.current.addEventListener("hidden.bs.modal", () => {
      setSelectedCourse(undefined);
      console.log("helloooo");
    });
  }, [modalRef.current]);

  useEffect(() => {
    if (selectedCourse) modal?.show();
    else modal?.hide();
  }, [selectedCourse]);

  return (
    <div className="modal hide" tabIndex={-1} ref={modalRef}>
      <div className="modal-dialog modal-lg">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">
              {selectedCourse?.courseId}: {selectedCourse?.courseName}
            </h5>
            <button
              type="button"
              className="btn-close"
              onClick={() => setSelectedCourse(undefined)}
            ></button>
          </div>
          <div className="modal-body">
            <p className="fw-bold mb-1">Description</p>
            <p>{selectedCourse?.description}</p>
            <p className="fw-bold mb-1">Availability</p>
            <p>
              {selectedCourse?.availability ??
                "No availability information provided."}
            </p>
            <p className="fw-bold mb-1">Prerequisites</p>
            <p>{selectedCourse?.prerequisites}</p>

            <p className="fw-bold mb-1">Corequisites</p>
            <p>{selectedCourse?.coRequisites}</p>

            <p className="fw-bold mb-1">
              Credit Hours: {selectedCourse?.credits}
            </p>
          </div>
          <div className="modal-footer">
            {isCompleted ? (
              <>
                <em className="me-auto">Completed</em>
                <button
                  className="btn btn-warning"
                  onClick={() => {
                    if (selectedCourse && user)
                      markIncomplete({
                        courseId: selectedCourse.courseId,
                        userId: user.id,
                      });
                  }}
                >
                  Mark as Incomplete
                </button>
              </>
            ) : (
              <button
                className="btn btn-success"
                onClick={() => {
                  if (selectedCourse && user)
                    markComplete({
                      courseId: selectedCourse.courseId,
                      userId: user.id,
                    });
                }}
              >
                Mark as Completed
              </button>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default CourseInfo;