import React from "react";
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
        return (orderBy(this.props.Items, ['Name']).map(function(item)
        {
            const attributes =
                orderBy(item.Properties, ['Minimum'],['desc'])
                .map(property =>
                {
                    var value = property.Par > 0 ? property.Par : (property.Minimum === property.Maximum ? `${property.Minimum}` :
                        `[${Math.min(property.Minimum, property.Maximum)}-${Math.max(property.Minimum, property.Maximum)}]`)
                    var isPercent = (property.IsPercent ? '%' : '')

                    return <div className="diablo-attribute">+{value}{isPercent} {property.Name}</div>
                });

            var defense  = item.MaximumDefenseMinimum === item.MaximumDefenseMaximum ? item.MaximumDefenseMinimum :`${item.MaximumDefenseMinimum}-${item.MaximumDefenseMaximum}`;

            return(
            <>
           <div className="item" style={style}>

           OBJECT IMAGE
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
            </>)        }))
    }
}
export default ItemViewer;