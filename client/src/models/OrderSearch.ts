import {Pagination} from "./Pagination.ts";
import {OrderTimeFrame} from "./OrderTimeFrame.ts";
import {DeliveryTimeFrame} from "./DeliveryTimeFrame.ts";

export interface OrderSearch {
    maxAmount: number
    minAmount: number
    deliveryTimeFrameDto: DeliveryTimeFrame
    orderTimeFrameDto: OrderTimeFrame
    orderStatus: string
    paginationDto: Pagination
}
