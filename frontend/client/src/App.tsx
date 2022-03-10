import React, { useEffect } from 'react';
import {
  Routes,
  Route
} from "react-router-dom";
import Home from './Components/Home';
import Login from './Components/Login';
import SignUp from './Components/SignUp';
import { instance } from './Lib/ApiClient';

function App() {
  instance.getStatuses()
    .then(s => console.log(s))

  return (
    <>
      <div>
        <Routes >
          <Route path="/register" element={<SignUp />} />
          <Route path="/login" element={<Login />} />
          <Route path="/" element={<Home />}/>
        </Routes>
      </div>
    </>
  );
}

export default App;
