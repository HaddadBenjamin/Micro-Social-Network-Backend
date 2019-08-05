import React from "react";
import { orderBy } from 'lodash'
import * as style from './ItemViewer.css'
import {
    MDBDataTable,
    MDBContainer,
    MDBView,
    MDBMask,
    MDBRow,
    MDBCol
} from 'mdbreact';
import {connect} from "react-redux";

class ItemViewer extends React.Component {
    constructor(props) {
        super(props);
    }

    render()
    {
        const data =
        {
            columns:
            [
                {
                    label: 'Name',
                    field: 'Name',
                    sort: 'asc',
                    width: 150
                },
                {
                    label: 'Stats',
                    field: 'Stats',
                }
            ]
        };
        // 3) revoir l’affichage : fond sombre
        // rendu en flexbox
        var rows =
            orderBy(this.props.Items, ['Name'])
            .map(function(item)
            {
                const attributes =
                    orderBy(item.Properties, ['Minimum'],['desc'])
                    .map(property =>
                    {
                        var value = property.Par > 0 ? property.Par : (property.Minimum === property.Maximum ? `${property.Minimum}` :
                            `${Math.min(property.Minimum, property.Maximum)}-${Math.max(property.Minimum, property.Maximum)}`)
                        var isPercent = (property.IsPercent ? '%' : '')

                        return <div className="diablo-attribute">+{value}{isPercent} {property.Name}</div>
                    });

                var defense  = item.MaximumDefenseMinimum === item.MaximumDefenseMaximum ? item.MaximumDefenseMinimum :`${item.MaximumDefenseMinimum}-${item.MaximumDefenseMaximum}`;

                var itemFormatted =   <>
                    <div className="item" style={style}>

                        {/* OBJECT IMAGE */ }
                        <div className="unique"> {/*qualité lié à la qualité e l'objet et revoit la couleur */}
                            {item.Name} <br/>
                            {item.Type}
                        </div>
                        <div style={{color : "white"}}>
                            {item.MaximumDefenseMinimum > 0 ? <div>Defense : <span className="diablo-attribute">{defense}</span></div> : ''}
                            {item.MinimumOneHandedDamageMinimum > 0 ? <div>One-Hand Damage : <span className="diablo-attribute">{item.MinimumOneHandedDamageMinimum}-{item.MaximumOneHandedDamageMinimum} to {item.MinimumOneHandedDamageMaximum}-{item.MaximumOneHandedDamageMaximum}</span></div> : ''}
                            {item.MinimumTwoHandedDamageMinimum > 0 ? <div>Two-Hand Damage : <span className="diablo-attribute">{item.MinimumTwoHandedDamageMinimum}-{item.MaximumTwoHandedDamageMinimum} to {item.MinimumTwoHandedDamageMaximum}-{item.MaximumTwoHandedDamageMaximum}</span></div> : ''}
                        </div>
                        <div className="required-attribute">
                            {item.StrengthRequired > 0 ? <div>Required Strength : {item.StrengthRequired} </div> : ''}
                            {item.DexterityRequired > 0 ? <div>Required Dexterity : {item.DexterityRequired} </div> : ''}
                            {item.LevelRequired > 0 ? <div>Required Level : {item.LevelRequired} </div> : ''}
                        </div>
                        <div style={{color : "#4545a4"}}>{/* rvoir la couleur */}
                            {attributes}
                        </div>
                    </div>
                </>;

                return {
                    Name : item.Name,
                    Item : itemFormatted,
                }
       });

        data.rows = rows.map(function(item) {
            return {
                'Name': item.Name,
                'Stats': item.Item
            };
        });

        return (
            <>
                <div id="item-filter-view">
                    <MDBView >
                        <MDBMask className="d-flex justify-content-center align-items-center gradient">
                            <MDBContainer>
                                <MDBRow>
                                    <MDBCol>
            <MDBDataTable className="item" style={style}
            data={data}
            entries={3}/>
                                        <MDBCol/>
                                    </MDBCol>
                                </MDBRow>
                            </MDBContainer>
                        </MDBMask>
                    </MDBView>
                </div>
            </>
                );

    }
}
const mapStateToProps = function (state)
{
    return {
        Items: state.searchItems.items
    };
};

export default connect(mapStateToProps)(ItemViewer)
