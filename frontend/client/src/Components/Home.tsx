import React from "react";
import { useRecoilState } from "recoil";
import { ApiClientInstance } from "../Lib/ApiClient";
import { JWTAtom, StatusesAtom, UserAtom, UsersAtom } from "../Lib/Atoms";
import { useNavigate } from 'react-router-dom'
import { User } from "../Types/User";
import StatusHubClient from '../Lib/StatusHub'
import Statuses from "./Statuses";
import Users from "./Users";
import { Button, Fade, Stack, Typography } from "@mui/material";
import { Box } from "@mui/system";

function Home() {

  const [jwt, setJwt] = useRecoilState(JWTAtom)
  const [user, setUser] = useRecoilState(UserAtom)
  const [users, setUsers] = useRecoilState(UsersAtom)
  const [statuses, setStatuses] = useRecoilState(StatusesAtom)

  let navigate = useNavigate()

  const initialLoad = async () => {
    setJwt(sessionStorage.getItem('token'))
    setStatuses(await ApiClientInstance.getStatuses())

    const users = await ApiClientInstance.getUsers()
    setUsers(users)
    setUser(user => users.find(u => u.username === user.username) || user)

    const connection = await StatusHubClient(process.env.REACT_APP_API_BASE_URL)
    connection.on("StatusChange", (changedUser: User) => {
      setUsers(users => {
        const newUsers = Array.from(users)
        const idx = newUsers.findIndex(u => u.id === changedUser.id)
        newUsers[idx] = changedUser
        return newUsers
      })

      // Relying on the username here is find since its unique in the backend
      if (user.username === changedUser.username)
        setUser(changedUser)
    });
  }

  const signOutHandler = () => {
    window.sessionStorage.removeItem('token')
    setJwt('')
    navigate('/login')
  }

  React.useEffect(() => {
    if (!jwt) {
      navigate('/login')
    }

    initialLoad()

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])

  return (
    <>
      <Typography component="h1" variant="h3">Greetings {user.username}!</Typography>
      <Stack direction='row' spacing={2} justifyContent='space-around'>
        <Stack direction='row' spacing={2}>
          <Statuses statuses={statuses} />
        </Stack>
        <Button onClick={signOutHandler} variant='outlined' color="secondary">SignOut</Button>
      </Stack>
      <Users users={users} />
    </>
  );
}

export default Home;
