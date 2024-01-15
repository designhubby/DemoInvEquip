import React, {useState, useRef,useEffect} from 'react';

export function ContractViewTopPanel({title, lines}){

    function renderView(title, lines){

        return(
            <>
                <h1>{title}</h1>
                <table className = 'table table-dark contractdetail'>
                {
                    lines.map(indivLine=>(
                        <tr className='contractdetaildata'>
                        {
                            indivLine.map(lineElement=>(
                                <td className="contractdetaildata">{lineElement}</td>
                            ))
                        }
                        </tr>
                        )
                    )
                }
                </table>
            </>
        )
    }

    return(
        renderView(title, lines)
    )
}