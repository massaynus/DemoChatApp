import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import dayjs from 'dayjs';
import React, { useEffect, useState } from 'react'
import { useRecoilValue } from 'recoil';
import { UserAtom } from '../Lib/Atoms';
import { User } from '../Types/User'

const columns = [
    { id: 'id', label: 'id', minWidth: 100 },
    { id: 'username', label: 'username', minWidth: 100 },
    { id: 'status', label: 'status', minWidth: 100 },
    { id: 'lastStatusChange', label: 'last status change', minWidth: 100 },
    { id: 'elapsed', label: 'elapsed time', minWidth: 100 },
]

export default function Users({ users }: {
    users: User[]
}) {

    const user = useRecoilValue(UserAtom)
    const [now, setNow] = useState(Date.now())

    useEffect(() => {
        setInterval(() => setNow(Date.now()), 1000)
    }, [users])

    const rows = users
    .filter(u => u.username !== user.username)
    .map(user => {
        return {
            ...user,
            elapsed: dayjs(now).diff(user.lastStatusChange),
            lastStatusChange: dayjs(user.lastStatusChange).format('DD/MM/YYYY HH:mm:ss')
        }
    })

    const tableRows = rows.map(rowData => (
        <TableRow hover role="checkbox" tabIndex={-1} key={rowData.id}>
            <TableCell key={'id'} align={'center'}>
                {rowData.id}
            </TableCell>
            <TableCell key={'username'} align={'center'}>
                {rowData.username}
            </TableCell>
            <TableCell key={'status'} align={'center'}>
                {rowData.status}
            </TableCell>
            <TableCell key={'lastStatusChange'} align={'center'}>
                {rowData.lastStatusChange}
            </TableCell>
            <TableCell key={'elapsed'} align={'center'}>
                {dayjs(rowData.elapsed).format('HH:mm:ss')}
            </TableCell>
        </TableRow>
    ))

    return (
        <Paper sx={{ width: '100%', overflow: 'hidden' }} elevation={12}>
            <TableContainer sx={{ maxHeight: 440 }}>
                <Table stickyHeader aria-label="sticky table">
                    <TableHead>
                        <TableRow>
                            {columns.map((column) => (
                                <TableCell
                                    key={column.id}
                                    align={'center'}
                                    style={{ minWidth: column.minWidth }}
                                >
                                    {column.label}
                                </TableCell>
                            ))}
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {tableRows}
                    </TableBody>
                </Table>
            </TableContainer>
        </Paper>
    )
}
