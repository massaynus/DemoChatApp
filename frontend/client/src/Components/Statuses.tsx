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
            key={status.statusName}
            variant={status.statusName === currentStatus ? 'contained' : 'outlined'}
            color={status.statusName === currentStatus ? 'warning' : 'info'}
            onClick={() => updateStatus(status.statusName)}
          >
            {status.statusName}
          </Button>
        ))
      }
    </>
  )
}
