import './App.css'
import OrderHistoryComponent from "./components/OrderHistoryComponent.tsx";
import {BrowserRouter, Route, Routes} from "react-router-dom";

function App() {
    return <>
        <BrowserRouter>
            <Routes>
                <Route path='/orderHistory/:customerId/:pageNumber/:pageSize' element={<OrderHistoryComponent />}/>
            </Routes>
        </BrowserRouter>
    </>;
}

export default App
