import CourseInfo from "./CourseInfo/CourseInfo";
import CreditsOverview from "./CreditsOverview/CreditsOverview";
import DegreeMap from "./DegreeMap/DegreeMap";
import { TrackerContextProvider } from "./DegreeMap/TrackerContext";
import "./Tracker.scss";

interface TrackerProps {}

function Tracker(props: TrackerProps) {
  return (
    <TrackerContextProvider>
      <div className="container-fluid flex-fill">
        <div className="row flex-nowrap h-100">
          <div className="col-auto px-0 h-100 p-0">
            <CreditsOverview />
          </div>
          <div className="col h-100 p-0">
            <DegreeMap />
          </div>
        </div>
      </div>
      <CourseInfo />
    </TrackerContextProvider>
  );
}

export default Tracker;
