import { User } from "./User"

export type Status = {
    statusName: string,
}

export type UpdateStatusRequest = {
    status: string
}

export type UpdateStatusResponse = User
