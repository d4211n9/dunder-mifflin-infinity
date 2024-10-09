import './App.css'
import OrderHistoryComponent from "./components/OrderHistoryComponent.tsx";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import PaperOverviewComponent from "./components/PaperOverviewComponent.tsx";

function App() {
    return <>
        <BrowserRouter>
            <Routes>
                <Route path='/orderHistory/:customerId/:pageNumber/:pageSize' element={<OrderHistoryComponent />}/>
                <Route path='/paperOverview' element={<PaperOverviewComponent />}/>
            </Routes>
        </BrowserRouter>
    </>;
}

export default App
