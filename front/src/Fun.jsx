// import React from 'react';   
import React, { useState,useEffect } from 'react';
import './fun.css';

import { Link } from 'react-router-dom';
// import './new1.html';
function Fun()
{
    const [animeList, setAnimeList] = useState([]);
    

    useEffect(() => {
        fetch(`${import.meta.env.VITE_API_URL}/series`)
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
                const seriesName = animeList[i];
                const cleanedSeriesName = seriesName.replace(/\s+/g, ''); 
                const coverImageUrl = `${import.meta.env.VITE_API_URL_MANGA}static-manga-files/Covers/${encodeURIComponent(cleanedSeriesName)}.jpeg`;

        rectangles.push(
            
            <Link
                key={i}
                to={`/chapters/${encodeURIComponent(animeList[i])}`}
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
                   
                    textDecoration: 'none',
                    color: 'inherit', 
                    display: 'flex', 
                    justifyContent: 'center',
                    alignItems: 'center',
                    flexDirection: 'column' 
                }}
                
            >
                
                <img
                src={coverImageUrl} 
                alt={animeList[i].name} 
                style={{
                    flexWrap: 'wrap',
                    width: '80%',         
                    height: 'auto',
                    marginBottom: '10px', 
                    borderRadius: '5px',
                    objectFit: 'contain',   
                    boxSizing: 'border-box' 
                }}
            />
                
                <span>{animeList[i]}</span>
            </Link>
        );
    }
   
    return(
    <div  style={{
        display: 'grid',
        // alignContent: 'flex-start',
        gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))', 
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