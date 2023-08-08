import React from "react";

const CheckButton = ({id, value, label, toggleCheckBox}) =>
    <label className="col-form-check-label d-block pl-1 pr-1 pt-1 pb-1" htmlFor={id}>
        <input tabIndex="" className="form-check-input m-1 mr-2" type="checkbox"
               checked={value}
               id={id}
               onChange={() => toggleCheckBox(id)}/>
        {label}
    </label>
export default CheckButton;
