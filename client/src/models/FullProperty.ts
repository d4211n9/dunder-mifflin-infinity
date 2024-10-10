import {FullPaper} from "./FullPaper.ts";

export interface FullProperty {
    id: number
    propertyName: string
    papers: FullPaper[]
}