import {atom} from "jotai";
import {Paper} from "../models/Paper.ts";

export const paper_atom = atom<Paper>({
    discontinued: false,
    id: 0,
    name: "",
    price: 0,
    properties: [],
    stock: 0
});