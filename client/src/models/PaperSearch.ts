import {Pagination} from "./Pagination.ts";

export interface PaperSearch {
    maxPrice: number
    minPrice: number
    minStock: number
    showDiscontinued: boolean
    nameSearchQuery: string
    paginationDto: Pagination
}