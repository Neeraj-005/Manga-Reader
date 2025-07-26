import React, { useState,useEffect } from 'react';
import './fun.css';
import { useParams } from 'react-router-dom';

function chapterselect()
{
    const [animeList, setAnimeList] = useState([]);
    const {seriesName} =  useParams();
    console.log("Series name from URL:", seriesName);

    useEffect(() => {
        fetch(`https://localhost:7149/api/Manga/series/${seriesName}/chapters`)
        .then(response => response.json())
        .then(data => {
            setAnimeList(data);
        })
        .catch(error => {
            console.error('Error fetching anime:', error);
        });
        }, [seriesName]);
        const rectangles = [];
    for(let i = 0; i < animeList.length; i++) {
        rectangles.push(
            <div
                key={i}
                className="cont"
                style={{
                    width: '100%',
                    maxWidth: '200px',
                    textAlign: 'center',
                    height: '300px',
                    backgroundColor: 'skyblue',
                    margin: '10px',
                    borderRadius: '10px',
                    // border: '2px solid #333',
                    marginBottom: '100px',
                    // marginLeft:'10px',
                    marginRight: '80px',
                    border: 'solid 2px red',
                    boxSizing: 'border-box'
                }}
                onClick={() => window.open(`https://localhost:7149/api/Manga/files/${animeList[i].id}`, '_blank')}
            >
                 <img
                src="https://upload.wikimedia.org/wikipedia/en/c/c6/Blue_Lock_manga_volume_1.png" // Replace with your image URL
                alt={animeList[i].name} // Add alt text for accessibility
                style={{
                    width: '80%',         // Adjust image size as needed
                    height: 'auto',
                    marginBottom: '10px', // Add some space between image and text
                    borderRadius: '5px',
                    objectFit: 'contain'   // Ensures the image fits within the container without cropping
                }}
            />
                <label htmlFor="index.html">{animeList[i].name}</label>
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
export default chapterselect;