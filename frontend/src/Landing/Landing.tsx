import './Landing.scss';

/**
 * The landing page.
 */
function Landing() {
  return (
    <div className='container-fluid bg-black text-light centered flex-fill'>
      <div className='row landing-area p-5 flex-fill h-100 align-items-center'>
        <div className='col-auto col-lg-6'>
          <div className='row py-3 justify-content-center justify-content-lg-start text-center text-lg-start'>
            <h1 className='display-1 landing-text'>Get your Academic Life On Track Today</h1>
            <p className='lead fs-2 fw-bold landing-text'>Track your to-do courses, degree progress, and major requirements all in one place</p>
          </div>
          <div className='row'>
            <div className='d-flex flex-row gap-2 justify-content-center justify-content-lg-start'>
              <button className='btn btn-primary btn-lg mw-50 px-5 shadow-lg'>Login</button>
              <button className='btn btn-secondary btn-lg mw-50 px-5 shadow-lg'>Sign Up</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
export default Landing;