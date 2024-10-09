import {useParams} from "react-router-dom";
import {base_api_route} from "../helper/ApiHelper.ts";
import {Paper} from "../models/Paper.ts";
import toast, {Toaster} from "react-hot-toast";
import {useAtom} from "jotai";
import {paper_atom} from "../atoms/PaperAtom.ts";
import {useEffect} from "react";

export default function PaperComponent() {
    const base_component_route = 'paper';
    const params = useParams();
    let paperId = params['paperId'] as Number;
    const [paper, setPaper] = useAtom(paper_atom);

    useEffect(() => {
        GetPaperById(paperId).then(data => {
            let paperData = data as Paper;

            let paperCopy = {...paper};
            paperCopy.id = paperData.id;
            paperCopy.stock = paperData.stock;
            paperCopy.name = paperData.name;
            paperCopy.price = paperData.price;
            paperCopy.discontinued = paperData.discontinued;
            paperCopy.properties = paperData.properties ? paperData.properties : [];

            setPaper(paperCopy);
        });
    }, []);

    return <>
        <div className="card bg-base-100 w-96 shadow-xl">
            <div className="card-body">
                <form onSubmit={OnSubmit}>
                    <h2 className="card-title justify-center">{paper.name}</h2>
                    <p>ID: {paper.id}</p>
                    <label>Stock:</label>
                    <input type="text" placeholder="Type here" className="input w-full max-w-xs"
                           value={paper.stock} onInput={OnStockInput} />
                    <p>Price: {paper.price}</p>
                    <label>Discontinued:</label>
                    <input type="checkbox" className="toggle"
                           checked={paper.discontinued} onChange={OnDiscontinuedInput} />
                    <div className="overflow-x-auto">
                        <table className="table">
                            {/* head */}
                            <thead>
                            <tr>
                                <th>Property ID</th>
                                <th>Property Name</th>
                            </tr>
                            </thead>
                            <tbody>
                            {paper.properties.map(property => (
                                <tr>
                                    <th>{property.id}</th>
                                    <th>{property.propertyName}</th>
                                </tr>
                            ))}
                            </tbody>
                        </table>
                    </div>
                    <div className="card-actions justify-center">
                        <button className="btn btn-primary" type="submit">Update</button>
                    </div>
                </form>
            </div>
        </div>

        <Toaster />
    </>

    async function OnSubmit(event) {
        event.preventDefault();

        await fetch(base_api_route + base_component_route + '/discontinue', {
            method: 'PUT',
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify({
                discontinue: paper.discontinued,
                paperId: paper.id
            })
        })
            .catch(e => toast.error('Failed to update paper'));

        await fetch(base_api_route + base_component_route + '/stock', {
            method: 'PUT',
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify({
                changedStock: paper.stock,
                paperId: paper.id
            })
        })
            .catch(e => toast.error('Failed to update Paper'));

        toast.success('Successfully updated paper');
    }

    function OnStockInput(event) {
        let paperCopy = {...paper}
        paperCopy.stock = event.target.value;

        setPaper(paperCopy);
    }

    function OnDiscontinuedInput(event) {
        let paperCopy = {...paper}
        paperCopy.discontinued = !paperCopy.discontinued;

        setPaper(paperCopy);
    }

    async function GetPaperById(paperId: number) {
        let data = await fetch(base_api_route + base_component_route + '/' + paperId)
            .then(response => response.json())
            .then((data: Paper) => data)
            .catch(e => toast.error("Failed to get paper"));

        return data;
    }
}