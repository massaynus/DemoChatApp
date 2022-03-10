import { Container, createTheme, CssBaseline, ThemeProvider } from '@mui/material';
import React from 'react';
import {
  Routes,
  Route
} from "react-router-dom";
import Home from './Components/Home';
import Login from './Components/Login';
import SignUp from './Components/SignUp';

const theme = createTheme({
  palette: {
    mode: 'dark'
  }
});

function App() {
  return (
    <ThemeProvider theme={theme}>
      <Container>
        <CssBaseline />
        <Routes >
          <Route path="/register" element={<SignUp />} />
          <Route path="/login" element={<Login />} />
          <Route path="/" element={<Home />} />
        </Routes>
      </Container>
    </ThemeProvider>
  );
}

export default App;
