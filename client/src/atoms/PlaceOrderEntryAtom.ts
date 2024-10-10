import {atom} from "jotai";
import {OrderEntryWithoutOrderId} from "../models/OrderEntryWithoutOrderId.ts";

export const place_order_entries_atom = atom<OrderEntryWithoutOrderId[]>([]);