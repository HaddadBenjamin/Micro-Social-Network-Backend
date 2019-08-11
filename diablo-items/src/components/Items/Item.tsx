interface Item
{
    Name : string,
    Quality : string,
    Category : string,
    SubCategory : string,
    Type : string,
    LevelRequired : number,
    Level : number,
    Properties : ItemProperty[],

    // Relative to defense :
    MinimumDefenseMinimum  : number,
    MaximumDefenseMinimum  : number,
    MinimumDefenseMaximum  : number,
    MaximumDefenseMaximum  : number,

    // Relative to weapons :
    MinimumOneHandedDamageMinimum  : number,
    MaximumOneHandedDamageMinimum  : number,
    MinimumTwoHandedDamageMinimum  : number,
    MaximumTwoHandedDamageMinimum  : number,
    MinimumOneHandedDamageMaximum  : number,
    MaximumOneHandedDamageMaximum  : number,
    MinimumTwoHandedDamageMaximum  : number,
    MaximumTwoHandedDamageMaximum  : number,

    // Stat required.
    StrengthRequired  : number,
    DexterityRequired : number,
}



interface ItemProperty
{
    Name : string,
    FormattedName : string,
    Par : number,
    Minimum : number,
    Maximum : number,
    IsPercent : boolean,
    Id : string,
    FirstCharacter : string,
    OrderIndex : number,
}

export default Item;

// Utilisation des variables et des fichiers de configuration différents en fonction de l'environnement. (API URL par exemple) : https://serverless-stack.com/chapters/environments-in-create-react-app.html