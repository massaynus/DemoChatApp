import { atom, RecoilState } from "recoil";
import { Status } from "../Types/Status";
import { User } from "../Types/User";

export const JWTAtom : RecoilState<string | null> = atom({
    key: 'token',
    default: window.sessionStorage.getItem('token')
})

export const UserAtom : RecoilState<User> = atom({
    key: 'user',
    default: {
        username: window.sessionStorage.getItem('username')
    } as User
})

export const UsersAtom : RecoilState<User[]> = atom({
    key: 'users',
    default: [] as User[]
})

export const StatusesAtom : RecoilState<Status[]> = atom({
    key: 'statuses',
    default: [] as Status[]
})