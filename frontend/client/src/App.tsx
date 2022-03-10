import React, { useEffect } from 'react';
import {
  Routes,
  Route
} from "react-router-dom";
import Home from './Components/Home';
import Login from './Components/Login';
import SignUp from './Components/SignUp';
import { instance } from './Lib/ApiClient';
import StatusHubClient from './Lib/StatusHub'
import { User } from './Types/User';

function App() {
  useEffect(() => {
    (
      async function () {
        await instance.logIn({username: "massaynus", password: "password"})
        const connection = await StatusHubClient(process.env.REACT_APP_API_BASE_URL)
        connection.on("StatusChange", (user: User) => {
          console.log('user status changed:', user)
        });
      }
    )()
  }, [])

  return (
    <>
      <div>
        <Routes >
          <Route path="/register" element={<SignUp />} />
          <Route path="/login" element={<Login />} />
          <Route path="/" element={<Home />} />
        </Routes>
      </div>
    </>
  );
}

export default App;
