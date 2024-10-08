import {Paper} from "./SelectionWithPagination.ts";
import {FullOrder} from "./FullOrder.ts";

export interface FullOrderEntry {
    id: number
    quantity: number
    productId: number
    orderId: number
    order: FullOrder
    product: Paper
}