export type LoginRequest = {
    Username: string,
    Password: string
}

export type LoginResponse = {
    Username: string,
    JWTToken: string,
    OperationResult: 0 | 1
}