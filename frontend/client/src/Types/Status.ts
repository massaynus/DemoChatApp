import { User } from "./User"

export type Status = {
    Status: string,
}

export type UpdateStatusRequest = {
    Status: string
}

export type UpdateStatusResponse = User
