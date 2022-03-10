export type LoginRequest = {
    username: string,
    password: string
}

export type LoginResponse = {
    username: string,
    jwtToken: string,
    operationResult: 0 | 1
}