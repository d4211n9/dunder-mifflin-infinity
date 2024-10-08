import {OrderEntryWithoutOrderId} from "./OrderEntryWithoutOrderId.ts";

export interface Root {
    customerId: number
    orderEntryDtos: OrderEntryWithoutOrderId[]
}