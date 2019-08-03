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

        this.onClickNormalUniques = this.onClickNormalUniques.bind(this);
        this.onClickExceptionalUniques = this.onClickExceptionalUniques.bind(this);
        this.onClickEliteUniques = this.onClickEliteUniques.bind(this);
        this.onClickLegendaryUniques = this.onClickLegendaryUniques.bind(this);

        this.onClickBodyArmors = this.onClickBodyArmors.bind(this);
        this.onClickShields = this.onClickShields.bind(this);
        this.onClickGloves = this.onClickGloves.bind(this);
        this.onClickShoes = this.onClickShoes.bind(this);
        this.onClickHelms = this.onClickHelms.bind(this);
        this.onClickBelts = this.onClickBelts.bind(this);

        this.onClickBows = this.onClickBows.bind(this);
        this.onClickCrossbows = this.onClickCrossbows.bind(this);
        this.onClickArrows = this.onClickArrows.bind(this);

        this.onClickStaffs = this.onClickStaffs.bind(this);
        this.onClickWands = this.onClickWands.bind(this);
        this.onClickOrbs = this.onClickOrbs.bind(this);

        this.onClickSwords = this.onClickSwords.bind(this);
        this.onClickDaggers = this.onClickDaggers.bind(this);

        this.onClickAxes = this.onClickAxes.bind(this);
        this.onClickPolearms = this.onClickPolearms.bind(this);
        this.onClickSpears = this.onClickSpears.bind(this);

        this.onClickMasses = this.onClickMasses.bind(this);
        this.onClickScepters = this.onClickScepters.bind(this);
        this.onClickClubs = this.onClickClubs.bind(this);

        this.onClickThrowingWeapons = this.onClickThrowingWeapons.bind(this);
        this.onClickJavelins = this.onClickJavelins.bind(this);

        this.onClickAmulets = this.onClickAmulets.bind(this);
        this.onClickRings = this.onClickRings.bind(this);
        this.onClickCharms = this.onClickCharms.bind(this);
        this.onClickJewels = this.onClickJewels.bind(this);

        this.onClickAmazon = this.onClickAmazon.bind(this);
        this.onClickDruid = this.onClickDruid.bind(this);
        this.onClickBarbarian = this.onClickBarbarian.bind(this);
    }

    // Item difficulty :
   public onClickNormalUniques = ()  => this.props.search.MaximumLevelRequired = 30;
   public onClickExceptionalUniques = ()  => this.props.search.MaximumLevelRequired = 60;
   public onClickEliteUniques = ()  => this.props.search.MaximumLevelRequired = 90;
   public onClickLegendaryUniques = ()  => this.props.search.MaximumLevelRequired = Math.max();

    // Armors :
    public onClickBodyArmors = ()  => this.props.search.SubCategories = [ ItemSubCategory.Torso ];
    public onClickShields = ()  => this.props.search.SubCategories = [ ItemSubCategory.Offhand ];
    public onClickGloves = ()  => this.props.search.SubCategories = [ ItemSubCategory.Hands ];
    public onClickShoes = ()  => this.props.search.SubCategories = [ ItemSubCategory.Feet ];
    public onClickHelms = ()  => this.props.search.SubCategories = [ ItemSubCategory.Head ];
    public onClickBelts = ()  => this.props.search.SubCategories = [ ItemSubCategory.Waist ];

    // Weapons :
    public onClickBows = ()  => this.props.search.SubCategories = [ ItemSubCategory.Bow, ItemSubCategory.Two_Handed_Bow ];
    public onClickCrossbows = ()  => this.props.search.SubCategories = [ ItemSubCategory.Crossbow, ItemSubCategory.Two_Handed_Crossbow, ];
    public onClickArrows = ()  => this.props.search.SubCategories = [ ItemSubCategory.Arrows, ItemSubCategory.Bolts ];
    public onClickStaffs = ()  => this.props.search.SubCategories = [ ItemSubCategory.Staff, ItemSubCategory.Two_Handed_Staff ];
    public onClickWands = ()  => this.props.search.SubCategories = [ ItemSubCategory.Wand ];
    public onClickOrbs = ()  => this.props.search.SubCategories = [ ItemSubCategory.Orb ];
    public onClickSwords = ()  => this.props.search.SubCategories = [ ItemSubCategory.Sword, ItemSubCategory.Two_And_One_Handed_Sword ];
    public onClickDaggers = ()  => this.props.search.SubCategories = [ ItemSubCategory.Knife ];
    public onClickAxes = ()  => this.props.search.SubCategories = [ ItemSubCategory.Axe, ItemSubCategory.Two_Handed_Axe ];
    public onClickPolearms = ()  => this.props.search.SubCategories = [ ItemSubCategory.Polearm, ItemSubCategory.Two_Handed_Polearm ];
    public onClickSpears = ()  => this.props.search.SubCategories = [ ItemSubCategory.Spear, ItemSubCategory.Two_Handed_Spear ];
    public onClickMasses = ()  => this.props.search.SubCategories = [ ItemSubCategory.Mace, ItemSubCategory.Two_Handed_Hammer ];
    public onClickScepters = ()  => this.props.search.SubCategories = [ ItemSubCategory.Scepter ];
    public onClickClubs = ()  => this.props.search.SubCategories = [ ItemSubCategory.Club ];
    public onClickThrowingWeapons = ()  => this.props.search.SubCategories = [ ItemSubCategory.Throwing_Axe, ItemSubCategory.Throwing_Potions, ItemSubCategory.Thorwing_Knife ];
    public onClickJavelins = ()  => this.props.search.SubCategories = [ ItemSubCategory.Javelin ];

    // Jewelry and others :
    public onClickAmulets = ()  => this.props.search.SubCategories = [ ItemSubCategory.Amulet ];
    public onClickRings = ()  => this.props.search.SubCategories = [ ItemSubCategory.Ring ];
    public onClickCharms = ()  => this.props.search.SubCategories = [ ItemSubCategory.Charm ];
    public onClickJewels = ()  => this.props.search.SubCategories = [ ItemSubCategory.Jewel ];

    // Class specific :
    public onClickAmazon = ()  => this.props.search.SubCategories = [ ItemSubCategory.Amazon_Bow, ItemSubCategory.Amazon_Javelin, ItemSubCategory.Amazon_Spear, ItemSubCategory.Two_Handed_Amazon_Bow, ItemSubCategory.Two_Handed_Amazon_Spear ];
    public onClickDruid = ()  => this.props.search.SubCategories = [ ItemSubCategory.Druid_Helm ];
    public onClickBarbarian = ()  => this.props.search.SubCategories = [ ItemSubCategory.Barbarian_Helm ];

    render()
    {
        return ('x');
    }
}

export default SearchItem;