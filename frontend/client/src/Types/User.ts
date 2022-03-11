export type User = {
    id: string,
    username: string,
    status: string,
    lastStatusChange: string,
    elapsed: number,
    isOnline: boolean | null,
}

export type UserList = {
    total: number,
    page: number,
    users: User[]
}