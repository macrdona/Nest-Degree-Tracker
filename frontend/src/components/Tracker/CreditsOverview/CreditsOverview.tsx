import "./CreditsOverview.scss";

interface CreditsOverviewProps {}

function CreditsOverview(props: CreditsOverviewProps) {
  return (
    <div className="credits-overview d-flex flex-column px-4 pt-4 min-h-100 shadow">
      <h2 className="display-5 mb-4">Credits</h2>
      <div className="mb-3">
        <h3>General Education (40/120)</h3>
        <div
          className="progress"
          style={{
            width: "400px",
          }}
        >
          <div
            className="progress-bar"
            role="progressbar"
            style={{ width: "35%" }}
          ></div>
        </div>
      </div>
      <div className="mb-3">
        <h3>Major Requirements (69/420)</h3>
        <div
          className="progress"
          style={{
            width: "400px",
          }}
        >
          <div
            className="progress-bar"
            role="progressbar"
            style={{ width: "15%" }}
          ></div>
        </div>
      </div>
      <div className="mb-3">
        <h3>Major Electives (3/9)</h3>
        <div
          className="progress"
          style={{
            width: "400px",
          }}
        >
          <div
            className="progress-bar"
            role="progressbar"
            style={{ width: "33%" }}
          ></div>
        </div>
      </div>
    </div>
  );
}

export default CreditsOverview;
