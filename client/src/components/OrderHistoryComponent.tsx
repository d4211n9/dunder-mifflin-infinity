import {base_api_route} from "../helper/ApiHelper.ts";
import {SelectionWithPagination} from "../models/SelectionWithPagination.ts";
import {useEffect, useState} from "react";
import toast from "react-hot-toast";
import {FullOrder} from "../models/FullOrder.ts";
import {useParams, useSearchParams} from "react-router-dom";
import {useAtom} from "jotai";
import {order_history_atom} from "../atoms/OrderHistoryAtom.ts";

export default function OrderHistoryComponent() {
    const [searchParams, setSearchParams] = useSearchParams();
    const params = useParams();
    const base_component_route: string = 'order';
    const [customerOrders, setCustomerOrders] = useAtom(order_history_atom);
    const customerId = params['customerId'] as Number;
    const pageNumber: number = searchParams.get('pageNumber') as Number;
    const pageSize: number = searchParams.get('pageSize') as Number

    useEffect(() => {
        OrdersForCustomer(customerId, pageNumber, pageSize)
            .then(data => {
                let ordersCopy = [...customerOrders];
                ordersCopy
                    .splice(0, ordersCopy.length, ...(data as SelectionWithPagination<FullOrder>).selection);

                setCustomerOrders(ordersCopy);
            })
    }, []);
    return <>
        <div className="overflow-x-auto">
            <table className="table">
                <thead>
                <tr>
                    <th>ID</th>
                    <th>Order Date</th>
                    <th>Delivery Date</th>
                    <th>Status</th>
                    <th>Total Amount</th>
                </tr>
                </thead>
                <tbody>
                    {customerOrders.map(order => (
                        <tr>
                            <th>{order.id}</th>
                            <th>{order.orderDate}</th>
                            <th>{order.deliveryDate}</th>
                            <th>{order.status}</th>
                            <th>{order.totalAmount}</th>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    </>

    async function OrdersForCustomer(customerId: number, pageNumber: number, pageSize: number) {
        const get_orders_for_customer_route: string = base_api_route + base_component_route + `/customer?customerId=${customerId}&paginationDto.pageNumber=${pageNumber}&paginationDto.pageSize=${pageSize}`;

        let data: SelectionWithPagination<FullOrder> | void = await fetch(get_orders_for_customer_route)
            .then(response => response.json()
                .then((value: SelectionWithPagination<FullOrder>) =>
                    value)
                .catch(error => {
                    toast.error('Failed to get orders')
                }));

        return data;
    }
}

