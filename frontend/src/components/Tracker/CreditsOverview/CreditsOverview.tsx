import { useContext, useEffect, useMemo } from "react";
import { useRequirements } from "../../../lib/api/useRequirements";
import "./CreditsOverview.scss";
import { TrackerContext } from "../DegreeMap/TrackerContext";

interface CreditsOverviewProps {}

function CreditsOverview(props: CreditsOverviewProps) {
  const { data: requirements } = useRequirements();
  const { selectedRequirement, setSelectedRequirement } =
    useContext(TrackerContext);

  useEffect(() => {
    if (selectedRequirement) {
      const newReq = requirements?.find(
        (req) => req.name == selectedRequirement.name
      );
      setSelectedRequirement(newReq);
    }
  }, [requirements]);

  return (
    <div className="credits-overview d-flex flex-column pt-4 min-h-100 shadow">
      <h2 className="display-5 mb-4 opacity-50 px-4">Credits</h2>
      {requirements?.map((req) => (
        <div
          className="mb-1 requirement px-4"
          onClick={() => setSelectedRequirement(req)}
        >
          <h3>
            {req.name} ({req.completedCredits}/{req.totalCredits})
          </h3>
          <div
            className="progress"
            style={{
              width: "400px",
            }}
          >
            <div
              className="progress-bar"
              role="progressbar"
              style={{
                width: `${(req.completedCredits / req.totalCredits) * 100}%`,
              }}
            ></div>
          </div>
        </div>
      ))}
    </div>
  );
}

export default CreditsOverview;
