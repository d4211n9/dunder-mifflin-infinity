import {Property} from "./Property.ts";

export interface Paper {
    id: number
    name: string
    discontinued: boolean
    stock: number
    price: number
    properties: Property[]
}
