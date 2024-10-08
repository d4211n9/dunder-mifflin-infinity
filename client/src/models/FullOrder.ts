import {OrderEntry} from "./SelectionWithPagination.ts";
import {FullCustomer} from "./FullCustomer.ts";

export interface FullOrder {
    id: number
    orderDate: string
    deliveryDate: string
    status: string
    totalAmount: number
    customerId: number
    customer: FullCustomer
    orderEntries: OrderEntry[]
}