import axios, { Axios, AxiosRequestHeaders } from "axios";
import { LoginRequest, LoginResponse } from "../Types/Login";
import { SignupRequest, SignupResponse } from "../Types/SignUp";
import { Status, UpdateStatusRequest, UpdateStatusResponse } from "../Types/Status";
import { User } from "../Types/User";

export default class ApiClient {
    private headers: AxiosRequestHeaders;
    private axios: Axios;

    constructor (apiHost: string = 'http://localhost:5002') {
        this.headers = {}

        this.axios = new axios.Axios({
            baseURL: apiHost,
            headers: this.headers
        })

        console.log(`Api Base url: ${apiHost}`)
    }

    async logIn(request: LoginRequest) : Promise<LoginResponse> {
        const result: LoginResponse = await this.axios.post('/SignIn', request)

        if (result.OperationResult === 1)
            return result

        window.sessionStorage.setItem('token', result.JWTToken)
        window.sessionStorage.setItem('username', result.Username)

        this.headers['Authorization'] = `Bearer ${result.JWTToken}`

        return result
    }

    async signUp(request: SignupRequest) : Promise<SignupResponse> {
        return this.axios.post('/SignUp', request)
    }

    async getUsers(page: number = 0) : Promise<User[]> {
        return this.axios.get(`/api/Users/GetUsers/${page}`)
    }

    async getUserByStatus(status: string) : Promise<User[]>{
        return this.axios.get(`/api/Users/GetUsersByStatus/${status}`)

    }

    async getStatuses() : Promise<Status[]>{
        return this.axios.get('/api/Users/Statuses')
    }

    async updateStatus(request: UpdateStatusRequest) : Promise<UpdateStatusResponse> {
        return this.axios.post('/api/Users/ChangeUserStatus', request)
    }

}

export const instance: ApiClient = new ApiClient(process.env.REACT_APP_API_BASE_URL)