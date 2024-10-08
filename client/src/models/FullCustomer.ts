import {FullOrder} from "./FullOrder.ts";

export interface FullCustomer {
    id: number
    name: string
    address: string
    phone: string
    email: string
    orders: FullOrder[]
}