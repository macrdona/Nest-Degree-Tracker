import { useContext, useEffect, useMemo, useRef, useState } from "react";
import { Course, useCourses } from "../../../lib/api/useCourses";
import "./RequirementInfo.scss";
import { TrackerContext } from "../DegreeMap/TrackerContext";
import { Modal } from "bootstrap";
import { useAddCourse } from "../../../lib/api/useAddCourse";
import { useAuth } from "../../../lib/auth/AuthContext";
import { useRemoveCourse } from "../../../lib/api/useRemoveCourse";

function RequirementInfo() {
  const {
    selectedRequirement: req,
    setSelectedRequirement,
    setSelectedCourse,
  } = useContext(TrackerContext);
  const { data: courses } = useCourses();

  const { user } = useAuth();

  const { mutate: markComplete } = useAddCourse();
  const { mutate: markIncomplete } = useRemoveCourse();

  const [modal, setModal] = useState<Modal>();

  const modalRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (!modalRef.current) return;

    const modal = new Modal(modalRef.current);
    setModal(modal);

    modalRef.current.addEventListener("hidden.bs.modal", () => {
      setSelectedRequirement(undefined);
      console.log("helloooo");
    });
  }, [modalRef.current]);

  useEffect(() => {
    if (req) modal?.show();
    else modal?.hide();
  }, [req]);

  return (
    <div className="modal fade" tabIndex={-1} ref={modalRef}>
      <div className="modal-dialog modal-lg">
        {req ? (
          <div className="modal-content">
            <div className="modal-header">
              <h4 className="modal-title">{req.name}</h4>

              <button
                type="button"
                className="btn-close"
                onClick={() => setSelectedRequirement(undefined)}
              ></button>
            </div>
            <div className="modal-body">
              <p className="fw-bold mb-1">
                {req.completedCredits} Credits Earned / {req.totalCredits}{" "}
                Credits Total
              </p>
              <div className="progress mb-3">
                <div
                  className="progress-bar"
                  role="progressbar"
                  style={{
                    width: `${
                      (req.completedCredits / req.totalCredits) * 100
                    }%`,
                  }}
                ></div>
              </div>
              {req.missingCourses.length ? (
                <>
                  <p className="fw-bold mb-1">Missing Course Requirements</p>
                  <em>Already completed courses not shown.</em>
                </>
              ) : null}
              <br />
              {req.missingCourses.map((missingCourses) => (
                <p>
                  <em className="fw-bold text-muted">
                    {missingCourses.description}:
                  </em>
                  {missingCourses.courseId.map((courseId, i) => {
                    const course = courses?.find(
                      (course) => course.courseId === courseId
                    );
                    return (
                      <>
                        <br />
                        <a
                          role="button"
                          className="link"
                          onClick={() => setSelectedCourse(course)}
                        >
                          {courseId}: {course?.courseName}
                        </a>
                      </>
                    );
                  })}
                </p>
              ))}
            </div>
            <div className="modal-footer"></div>
          </div>
        ) : null}
      </div>
    </div>
  );
}

export default RequirementInfo;
