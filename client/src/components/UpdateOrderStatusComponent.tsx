import {useAtom} from "jotai";
import {update_order_status_atom} from "../atoms/UpdateOrderStatusAtom.ts";
import {base_api_route} from "../helper/ApiHelper.ts";
import toast from "react-hot-toast";
import {useParams} from "react-router-dom";

export default function UpdateOrderStatusComponent() {
    const params = useParams();
    const base_component_route = 'order/status';
    const [status, setStatus] = useAtom(update_order_status_atom);
    const orderId: number = params['orderId'] as Number;

    return <>
        <div>
            <form className="flex flex-col justify-center items-center" onSubmit={UpdateStatus}>
                <label>Status</label>
                <input type="text" className="input input-bordered w-full max-w-xs"
                       value={status} onInput={OnStatusInput} />
                <input className="btn btn-primary" type="submit" value="Create" />
            </form>
        </div>
    </>

    async function UpdateStatus(event) {
        event.preventDefault();

        let response = await fetch(base_api_route + base_component_route, {
            method: 'PUT',
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify({
                orderId: Number(orderId),
                updatedStatus: String(status)
            })
        })
            .catch(e => toast.error('Failed to update status'));

        if ((response as Response).ok)
            toast.success('Successfully updated status');
    }

    function OnStatusInput(event) {
        let statusCopy = status.slice(0);
        statusCopy = event.target.value;

        setStatus(statusCopy);
    }
}