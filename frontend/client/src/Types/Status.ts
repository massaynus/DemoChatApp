import { User } from "./User"

export type Status = {
    status: string,
}

export type UpdateStatusRequest = {
    status: string
}

export type UpdateStatusResponse = User
