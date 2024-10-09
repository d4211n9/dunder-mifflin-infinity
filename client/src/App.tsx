import './App.css'
import OrderHistoryComponent from "./components/OrderHistoryComponent.tsx";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import PaperOverviewComponent from "./components/PaperOverviewComponent.tsx";
import CreateNewPaper from "./components/CreateNewPaper.tsx";
import PaperComponent from "./components/PaperComponent.tsx";
import {Toaster} from "react-hot-toast";
import CreatePaperPropertyComponent from "./components/CreatePaperPropertyComponent.tsx";
import UpdateOrderStatusComponent from "./components/UpdateOrderStatusComponent.tsx";

function App() {
    return <>
        <BrowserRouter>
            <Routes>
                <Route path='/order/history/:customerId' element={<OrderHistoryComponent />}/>
                <Route path='/paper/overview' element={<PaperOverviewComponent />}/>
                <Route path='/paper/create' element={<CreateNewPaper />}/>
                <Route path='/paper/:paperId' element={<PaperComponent />} />
                <Route path='/paper/property/create' element={<CreatePaperPropertyComponent />} />
                <Route path='/order/:orderId' element={<UpdateOrderStatusComponent />} />
            </Routes>
        </BrowserRouter>
        <Toaster />
    </>;
}

export default App
