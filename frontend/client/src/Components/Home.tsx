import React from "react";
import { useRecoilState } from "recoil";
import { ApiClientInstance } from "../Lib/ApiClient";
import { JWTAtom, StatusesAtom, UserAtom, UsersAtom } from "../Lib/Atoms";
import { useNavigate } from 'react-router-dom'
import { User } from "../Types/User";
import StatusHubClient from '../Lib/StatusHub'
import Statuses from "./Statuses";
import Users from "./Users";
import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Stack, TextField, Typography } from "@mui/material";
import { useSnackbar } from "notistack";
import { Status } from "../Types/Status";
import _ from 'lodash'
import { HubConnection } from "@microsoft/signalr";

function Home() {

  const { enqueueSnackbar } = useSnackbar()

  const [jwt, setJwt] = useRecoilState(JWTAtom)
  const [user, setUser] = useRecoilState(UserAtom)
  const [users, setUsers] = useRecoilState(UsersAtom)
  const [statuses, setStatuses] = useRecoilState(StatusesAtom)

  const [isCreateStatusModalOpen, setIsCreateStatusModalOpen] = React.useState(false);
  const [createStatusTxt, setCeateStatusTxt] = React.useState('');

  const handleClickOpen = () => {
    setIsCreateStatusModalOpen(true);
    setCeateStatusTxt('')
  };

  const handleClose = () => {
    setIsCreateStatusModalOpen(false);
    setCeateStatusTxt('')
  };

  const handleCreate = async () => {
    try {
      await ApiClientInstance.createStatus({ statusName: createStatusTxt })
    } catch (error) {
      enqueueSnackbar(
        `Unfortunately the new status ${createStatusTxt} was not created!`,
        { variant: 'error' }
      )
    } finally {
      handleClose()
    }
  }

  let navigate = useNavigate()

  const initialLoad = async () => {
    try {
      setJwt(sessionStorage.getItem('token'))
      setStatuses(await ApiClientInstance.getStatuses())
    } finally {
      if (!jwt) {
        navigate('/login')
        return
      }
    }

    const users = await ApiClientInstance.getOnlineUsers()
    setUsers(users)
    setUser(user => users.users.find(u => u.username === user.username) || user)

    const connection = await StatusHubClient(process.env.REACT_APP_API_BASE_URL)
    connection.on("StatusChange", (changedUser: User) => {
      setUsers(usersList => {
        const users = usersList.users
        const newUsers = Array.from(users)
        const idx = newUsers.findIndex(u => u.id === changedUser.id)
        newUsers[idx] = changedUser

        return { ...usersList, users: newUsers }
      })

      // Relying on the username here is find since its unique in the backend
      if (user.username === changedUser.username) {
        setUser(changedUser)
        enqueueSnackbar(
          `Status changed to ${changedUser.status} successfuly!`
        )
      } else {
        enqueueSnackbar(
          `User ${changedUser.username} changed their status to ${changedUser.status}`
        )
      }
    });

    connection.on("StatusAdded", (status: Status) => {
      enqueueSnackbar(
        `New Status added ${status.statusName}!`,
        { variant: 'info' }
      )

      setStatuses(sts => _.uniqBy([...sts, status], s => s.statusName))
    });

    connection.on("UserLoggedIn", async (newUser: User) => {
      enqueueSnackbar(
        newUser.username === user.username
          ? `Welcomeback ${user.username}!!`
          : `User ${newUser.username} Logged In!`,
        { variant: 'info', preventDuplicate: true }
      )

      const users = await ApiClientInstance.getOnlineUsers()
      setUsers(users)
    });

    connection.on("UserSignOff", async (id: string) => {

      const user = users.users.find(u => u.id === id)

      if (user) {
        enqueueSnackbar(
          `User ${user.username} Signed Off!`,
          { variant: 'info', preventDuplicate: true }
        )
      }

      const onlineUsers = await ApiClientInstance.getOnlineUsers()
      setUsers(onlineUsers)
    });

  }

  const signOutHandler = async () => {
    window.sessionStorage.removeItem('token')
    setJwt('')
    const cn = await StatusHubClient(process.env.REACT_APP_API_BASE_URL)
    cn.off("StatusChange")
    cn.off("StatusAdded")
    cn.off("UserLoggedIn")
    cn.off("UserSignOff")
    cn.stop()
    navigate('/login')
  }

  React.useEffect(() => {
    initialLoad()

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])

  return (
    <>
      <Typography component="h1" variant="h3" marginY={5}>Greetings {user.username}!</Typography>
      <Stack direction='row' spacing={2} justifyContent='space-between' marginY={5}>
        <Stack direction='row' spacing={2}>
          <Statuses statuses={statuses} />
        </Stack>
        <Button variant="outlined" onClick={handleClickOpen}>
          Create new status
        </Button>
        <Button onClick={signOutHandler} variant='outlined' color="secondary">SignOut</Button>
      </Stack>
      <Users users={users.users} />

      <Dialog open={isCreateStatusModalOpen} onClose={handleClose}>
        <DialogTitle>Subscribe</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Enter the status name you want to create
          </DialogContentText>
          <TextField
            autoFocus
            margin="dense"
            id="statusName"
            label="Statue Name"
            type="text"
            fullWidth
            variant="standard"
            onChange={(e) => setCeateStatusTxt(e.target.value)}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
          <Button onClick={handleCreate}>Create</Button>
        </DialogActions>
      </Dialog>
    </>
  );
}

export default Home;
