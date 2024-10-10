import {atom} from "jotai";
import {CreatePaper} from "../models/CreatePaper.ts";

export const create_new_paper_atom = atom<CreatePaper>({
    name: "",
    price: 0,
    stock: 0
});