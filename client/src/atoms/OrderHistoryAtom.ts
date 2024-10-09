import {atom} from "jotai";
import {FullOrder} from "../models/FullOrder.ts";

export const order_history_atom = atom<FullOrder[]>([])