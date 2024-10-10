import {useAtom} from "jotai";
import {property_name_atom} from "../atoms/PropertyNameAtom.ts";
import {base_api_route} from "../helper/ApiHelper.ts";
import toast from "react-hot-toast";

export default function CreatePaperPropertyComponent() {
    const base_component_route = 'paper/property/create';
    const [propertyName, setPropertyName] = useAtom(property_name_atom);

    return <>
        <div>
            <form className="flex flex-col justify-center items-center" onSubmit={CreateNewProperty}>
                <label>Property Name</label>
                <input type="text" className="input input-bordered w-full max-w-xs"
                       value={propertyName} onInput={OnPropertyNameInput} />
                <input className="btn btn-primary" type="submit" value="Create" />
            </form>
        </div>
    </>

    async function CreateNewProperty(event) {
        event.preventDefault();

        let response = await fetch(base_api_route + base_component_route + '/' + propertyName, {
            method: 'POST'
        })
            .catch(e => toast.error('Failed to create new property'));

        if ((response as Response).ok)
            toast.success('Successfully added property');
    }

    function OnPropertyNameInput(event) {
        let propertyNameCopy = propertyName.slice(0);
        propertyNameCopy = event.target.value;

        setPropertyName(propertyNameCopy);
    }
}