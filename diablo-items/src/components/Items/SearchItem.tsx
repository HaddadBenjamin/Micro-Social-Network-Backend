import React  from 'react';
import SearchItemDto, {ItemSubCategory} from "./SearchItemDto";
import Item from "./Item";
import axios from 'axios';
import  ItemCategoriesFilters from './ItemCategoriesFilter'
import { store } from '../../store/store'
import { map } from 'lodash'
import qs from 'qs'

interface Props
{
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
        this.onClickAssassin = this.onClickAssassin.bind(this);
        this.onClickSorceress = this.onClickSorceress.bind(this);

        this.setSubCategories = this.setSubCategories.bind(this);
        this.generateSearchQueryParameters = this.generateSearchQueryParameters.bind(this);
    }

    // Item difficulty :
   public onClickNormalUniques()
   {
       this.props.search.MinimumLevel = 0;
       this.props.search.MaximumLevel = 30;
       alert("x");

   }
   public onClickExceptionalUniques()
   {
       this.props.search.MinimumLevel = 30;
       this.props.search.MaximumLevel = 60;
       alert("x");
   }

   public onClickEliteUniques()
   {
       this.props.search.MinimumLevel = 60;
       this.props.search.MaximumLevel = 90;
       alert("x");
   }
   public onClickLegendaryUniques()
   {
       this.props.search.MinimumLevel = 90;
       this.props.search.MaximumLevel = Math.max();
       alert("x");
   }

    // Armors :
    public onClickBodyArmors = ()  => this.setSubCategories([ ItemSubCategory.Torso ]);
    public onClickShields = ()  => this.setSubCategories([ ItemSubCategory.Offhand ]);
    public onClickGloves = ()  => this.setSubCategories([ ItemSubCategory.Hands ]);
    public onClickShoes = ()  => this.setSubCategories([ ItemSubCategory.Feet ]);
    public onClickHelms = ()  => this.setSubCategories([ ItemSubCategory.Head ]);
    public onClickBelts = ()  => this.setSubCategories([ ItemSubCategory.Waist ]);

    // Weapons :
    public onClickBows = ()  => this.setSubCategories([ ItemSubCategory.Bow, ItemSubCategory.Two_Handed_Bow ]);
    public onClickCrossbows = ()  => this.setSubCategories([ ItemSubCategory.Crossbow, ItemSubCategory.Two_Handed_Crossbow, ]);
    public onClickArrows = ()  => this.setSubCategories([ ItemSubCategory.Arrows, ItemSubCategory.Bolts ]);
    public onClickStaffs = ()  => this.setSubCategories([ ItemSubCategory.Staff, ItemSubCategory.Two_Handed_Staff ]);

    public onClickSwords = ()  => this.setSubCategories([ ItemSubCategory.Sword, ItemSubCategory.Two_And_One_Handed_Sword ]);
    public onClickDaggers = ()  => this.setSubCategories([ ItemSubCategory.Knife ]);
    public onClickAxes = ()  => this.setSubCategories([ ItemSubCategory.Axe, ItemSubCategory.Two_Handed_Axe ]);
    public onClickPolearms = ()  => this.setSubCategories([ ItemSubCategory.Polearm, ItemSubCategory.Two_Handed_Polearm ]);
    public onClickSpears = ()  => this.setSubCategories([ ItemSubCategory.Spear, ItemSubCategory.Two_Handed_Spear ]);
    public onClickMasses = ()  => this.setSubCategories([ ItemSubCategory.Mace, ItemSubCategory.Two_Handed_Hammer ]);
    public onClickScepters = ()  => this.setSubCategories([ ItemSubCategory.Scepter ]);
    public onClickClubs = ()  => this.setSubCategories([ ItemSubCategory.Club ]);
    public onClickThrowingWeapons = ()  => this.setSubCategories([ ItemSubCategory.Throwing_Axe, ItemSubCategory.Throwing_Potions, ItemSubCategory.Thorwing_Knife ]);
    public onClickJavelins = ()  => this.setSubCategories([ ItemSubCategory.Javelin ]);

    // Jewelry and others :
    public onClickAmulets = ()  => this.setSubCategories([ ItemSubCategory.Amulet ]);
    public onClickRings = ()  => this.setSubCategories([ ItemSubCategory.Ring ]);
    public onClickCharms = ()  => this.setSubCategories([ ItemSubCategory.Charm ]);
    public onClickJewels = ()  => this.setSubCategories([ ItemSubCategory.Jewel ]);

    // Class specific :
    public onClickAmazon = ()  => this.setSubCategories([ ItemSubCategory.Amazon_Bow, ItemSubCategory.Amazon_Javelin, ItemSubCategory.Amazon_Spear, ItemSubCategory.Two_Handed_Amazon_Bow, ItemSubCategory.Two_Handed_Amazon_Spear ]);
    public onClickDruid = ()  => this.setSubCategories([ ItemSubCategory.Druid_Helm ]);
    public onClickBarbarian = ()  => this.setSubCategories([ ItemSubCategory.Barbarian_Helm ]);
    public onClickAssassin = ()  => this.setSubCategories([ ItemSubCategory.Hand_To_Hand, ItemSubCategory.Hand_To_Hand_Two_Handed ]);
    public onClickSorceress = ()  => this.setSubCategories([ ItemSubCategory.Orb ]);
    public onClickNecromancer = ()  => this.setSubCategories([ ItemSubCategory.Wand ]);

    public setSubCategories(subCategories : ItemSubCategory[])
    {
        this.props.search.SubCategories = subCategories;

        var searchQueryParameters = this.generateSearchQueryParameters();

        this.search(searchQueryParameters );
    }

    public generateSearchQueryParameters() : string
    {
        const { SubCategories, MinimumLevel, MaximumLevel} = this.props.search;
        const subCategories = map(SubCategories, _ => ItemSubCategory[_]).join(', ');
        const searchQueryParameters = qs.stringify({
           SubCategories : subCategories,
            MinimumLevel : MinimumLevel,
            MaximumLevel : MaximumLevel
        });
        
        console.log(searchQueryParameters);
        return `?${searchQueryParameters}`;
    }
    
    public search(searchQueryParameters : string)
    {
        axios.get<Item[]>(`http://localhost:56205/api/v1/Items/searchuniques/${searchQueryParameters}`)
             .then(response => {
                 store.dispatch({
                     type: 'SEARCH_ITEMS',
                     payload : response.data
                 });
             }/* items = response.data */);
    }

    render()
    {
        return (
            <>
                <ItemCategoriesFilters
                    onClickNormalUniques={this.onClickNormalUniques}
                    onClickExceptionalUniques={this.onClickExceptionalUniques}
                    onClickEliteUniques={this.onClickEliteUniques}
                    onClickLegendaryUniques={this.onClickLegendaryUniques}

                    onClickBows={this.onClickBows}
                    onClickCrossbows={this.onClickCrossbows}
                    onClickClubs={this.onClickClubs}
                    onClickArrows={this.onClickArrows}
                    onClickStaffs={this.onClickStaffs}
                    onClickSwords={this.onClickSwords}
                    onClickDaggers={this.onClickDaggers}
                    onClickAxes={this.onClickAxes}
                    onClickPolearms={this.onClickPolearms}
                    onClickSpears={this.onClickSpears}
                    onClickMasses={this.onClickMasses}
                    onClickScepters={this.onClickScepters}
                    onClickThrowingWeapons={this.onClickThrowingWeapons}
                    onClickJavelins={this.onClickJavelins}

                    onClickBodyArmors={this.onClickBodyArmors}
                    onClickShields={this.onClickShields}
                    onClickGloves={this.onClickGloves}
                    onClickShoes={this.onClickShoes}
                    onClickHelms={this.onClickHelms}
                    onClickBelts={this.onClickBelts}

                    onClickAmulets={this.onClickAmulets}
                    onClickRings={this.onClickRings}
                    onClickCharms={this.onClickCharms}
                    onClickJewels={this.onClickJewels}

                    onClickAmazon={this.onClickAmazon}
                    onClickDruid={this.onClickDruid}
                    onClickBarbarian={this.onClickBarbarian}
                    onClickAssassin={this.onClickAssassin}
                    onClickSorceress={this.onClickSorceress}
                    onClickNecromancer={this.onClickNecromancer}
                />
            </>
        );
    }
}

export default SearchItem;