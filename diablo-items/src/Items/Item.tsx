interface Item
{
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

// Utilisation des variables et des fichiers de configuration différents en fonction de l'environnement. (API URL par exemple) : https://serverless-stack.com/chapters/environments-in-create-react-app.html