import React, {useEffect, useRef} from 'react';

export const PersonDeviceActions = ({buttons}) => {
    let rowOpen = false;
    return(
        <React.Fragment>
            <div className="row">
                {buttons.map((indiv)=>(
                    <div className = "col"> {indiv}</div>
                ))}

            </div>
            
        </React.Fragment>
    )

}