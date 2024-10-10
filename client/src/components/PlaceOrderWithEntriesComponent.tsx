import {useParams} from "react-router-dom";
import PaperOverviewComponent from "./PaperOverviewComponent.tsx";
import {useAtom} from "jotai";
import {place_order_entries_atom} from "../atoms/PlaceOrderEntryAtom.ts";
import {base_api_route} from "../helper/ApiHelper.ts";
import toast from "react-hot-toast";
import {order_entry_without_order_id_atom} from "../atoms/OrderEntryWithoutOrderIdAtom.ts";

export default function PlaceOrderWithEntriesComponent() {
    const base_component_route = 'order/create';
    const params = useParams();
    const [orderEntries, setOrderEntries] = useAtom(place_order_entries_atom);
    const customerId: number = params['customerId'] as Number;
    const [entry, setEntry] = useAtom(order_entry_without_order_id_atom);

    return <>
        <div>
            <form className="flex flex-col justify-center content-center" onSubmit={OnSubmitOrderEntry}>
                <label>Paper Id</label>
                <input type="number" placeholder="Type here" className="input input-bordered w-full max-w-xs"
                    value={entry.productId} onInput={OnPaperIdInput} />

                <label>Quantity</label>
                <input type="number" placeholder="Type here" className="input input-bordered w-full max-w-xs"
                       value={entry.quantity} onInput={OnQuantityInput} />

                <input className="btn btn-primary" type="submit" value="Place Entry" />
            </form>
        </div>

        <div className="overflow-x-auto">
            <table className="table">
                <thead>
                <tr>
                    <th>Paper Id</th>
                    <th>Quantity</th>
                </tr>
                </thead>
                <tbody>
                    {orderEntries.map(entry => (
                        <tr>
                            <th>{entry.productId}</th>
                            <th>{entry.quantity}</th>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>

        <div>
            <button className="btn btn-primary" onClick={PlaceOrderWithEntries}>Place Order</button>
        </div>

        <PaperOverviewComponent />
    </>

    async function PlaceOrderWithEntries() {
            let orderEntryDtos: object[] = [];
            orderEntries.forEach(entry => {
                orderEntryDtos.push({
                    productId: entry.productId,
                    quantity: entry.quantity
                });
            });


           let response = await fetch(base_api_route + base_component_route, {
               method: 'POST',
               headers: {
                   'content-type': 'application/json'
               },
               body: JSON.stringify({
                   customerId: Number(customerId),
                   orderEntryDtos
               })
           })
               .catch(e => toast.error('Failed to place order'));

           if ((response as Response).ok)
               toast.success('Successfully placed order');
    }

    function OnSubmitOrderEntry(event) {
            event.preventDefault();

            let orderEntriesCopy = [...orderEntries];
            orderEntriesCopy.push({
                productId: Number(entry.productId),
                quantity: Number(entry.quantity)
            });

            setOrderEntries(orderEntriesCopy);
    }

    function OnPaperIdInput(event) {
            let entryCopy = {...entry};
            entryCopy.productId = event.target.value;

            setEntry(entryCopy);
    }

    function OnQuantityInput(event) {
        let entryCopy = {...entry};
        entryCopy.quantity = event.target.value;

        setEntry(entryCopy);
    }
}