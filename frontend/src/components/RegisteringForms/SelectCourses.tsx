import React, {useState} from 'react';
import { toast } from "react-toastify";
import Steps2 from '../../assets/steps-image-2.png';
import "bootstrap/dist/css/bootstrap.min.css";
import './SelectCourses.scss';

function selectCourses() {
    const major = "Computer Science"; //placeholder will get real major
    const minor = "Biology"; //placeholder will get real major
    const userMajorCourses = [];
    const userMinorCourses = [];
    const userGenEds = [];

    let majorCourses = [ //all of this is a placeholder for the real list from the database
        {name: "Intro to Databases"},
        {name: "Algorithms"},
        {name: "Data Structures"}
    ]
    let minorCourses = [ //all of this is a placeholder for the real list from the database
        {name: "Physical Therapy"},
        {name: "Stress Management"}
    ]
    let genEds = [ //all of this is a placeholder for the real list from the database
        {name: "Biology"},
        {name: "Earth Science"}
    ]

    const [isActive, setActive] = useState(false);

    const handleClick = () => {
        //will change the button color
        //need to add a handle submit for the next button
        //handle submit will store all the button values currently selected
        setActive(isActive => !isActive)
       }
    const handleSubmit = () =>{
    //wip
    }


    return(
        <div className="main container justify-content-center">
            <div className="row justify-content-center">
                <img src={Steps2}/>
            </div>
            <div className="row justify-content-center">
                Select all completed courses
            </div>
            <div className="row justify-content-center">
                <div className="col flex-nowrap">Major: <u>{major}</u></div>
            </div>
            <div className="buttonContainer row justify-content-center">
                    {majorCourses.map((course) => <button type="button" id="btn" onClick={handleClick} className={'col-md-4 ' + (isActive ? "selectedButton" : "defaultButton")}>{course.name}</button>)}
            </div>
            <div className="row justify-content-center">
                <div className="col flex-nowrap">Minor: <u>{minor}</u></div>
            </div>
            <div className="buttonContainer row justify-content-center">
                {minorCourses.map((course) => <button type="button" id="btn" onClick={handleClick} className={'col-md-4 ' + (isActive ? "selectedButton" : "defaultButton")}>{course.name}</button>)}
            </div>
            <div className="row justify-content-center">
                <div className="col flex-nowrap">General Education Courses:</div>
            </div>
            <div className="buttonContainer row justify-content-center">
                {genEds.map((course) => <button type="button" className="defaultButton col-md-4">{course.name}</button>)}
            </div>
            <div className="row justify-content-center">
            <div className="col justify-content-center flex-nowrap">
                <button
                    onClick={(e) => {handleSubmit();}}
                    className="btn btn-primary btn-lg mt-3 fs-2 mw-75 align-self-center text-nowrap" type="submit">
                    Next
                </button>
            </div>
            </div>
        </div>
    );
}
export default selectCourses;