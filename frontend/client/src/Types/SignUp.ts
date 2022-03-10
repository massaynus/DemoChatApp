export type SignupRequest = {
    firstName: string,
    lastName: string,
    username: string,
    password: string
}

export type SignupResponse = {
    username: string,
    status: string,
    message: string
}