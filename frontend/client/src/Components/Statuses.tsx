import { Button } from '@mui/material'
import React from 'react'
import { useRecoilValue } from 'recoil'
import { ApiClientInstance } from '../Lib/ApiClient'
import { UserAtom } from '../Lib/Atoms'
import { Status } from '../Types/Status'

export default function Statuses({statuses}: {
    statuses: Status[]
}) {

  const user = useRecoilValue(UserAtom)
  const currentStatus = user.status
  const updateStatus = async (status: string) => {
    await ApiClientInstance.updateStatus({status})
  }

  return (
    <>
      {
        statuses.map(status => (
          <Button
            variant='contained'
            color={status.statusName === currentStatus ? 'success' : 'primary'}
            onClick={() => updateStatus(status.statusName)}
          >
            {status.statusName}
          </Button>
        ))
      }
    </>
  )
}
