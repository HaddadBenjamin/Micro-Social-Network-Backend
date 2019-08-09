import React, {Component} from 'react';
import ItemViewer from './ItemViewer'


interface Props {
}
interface State
{
//
}

class Items extends Component<Props, State>
{
    public constructor(props : Props)
    {
        super(props);
    }

    componentDidMount()
    {
        // TODO :
        // - API URL should be stored later in a configuration file dedicated to his environment.
        // - Update attributes names / images when @Ascended#1962  will be ready to sent them.
        // - Display several item filtres by difficulty (axe / sword / body / helm / etc..) in order to filter items.
        // - Display a page only to search items by toooons of different custom filters.
        // - Level looks bad, search why and fix it
    }

    render()
    {
        return (<ItemViewer Items={[]}></ItemViewer>)
    }
}

export default Items;

