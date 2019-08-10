import {store} from "../../src/store/store";
import axios from 'axios'
import config from './config'

class Api
{
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
        return `${config.apiUrl}/${endpoint}/${queryParameters ? '?' + queryParameters : ''}`;
    }
}

const api = new Api();

export default api;
