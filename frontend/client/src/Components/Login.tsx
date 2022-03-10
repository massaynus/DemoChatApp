import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { ApiClientInstance } from '../Lib/ApiClient';
import { useRecoilState } from 'recoil';
import { JWTAtom, UserAtom } from '../Lib/Atoms';
import { useNavigate } from 'react-router-dom'

const theme = createTheme();

export default function SignIn() {
  const [validationText, setvalidationText] = React.useState('')
  const [, setJwt] = useRecoilState(JWTAtom)
  const [, setUser] = useRecoilState(UserAtom)
  let navigate = useNavigate()

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);

    if (!event.currentTarget.checkValidity()) {
      setvalidationText('Username and password are required')
      return
    }

    const response = await ApiClientInstance.logIn({
      username: data.get('username')?.toString() || '',
      password: data.get('password')?.toString() || ''
    });

    if (response.operationResult === 0) {
      setvalidationText('')
      setJwt(response.jwtToken)
      setUser((old) => ({...old, username: response.username}))
      navigate('/')
    }
    else setvalidationText('Incorrect username or password')
  };

  return (
    <ThemeProvider theme={theme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h3">
            Sign in
          </Typography>
          <Typography component="h5" variant="subtitle1" color={'red'}>
            {validationText}
          </Typography>
          <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
            <TextField
              margin="normal"
              required
              fullWidth
              id="username"
              label="Username"
              name="username"
              autoComplete="username"
              autoFocus
            />
            <TextField
              margin="normal"
              required
              fullWidth
              name="password"
              label="Password"
              type="password"
              id="password"
              autoComplete="current-password"
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Login
            </Button>
            <Grid container>
              <Grid item>
                <Link href="/register" variant="body2">
                  {"Don't have an account? Sign Up"}
                </Link>
              </Grid>
            </Grid>
          </Box>
        </Box>
      </Container>
    </ThemeProvider>
  );
}