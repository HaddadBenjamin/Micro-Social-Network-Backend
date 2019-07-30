import React, { PureComponent } from 'react'
import Item from './Item'

interface Props {}
interface State
{
    todos : Item[]
}

class Items extends PureComponent<Props, State>
{
    public constructor(props: Props)
    {
        super(props);

        // Bind de méthodes ?
    }
}