import { store } from "../../src/store/store";
import axios from 'axios'

export default class api
{
    // Get from variable environment / file different by environment.
    readonly apiUrl : string = 'http://localhost:56205/api/v1';

    public get<T>(endpoint : string, action : string, queryParameters? : string)
    {
        var url = this.getUrl(endpoint, queryParameters);

        axios.get<T>(url)
            .then(response =>
            {
                // typescript-fsa to be to handle success / fail normal behaviour
                store.dispatch(
                    {
                        type: action,
                        payload : response.data
                    });
            });

        console.log(url);
    }

    private getUrl(endpoint : string, queryParameters? : string) : string
    {
        return `${this.apiUrl}/${endpoint}/?${queryParameters ? '?' + queryParameters : ''}`;
    }
}
