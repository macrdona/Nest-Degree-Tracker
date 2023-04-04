import { AxiosError } from "axios"

export type ErrorResponse = AxiosError<{
    statusCode?: number
    errors?: ErrorDescription[]
    message?: string
}>

export interface ErrorDescription {
    field?: string
    message?: string
}

export interface User {
    id: number,
    username: string,
    firstName: string,
    lastName: string
}