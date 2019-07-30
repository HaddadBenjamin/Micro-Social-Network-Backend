interface Item
{
    id : string,
    Name : string,
    Quality : string,
    Category : string,
    SubCategory : string,
    LevelRequired : number,
    Level : number,
    Properties : ItemProperty[]
}

interface ItemProperty
{
    Name : string,
    Par : number,
    Minimum : number,
    Maximum : number,
    IsPercent : boolean,
}

export default Item;