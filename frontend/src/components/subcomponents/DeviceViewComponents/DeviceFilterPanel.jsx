import React, {useEffect, useRef, useState} from "react";
import { propTypes } from "react-bootstrap/esm/Image";
import { SelectField, TextField } from "../../common/form";

export const DeviceFilterPanel = (props) =>{

    return(
        <React.Fragment>
            {props.children}
        </React.Fragment>
    )
}