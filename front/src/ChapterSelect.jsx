import React, { useState,useEffect } from 'react';
import  './fun.css';
import PDFRender from './PDFRender';
import { useParams } from 'react-router-dom';

function chapterselect()
{
    const [animeList, setAnimeList] = useState([]);
    const {seriesName} =  useParams();
    const [selectedChapterId, setSelectedChapterId] = useState(null);
    console.log("Series name from URL:", seriesName);
    const cleanedSeriesName = seriesName.replace(/\s+/g, ''); 
    const coverImageUrl = `${import.meta.env.VITE_API_URL_MANGA}static-manga-files/Covers/${encodeURIComponent(cleanedSeriesName)}.jpeg`

  
    const [pdfUrlToDisplay, setPdfUrlToDisplay] = useState(null);

    useEffect(() => {
        fetch(`${import.meta.env.VITE_API_URL}/series/${encodeURIComponent(seriesName)}/chapters`) 
        .then(response => response.json())
        .then(data => {
            setAnimeList(data);
        })
        .catch(error => {
            console.error('Error fetching anime:', error);
        });
        }, [seriesName]); 

    const rectangles = [];

    if (animeList.length === 0) {
        return <p>No chapters available for this series.</p>;
    }

    for(let i = 0; i < animeList.length; i++) {
        const pdfUrl = `${import.meta.env.VITE_API_URL}/files/${animeList[i].id}`;

        rectangles.push(
            <div
                key={animeList[i].id} 
                className="cont" 
                // style={{
                //     width: '100%',
                //     maxWidth: '200px',
                //     textAlign:'center',
                //     height: '300px',
                //     backgroundColor: 'skyblue',
                //     margin: '10px',
                //     borderRadius:'10px',
                //     // border: '2px solid #333',
                //     marginBottom: '100px',
                //     // marginLeft:'10px',
                //     marginRight:'80px',
                //     border:'solid 2px red',
                //     boxSizing: 'border-box'
                // }}
                
                onClick={() => {
                    const selectedChapter = animeList[i].id;
                    
                    console.log("Clicked chapter ID:", animeList[i].id);
                    console.log("Setting PDF URL for iframe:", pdfUrl);
                    setPdfUrlToDisplay(pdfUrl); 
                }}
            >
                <img
                // src={coverImageUrl} 
                alt={animeList[i].name} 
                style={{
                    flexWrap: 'wrap',
                    width: '100%',         
                    height: 'auto',
                    marginBottom: '10px', 
                    borderRadius: '5px',
                    objectFit: 'contain',   
                    boxSizing: 'border-box',
                    cursor:"pointer", 
                }}
            />
                {}
                <span>{animeList[i].name}</span> {}
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
            {}
            {animeList.length === 0 && <p>No chapters available for this series.</p>}

            {pdfUrlToDisplay && (
                <PDFRender
                    pdfUrl={pdfUrlToDisplay}
                    onClose={() => setPdfUrlToDisplay(null)}
                />
        )}
        </div>
    );
}

export default chapterselect;