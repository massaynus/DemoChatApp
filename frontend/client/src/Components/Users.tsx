import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import dayjs from 'dayjs';
import React from 'react'
import { User } from '../Types/User'

export default function Users({ users }: {
    users: User[]
}) {
    const columns = [
        { id: 'id', label: 'id', minWidth: 100 },
        { id: 'username', label: 'username', minWidth: 100 },
        { id: 'status', label: 'status', minWidth: 100 },
        { id: 'lastStatusChange', label: 'last status change', minWidth: 100 },
        { id: 'elapsed', label: 'elapsed time', minWidth: 100 },
    ]

    const now = new Date().getUTCDate()
    const rows = users.map(user => {
        console.debug(user.username, user.lastStatusChange, dayjs(dayjs(now).diff(user.lastStatusChange)).format('HH:mm:ss'))
        return {
            ...user,
            elapsed:  dayjs(dayjs(now).diff(user.lastStatusChange)).format('HH:mm:ss')
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
                    {dayjs(rowData.lastStatusChange).format('DD-MM-YYYY HH:mm:ss')}
                </TableCell>
                <TableCell key={'elapsed'} align={'center'}>
                    {rowData.elapsed}
                </TableCell>
            </TableRow>
    ))

    return (
        <Paper sx={{ width: '100%', overflow: 'hidden' }}>
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
