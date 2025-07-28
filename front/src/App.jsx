import React from 'react'; // ← REQUIRED for JSX
import { useEffect, useState } from "react";
import Fun from './Fun.jsx';
import ChapterSelect from './chapterselect.jsx';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

function App() {
  return (
    // Wrap your entire application content that uses routing within <Router>
    <Router>
      {/* <Routes> is where you define all your different pages/components */}
      <Routes>
        {/* Define the route for your first page (Fun.jsx) */}
        {/* When the URL is '/', the Fun component will be rendered */}
        <Route path="/" element={<Fun />} />

        {/* Define the route for your second page (ChapterSelect.jsx) */}
        {/* When the URL is '/chapters/SOME_SERIES_NAME', the ChapterSelect component will be rendered */}
        {/* The ':seriesName' part is a dynamic URL parameter */}
        <Route path="/chapters/:seriesName" element={<ChapterSelect />} />

        {/* Optional: Add a catch-all route for 404 Not Found pages */}
        {/* <Route path="*" element={<div>404 Not Found</div>} /> */}

      </Routes>
    </Router>
  );
}

export default App;
