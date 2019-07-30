import Item from './Item'
import React, { Component, useState  } from 'react';
import axios from 'axios';
import ItemViewer from './ItemViewer'
interface Props {}
interface State
{
    Items : Item[]
}

class Items extends Component<Props, State>
{
    public constructor(props : Props)
    {
        super(props);

        this.state =
        {
            Items : []
        };
    }

    componentDidMount()
    {
        // This URL will be stored later in a configuration file dedicated to his environment.
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
        //return (<ul>{items}</ul>)
    }
}

export default Items;