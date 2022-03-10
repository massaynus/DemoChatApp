export type SignupRequest = {
    username: string,
    password: string
}

export type SignupResponse = {
    username: string,
    status: string,
    message: string
}