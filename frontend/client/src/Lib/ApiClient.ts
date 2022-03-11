import axios, { Axios, AxiosRequestHeaders, AxiosResponse } from "axios";
import { LoginRequest, LoginResponse } from "../Types/Login";
import { SignupRequest, SignupResponse } from "../Types/SignUp";
import { Status, UpdateStatusRequest, UpdateStatusResponse } from "../Types/Status";
import { UserList } from "../Types/User";

export default class ApiClient {
    private headers: AxiosRequestHeaders;
    private axios: Axios;

    constructor(apiHost: string = 'http://localhost:5002') {
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

        console.debug(`Api Base url: ${apiHost}`)
    }

    async logIn(request: LoginRequest): Promise<LoginResponse> {
        const response = await this.axios.post('/SignIn', JSON.stringify(request))
        const result: LoginResponse = response.data

        if (result.operationResult === 1)
            return result

        console.debug(result)
        window.sessionStorage.setItem('token', result.jwtToken)
        window.sessionStorage.setItem('username', result.username)

        this.headers['Authorization'] = `Bearer ${result.jwtToken}`

        return result
    }

    async signUp(request: SignupRequest): Promise<SignupResponse> {
        const response = await this.axios.post('/SignUp', JSON.stringify(request))
        return this.validateReponse(response)
    }

    async getUsers(page: number = 0): Promise<UserList> {
        const response = await this.axios.get(`/api/Users/GetUsers/${page}`)
        return this.validateReponse(response)
    }

    async getUserByStatus(status: string): Promise<UserList> {
        const response = await this.axios.get(`/api/Users/GetUsersByStatus/${status}`)
        return this.validateReponse(response)
    }

    async getStatuses(): Promise<Status[]> {
        const response = await this.axios.get('/api/Users/Statuses')
        return this.validateReponse(response)
    }

    async updateStatus(request: UpdateStatusRequest): Promise<UpdateStatusResponse> {
        const response = await this.axios.put('/api/Users/ChangeUserStatus', JSON.stringify(request))
        return this.validateReponse(response)
    }

    private validateReponse<TReturn>(response: AxiosResponse): TReturn {
        if (response.status === 401) {
            window.sessionStorage.removeItem('token')
            window.location.href = '/'
        }

        return response.data as TReturn
    }

}

export const ApiClientInstance: ApiClient = new ApiClient(process.env.REACT_APP_API_BASE_URL)