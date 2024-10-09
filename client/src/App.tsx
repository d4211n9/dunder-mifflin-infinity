import './App.css'
import OrderHistoryComponent from "./components/OrderHistoryComponent.tsx";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import PaperOverviewComponent from "./components/PaperOverviewComponent.tsx";
import CreateNewPaper from "./components/CreateNewPaper.tsx";

function App() {
    return <>
        <BrowserRouter>
            <Routes>
                <Route path='/orderHistory/:customerId' element={<OrderHistoryComponent />}/>
                <Route path='/paperOverview' element={<PaperOverviewComponent />}/>
                <Route path='/paper/create' element={<CreateNewPaper />}/>
            </Routes>
        </BrowserRouter>
    </>;
}

export default App
