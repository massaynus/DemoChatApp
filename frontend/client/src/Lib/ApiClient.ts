import axios, { Axios, AxiosRequestHeaders } from "axios";
import { LoginRequest, LoginResponse } from "../Types/Login";
import { SignupRequest, SignupResponse } from "../Types/SignUp";
import { Status, UpdateStatusRequest, UpdateStatusResponse } from "../Types/Status";
import { User } from "../Types/User";

export default class ApiClient {
    private headers: AxiosRequestHeaders;
    private axios: Axios;

    constructor (apiHost: string = 'http://localhost:5002') {
        this.headers = {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${window.sessionStorage.getItem('token') || ''}`
        }

        this.axios = new axios.Axios({
            baseURL: apiHost,
            headers: this.headers,
            responseType: 'json',
            transformResponse: body => JSON.parse(body)
        })

        console.log(`Api Base url: ${apiHost}`)
    }

    async logIn(request: LoginRequest) : Promise<LoginResponse> {
        const response = await this.axios.post('/SignIn', JSON.stringify(request))
        const result: LoginResponse = response.data

        if (result.operationResult === 1)
            return result

        console.log(result)
        window.sessionStorage.setItem('token', result.jwtToken)
        window.sessionStorage.setItem('username', result.username)

        this.headers['Authorization'] = `Bearer ${result.jwtToken}`

        return result
    }

    async signUp(request: SignupRequest) : Promise<SignupResponse> {
        const response = await  this.axios.post('/SignUp', JSON.stringify(request))
        return response.data
    }

    async getUsers(page: number = 0) : Promise<User[]> {
        const response = await  this.axios.get(`/api/Users/GetUsers/${page}`)
        return response.data
    }

    async getUserByStatus(status: string) : Promise<User[]>{
        const response = await  this.axios.get(`/api/Users/GetUsersByStatus/${status}`)
        return response.data

    }

    async getStatuses() : Promise<Status[]>{
        const response = await  this.axios.get('/api/Users/Statuses')
        return response.data
    }

    async updateStatus(request: UpdateStatusRequest) : Promise<UpdateStatusResponse> {
        const response = await  this.axios.post('/api/Users/ChangeUserStatus', JSON.stringify(request))
        return response.data
    }

}

export const instance: ApiClient = new ApiClient(process.env.REACT_APP_API_BASE_URL)