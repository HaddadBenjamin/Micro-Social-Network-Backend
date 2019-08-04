import Item from './Item'
import React, {Component} from 'react';
import ItemViewer from './ItemViewer'
import axios from 'axios'

interface Props {

}
interface State
{
    Items : Item[]
}

class Items extends Component<Props, State>
{
    public constructor(props : Props)
    {
        super(props);

        this.state =  {
            Items : []
        };
    }

    componentDidMount()
    {
        // TODO :
        // - API URL should be stored later in a configuration file dedicated to his environment.
        // - Update attributes names / images when @Ascended#1962  will be ready to sent them.
        // - Display several item filtres by difficulty (axe / sword / body / helm / etc..) in order to filter items.
        // - Display a page only to search items by toooons of different custom filters.
        // - Display a better background transparent in order to make it looks more diablo.
        // - Display all the items in a flexbox and paginate them (flexbox generator) ou sinon utiliser mon concept d'affichge de mes projets de mon portfolio mais avec de la pagination
        // - Les boutons de choix de difficultés ne sont jamais appelés
        // - Level looks bad, search why and fix it

        axios.get<Item[]>('http://localhost:56205/api/v1/Items/getalluniques')
             .then(response =>
             {
                 this.setState({ Items : response.data });
             })
    }

    render()
    {
        const {Items} = this.state;

        return (<ItemViewer Items={Items}></ItemViewer>)
    }
}

export default Items;