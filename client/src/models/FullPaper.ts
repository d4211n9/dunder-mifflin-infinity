import {Property} from "./SelectionWithPagination.ts";
import {FullOrderEntry} from "./FullOrderEntry.ts";

export interface FullPaper {
    id: number
    name: string
    discontinued: boolean
    stock: number
    price: number
    orderEntries: FullOrderEntry[]
    properties: Property[]
}