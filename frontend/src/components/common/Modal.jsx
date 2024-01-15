import React, {useState, useRef} from 'react';


export function Modal({handleClose, show, children}){

    const showStatus = show ? 'modal1 display-block' : 'modal1 display-none';
    const modalInject = "";
    let classInfo = showStatus == 'modal1 display-block' ? 'border modal-main' : '';
    
    return(
        <div className={showStatus}>
            <section className = {classInfo}>
                {show && children(modalInject)}
                <button type = "button" className = "btn btn-primary" onClick = {()=>handleClose()}>Close</button>
            </section>
        </div>
    )

}