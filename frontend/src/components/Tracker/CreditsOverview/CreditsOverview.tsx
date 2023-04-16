import { useRequirements } from "../../../lib/api/useRequirements";
import "./CreditsOverview.scss";

interface CreditsOverviewProps {}

function CreditsOverview(props: CreditsOverviewProps) {
  const { data: requirements } = useRequirements();

  return (
    <div className="credits-overview d-flex flex-column px-4 pt-4 min-h-100 shadow">
      <h2 className="display-5 mb-4 opacity-50">Credits</h2>
      {requirements?.map((req) => (
        <div className="mb-3">
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
