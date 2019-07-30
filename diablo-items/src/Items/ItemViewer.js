import React, { FunctionComponent } from "react";
import { MDBDataTable } from 'mdbreact';
import { orderBy } from 'lodash'
import * as style from './ItemViewer.css'

class ItemViewer extends React.Component
{
    constructor(props) {
        super(props);
        console.log(this.props);
    }

    render()
    {
        const itemData = this.props.Items.map(function(item) {
            return {
                LevelRequired: item.LevelRequired,
                Name: item.Name,
                Category: item.Category,
                SubCategory: item.SubCategory,
                Properties:
                    orderBy(item.Properties, ['Minimum'],['desc'])
                    .map(property => {

                    var value = property.Minimum == property.Maximum ? `${property.Minimum}` :
                                `[${property.Minimum}-${property.Maximum}]`;

                    return <div  className="diablo-attribute">+{value} {property.Name}</div>
                })

            }
        });

        const data =
        {
            columns: [
                {
                    label: 'Level Required',
                    field: 'LevelRequired',
                    sort: 'asc',
                    width: 50
                },
                {
                    label: 'Name',
                    field: 'Name',
                    sort: 'asc',
                    width: 90
                },
                {
                    label: 'Category',
                    field: 'Category',
                    sort: 'asc',
                    width: 50
                },
                {
                    label: 'Sub Category',
                    field: 'SubCategory',
                    sort: 'asc',
                    width: 80
                },
                {
                    label: 'Properties',
                    field: 'Properties',
                    width: 200
                },
            ],
            rows : itemData
        }
        return (<MDBDataTable striped bordered small data={data} />
    );
    }
};

export default ItemViewer;