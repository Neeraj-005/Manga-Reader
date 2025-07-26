import React, { useState } from 'react';
import './fun.css';
// import './new1.html';
function Fun()
{
    const rectangles = [];
    for (let i = 0; i < 12; i++) {
        rectangles.push(

            <div key={i} className="cont" style={{
                width: '100%',
                maxWidth: '200px',
                textAlign:'center',
                height: '300px',
                backgroundColor: 'skyblue',
                margin: '10px',
                border: '2px solid #333',
                marginBottom: '100px',
                // marginLeft:'10px',
                marginRight:'80px',
                border:'solid 2px red',
                boxSizing: 'border-box'
            }}  onClick={() => window.location.href = `/new1.html?message=${encodeURIComponent(i+1)}`}>{i+1}
            </div>
        );
    }
    return(
        <div style={{
            display: 'flex',
            flexWrap: 'wrap',
            marginTop:'200px',
            justifyContent: 'center',
            alignItems: 'center',
            gap: '20px',
            border:'solid 2px red'
        }}>
            {rectangles}
        </div>
    );
}
export default Fun