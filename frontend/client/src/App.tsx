import { Container, createTheme, CssBaseline, ThemeProvider } from '@mui/material';
import { SnackbarProvider } from 'notistack';
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
      <SnackbarProvider
        variant='success'
        anchorOrigin={{
          vertical: 'top',
          horizontal: 'right',
        }}
        maxSnack={5}>
        <Container>
          <CssBaseline />
          <Routes >
            <Route path="/register" element={<SignUp />} />
            <Route path="/login" element={<Login />} />
            <Route path="/" element={<Home />} />
          </Routes>
        </Container>
      </SnackbarProvider>
    </ThemeProvider>
  );
}

export default App;
