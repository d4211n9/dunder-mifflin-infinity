import {atom} from "jotai";
import {OrderEntryWithoutOrderId} from "../models/OrderEntryWithoutOrderId.ts";

export const order_entry_without_order_id_atom = atom<OrderEntryWithoutOrderId>({
    productId: 0,
    quantity: 0
});