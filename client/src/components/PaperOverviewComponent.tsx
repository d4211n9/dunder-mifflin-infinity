import {useAtom} from "jotai";
import {paper_overview_atom} from "../atoms/PaperOverviewAtom.ts";
import {PaperSearch} from "../models/PaperSearch.ts";
import {base_api_route} from "../helper/ApiHelper.ts";
import {SelectionWithPagination} from "../models/SelectionWithPagination.ts";
import {FullOrder} from "../models/FullOrder.ts";
import toast from "react-hot-toast";
import {Paper} from "../models/Paper.ts";
import {useEffect} from "react";
import {Params, useParams, useSearchParams} from "react-router-dom";
import {Pagination} from "../models/Pagination.ts";
import CreateNewPaper from "./CreateNewPaper.tsx";

export default function PaperOverviewComponent() {
    const base_component_route: string = 'paper';
    const [papers, setPapers] = useAtom(paper_overview_atom);
    const [searchParams, setSearchParams] = useSearchParams();
    let paperSearch: PaperSearch = RetrievePaperSearchFromSearchParams();
    let papersSortedAsc = false;

    useEffect(() => {
        GetPapers(paperSearch)
            .then(data => {
                let papersCopy = [...papers];
                papersCopy
                    .splice(0, papersCopy.length, ...(data as SelectionWithPagination<Paper>).selection);

                papersCopy.sort(paper => paper.id);
                setPapers(papersCopy);
            })
    }, []);

    return <>
        <div className="overflow-x-auto">
            <table className="table">
                <thead>
                <tr>
                    <th onClick={SortPapersById}>ID</th>
                    <th>Name</th>
                    <th>Discontinued</th>
                    <th>Price</th>
                    <th>Stock</th>
                    <th>Properties</th>
                </tr>
                </thead>
                <tbody>
                {papers.map(paper => (
                    <tr>
                        <th>{paper.id}</th>
                        <th>{paper.name}</th>
                        <th>{String(paper.discontinued)}</th>
                        <th>{paper.price}</th>
                        <th>{paper.stock}</th>
                        <th>
                            <ul>
                                {paper.properties.map(property => (
                                    <li>
                                        <p>Id: {property.id}</p>
                                        <p>Name{property.propertyName}</p>
                                    </li>
                                ))}
                            </ul>
                        </th>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    </>


    function SortPapersById() {
        let papersCopy = [...papers];
        papersCopy.sort(paper => paper.id);

        setPapers(papersCopy);
    }

    async function GetPapers(paperSearch: PaperSearch) {
        const get_papers_route: string = base_api_route + base_component_route;

        let data: SelectionWithPagination<Paper> | void = await fetch(get_papers_route, {
            method: 'POST',
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify({
                maxPrice: Number(paperSearch.maxPrice),
                minNumber: Number(paperSearch.minPrice),
                minStock: Number(paperSearch.minStock),
                nameSearchQuery: String(paperSearch.nameSearchQuery),
                showDiscontinued: Boolean(paperSearch.showDiscontinued),
                paginationDto: {
                    pageNumber: Number(paperSearch.paginationDto.pageNumber),
                    pageSize: Number(paperSearch.paginationDto.pageSize)
                }
            })
        })
            .then(response => response.json()
                .then((value: SelectionWithPagination<Paper>) =>
                    value)
                .catch(error => {
                    toast.error('Failed to get papers')
                }));

        return data;
    }

    function RetrievePaperSearchFromSearchParams() {
        return new class implements PaperSearch {
            maxPrice: number = searchParams.get('maxPrice') as Number;
            minPrice: number = searchParams.get('minPrice') as Number;
            minStock: number = searchParams.get('minStock') as Number;
            nameSearchQuery: string = searchParams.get('nameSearchQuery') as string;
            paginationDto: Pagination = new class implements Pagination {
                pageNumber: number = searchParams.get('pageNumber') as Number;
                pageSize: number = searchParams.get('pageSize') as Number;
            };
            showDiscontinued: boolean = searchParams.get('showDiscontinued') as boolean;
        }
    }
}