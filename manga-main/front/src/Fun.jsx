// import React from 'react';   
import React, { useState,useEffect } from 'react';
import './fun.css';

import { Link } from 'react-router-dom';
// import './new1.html';
function Fun()
{
    const [animeList, setAnimeList] = useState([]);

    useEffect(() => {
        fetch('https://localhost:7149/api/Manga/series')
        .then(response => response.json())
        .then(data => {
            setAnimeList(data);
        })
        .catch(error => {
            console.error('Error fetching anime:', error);
        });
    }, []);
    const rectangles = [];
    for(let i = 0; i < animeList.length; i++) {
        rectangles.push(
            // Use the Link component for navigation within React Router
            <Link
                key={i} // Crucial for list rendering performance and identifying unique elements
                to={`/chapters/${encodeURIComponent(animeList[i])}`} // Navigate to chapter list page with series name
                className="cont"
                style={{
                    
                    width: '100%',
                    maxWidth: '200px',
                    textAlign:'center',
                    height: '300px',
                    backgroundColor: 'skyblue',
                    margin: '10px',
                    borderRadius:'10px',
                    marginBottom: '100px',
                    marginRight:'80px',
                    border:'solid 2px red',
                    boxSizing: 'border-box',
                    // Styles to make the Link look like your original div, removing default underline
                    textDecoration: 'none',
                    color: 'inherit', // Inherit text color from parent for consistency
                    display: 'flex', // Use flexbox for centering content inside the Link
                    justifyContent: 'center',
                    alignItems: 'center',
                    flexDirection: 'column' // Stack content vertically if needed
                }}
            >
                <img
                src="https://upload.wikimedia.org/wikipedia/en/c/c6/Blue_Lock_manga_volume_1.png" // Replace with your image URL
                alt={animeList[i].name} // Add alt text for accessibility
                style={{
                    flexWrap: 'wrap',
                    width: '80%',         // Adjust image size as needed
                    height: 'auto',
                    marginBottom: '10px', // Add some space between image and text
                    borderRadius: '5px',
                    objectFit: 'contain',   // Ensures the image fits within the container without cropping
                    boxSizing: 'border-box' // Ensures padding and border are included in the element's total width and height
                }}
            />
                {/* Use a <span> or <div> for displaying text if it's not associated with an <input> */}
                <span>{animeList[i]}</span>
            </Link>
        );
    }
   
    return(
    <div  style={{
        display: 'grid',
        // alignContent: 'flex-start',
        gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))', // Creates a matrix
        flexWrap: 'wrap',
        gap: '20px',
        padding: '20px',
        justifyContent: 'center',
        marginTop: '200px',
        alignItems: 'center',
        border: 'solid 2px red'
    }}>
        {rectangles}
    </div>
    );
}
export default Fun