import React, { FunctionComponent } from "react";
import { MDBDataTable, MDBRow } from 'mdbreact';
import { orderBy } from 'lodash'
import * as style from './ItemViewer.css'

class ItemViewer extends React.Component {
    constructor(props) {
        super(props);
        console.log(this.props);
    }

    render()
    {
        // 3) revoir l’affichage : fond sombre
        // rendu en flexbox
        // - les propriétés de l’objet en bleu : utiliser isPercent
        return (orderBy(this.props.Items, ['Name']).map(function(item)
        {
            const attributes =
                orderBy(item.Properties, ['Minimum'],['desc'])
                .map(property =>
                {
                    var value = (property.Minimum == property.Maximum ? `${property.Minimum}` :
                        `[${property.Minimum}-${property.Maximum}]`)
                    var isPercent = (property.IsPercent ? '%' : '')

                    return <div  className="diablo-attribute">+{value}{isPercent} {property.Name}</div>
                });

            return(
            <>
           <div className="item">

           OBJECT IMAGE
            <div className="unique"> {/*qualité lié à la qualité e l'objet et revoit la couleur */}
          {item.Name} <br/>
          {item.Type} {/* Utiliser le type de l'objet plutôt */}
          </div>
          <div style={{color : "white"}}>{/*revoir la couleur*/}
                {item.MaximumDefenseMinimum > 0 ? <div>Defense : <span className="diablo-attribute">{item.MaximumDefenseMinimum}-{item.MaximumDefenseMaximum}</span></div> : ''}
                {item.MinimumOneHandedDamageMinimum > 0 ? <div>One-Hand Damage : <span className="diablo-attribute">{item.MinimumOneHandedDamageMinimum}-{item.MaximumOneHandedDamageMinimum} to {item.MinimumOneHandedDamageMaximum}-{item.MaximumOneHandedDamageMaximum}</span></div> : ''}
                {item.MinimumTwoHandedDamageMinimum > 0 ? <div>Two-Hand Damage : <span className="diablo-attribute">{item.MinimumTwoHandedDamageMinimum}-{item.MaximumTwoHandedDamageMinimum} to {item.MinimumTwoHandedDamageMaximum}-{item.MaximumTwoHandedDamageMaximum}</span></div> : ''}
            {/* - one-handed damage ou two-handed damage ou les deux : dommage avec le bonus de dommage compris (min damage, Max damage, pourcentage, d’image)
         - défense avec le bonus de défense (défense , defence pourcent )*/}
          </div>
          <div className="required-attribute">{/*  rvoir la couleur */}
            {/* - stats requis, level requis en rouge */ }
                {item.StrengthRequired > 0 ? <div>Required Strength : {item.StrengthRequired} </div> : ''}
                {item.DexterityRequired > 0 ? <div>Required Dexterity : {item.DexterityRequired} </div> : ''}
                {item.LevelRequired > 0 ? <div>Required Level : {item.LevelRequired} </div> : ''}
          </div>
          <div style={{color : "#4545a4"}}>{/* rvoir la couleur */}
          {attributes}
        </div>
        </div>
            </>)        }))
    }
}
export default ItemViewer;