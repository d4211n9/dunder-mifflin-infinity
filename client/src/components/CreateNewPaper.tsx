import {useAtom} from "jotai";
import {create_new_paper_atom} from "../atoms/CreateNewPaperAtom.ts";
import {CreatePaper} from "../models/CreatePaper.ts";
import {Paper} from "../models/Paper.ts";
import toast from "react-hot-toast";
import {base_api_route} from "../helper/ApiHelper.ts";

export default function CreateNewPaper() {
    const base_component_route: string = 'paper/create';
    const [newPaper, setNewPaper] = useAtom(create_new_paper_atom);

    return <>
        <form className="flex flex-col justify-center items-center" onSubmit={OnFormSubmit}>
            <label>Name</label>
            <input type="text" required placeholder="Name" className="input input-bordered w-full max-w-xs"
                   value={newPaper.name} onInput={(event) => OnNameInput(event)} />

            <label>Price</label>
            <input type="number" required placeholder="Price" className="input input-bordered w-full max-w-xs"
                   value={newPaper.price} onInput={(event) => OnPriceInput(event)} />
            <label>Stock</label>
            <input type="number" required placeholder="Stock" className="input input-bordered w-full max-w-xs"
                   value={newPaper.stock} onInput={(event) => OnStockInput(event)} />

            <input className="btn btn-primary" type="submit" value="Create" />
        </form>
    </>


    async function OnFormSubmit(event) {
        event.preventDefault();

        let data: Paper | void = await fetch(base_api_route + base_component_route, {
            method: 'POST',
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify({
                name: String(newPaper.name),
                stock: Number(newPaper.stock),
                price: Number(newPaper.price)
            })
        })
            .then(response => response.json()
                .then((value: Paper) =>
                    value))
            .catch(error => {
                toast.error('Failed to create paper')
            });

        return data;
    }

    function OnNameInput(event) {
        let newPaperCopy: CreatePaper = {...newPaper};
        newPaperCopy.name = event.target.value;

        setNewPaper(newPaperCopy);
    }

    function OnPriceInput(event) {
        let newPaperCopy: CreatePaper = {...newPaper};
        newPaperCopy.price = event.target.value;

        setNewPaper(newPaperCopy);
    }

    function OnStockInput(event) {
        let newPaperCopy: CreatePaper = {...newPaper};
        newPaperCopy.stock = event.target.value;

        setNewPaper(newPaperCopy);
    }
}