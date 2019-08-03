import React from "react";
import SearchItemDto, {ItemSubCategory} from "./SearchItemDto";

interface Props {
    search : SearchItemDto,
}
interface State
{
}
class SearchItem extends React.Component<Props, State>
{
    public constructor(props : Props)
    {
        super(props);

        this.onClickNormalUnique = this.onClickNormalUnique.bind(this);
        this.onClickExceptionalUnique = this.onClickExceptionalUnique.bind(this);
        this.onClickEliteUnique = this.onClickEliteUnique.bind(this);
        this.onClickLegendaryUnique = this.onClickLegendaryUnique.bind(this);

        this.onClickBodyArmor = this.onClickBodyArmor.bind(this);
        this.onClickShield = this.onClickShield.bind(this);
        this.onClickGlove = this.onClickGlove.bind(this);
        this.onClickShoes = this.onClickShoes.bind(this);
        this.onClickHelm = this.onClickHelm.bind(this);
    }

    // Item difficulty :
   public onClickNormalUnique = ()  => this.props.search.MaximumLevelRequired = 30;
   public onClickExceptionalUnique = ()  => this.props.search.MaximumLevelRequired = 60;
   public onClickEliteUnique = ()  => this.props.search.MaximumLevelRequired = 90;
   public onClickLegendaryUnique = ()  => this.props.search.MaximumLevelRequired = Math.max();

    // Armors :
    public onClickBodyArmor = ()  => this.props.search.SubCategories = [ ItemSubCategory.Torso ];
    public onClickShield = ()  => this.props.search.SubCategories = [ ItemSubCategory.Offhand ];
    public onClickGlove = ()  => this.props.search.SubCategories = [ ItemSubCategory.Hands ];
    public onClickShoes = ()  => this.props.search.SubCategories = [ ItemSubCategory.Feet ];
    public onClickHelm = ()  => this.props.search.SubCategories = [ ItemSubCategory.Head ];
    //public onClickBelt = ()  => this.props.search.SubCategories = [ ItemSubCategory. ];

    render()
    {
        return ('x');
    }
}

export default SearchItem;