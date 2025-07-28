import React from "react";

function PdfViewer({ pdfUrl, onClose }) {
    return (
         <div style={{
            position: 'fixed',
            top: 0, left: 0,
            width: '100%',
            height: '100%',
            backgroundColor: 'rgba(0, 0, 0, 0.8)',
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'center',
            alignItems: 'center',
            zIndex: 1000
        }}>
        
            <button
                onClick={onClose}
                style={{
                    position: 'absolute',
                    top: '10px',
                    right: '10px',
                    zIndex: 1001,
                    cursor: 'pointer',
                }}
            >
                x
            </button>
            <iframe
                src={pdfUrl}
                width="100%"
                height="100%"
                style={{ border: 'none', borderRadius: '8px' }}
                title="Manga PDF Viewer" />
        </div>
    );
}

export default PdfViewer;