import React from 'react';
import SearchItemDto, {ItemSubCategory} from "./SearchItemDto";
import Item from "./Item";
import ItemCategoriesFilters from './ItemCategoriesFilter'
import {map} from 'lodash'
import qs from 'qs'
import api from '../../Utilities/api'
import scrollTo from '../../Utilities/animate'

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
        this.onClickAssassin = this.onClickAssassin.bind(this);
        this.onClickBarbarian = this.onClickBarbarian.bind(this);
        this.onClickDruid = this.onClickDruid.bind(this);
        this.onClickNecromancer = this.onClickNecromancer.bind(this);
        this.onClickPaladin = this.onClickPaladin.bind(this);
        this.onClickSorceress = this.onClickSorceress.bind(this);

        this.setSubCategoriesAndSearch = this.setSubCategoriesAndSearch.bind(this);
    }

    // Armors :
    public onClickBodyArmors = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Torso ]);
    public onClickShields = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Offhand ]);
    public onClickGloves = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Hands ]);
    public onClickShoes = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Feet ]);
    public onClickHelms = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Head ]);
    public onClickBelts = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Waist ]);

    // Weapons :
    public onClickBows = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Bow, ItemSubCategory.Two_Handed_Bow ]);
    public onClickCrossbows = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Crossbow, ItemSubCategory.Two_Handed_Crossbow, ]);
    public onClickArrows = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Arrows, ItemSubCategory.Bolts ]);
    public onClickStaffs = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Staff, ItemSubCategory.Two_Handed_Staff ]);

    public onClickSwords = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Sword, ItemSubCategory.Two_And_One_Handed_Sword ]);
    public onClickDaggers = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Knife ]);
    public onClickAxes = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Axe, ItemSubCategory.Two_Handed_Axe ]);
    public onClickPolearms = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Polearm, ItemSubCategory.Two_Handed_Polearm ]);
    public onClickSpears = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Spear, ItemSubCategory.Two_Handed_Spear ]);
    public onClickMasses = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Mace, ItemSubCategory.Two_Handed_Hammer, ItemSubCategory.Hammer ]);
    public onClickScepters = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Scepter ]);
    public onClickClubs = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Club ]);
    public onClickThrowingWeapons = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Throwing_Axe, ItemSubCategory.Throwing_Potions, ItemSubCategory.Thorwing_Knife ]);
    public onClickJavelins = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Javelin ]);

    // Jewelry and others :
    public onClickAmulets = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Amulet ]);
    public onClickRings = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Ring ]);
    public onClickCharms = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Charm ]);
    public onClickJewels = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Jewel ]);

    // Class specific :
    public onClickAmazon = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Amazon_Bow, ItemSubCategory.Amazon_Javelin, ItemSubCategory.Amazon_Spear, ItemSubCategory.Two_Handed_Amazon_Bow, ItemSubCategory.Two_Handed_Amazon_Spear ]);
    public onClickDruid = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Druid_Helm ]);
    public onClickBarbarian = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Barbarian_Helm ]);
    public onClickAssassin = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Hand_To_Hand, ItemSubCategory.Hand_To_Hand_Two_Handed, ItemSubCategory.Assassin_Claw ]);
    public onClickSorceress = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Orb, ItemSubCategory.Sorceress_Orb ]);
    public onClickNecromancer = ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Wand, ItemSubCategory.Necromancer_Shield ]);
    public onClickPaladin= ()  => this.setSubCategoriesAndSearch([ ItemSubCategory.Paladin_Shield ]);

    public setSubCategoriesAndSearch(subCategories : ItemSubCategory[])
    {
        this.props.search.SubCategories = subCategories;

        this.search();
    }

    public search()
    {
       api.get<Item[]>(
           'Items/searchuniques',
           'SEARCH_ITEMS',
           qs.stringify(
       {
                   SubCategories : map(this.props.search.SubCategories, _ => ItemSubCategory[_]).join(', '),
           }));

        scrollTo('#item-filter-view');
    }

    // 1) This function is PURE, that's mean it should be extarnalised in another file with dedicated tests.
    // 2) We should have 3 events : search, search done, search failed.
    //    search should be an epic that do the search.
    //    then (search.success(response.data))
    //    failed (search.failed('can't search)


    render()
    {
        return (
            <>
                <ItemCategoriesFilters
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
                    onClickAssassin={this.onClickAssassin}
                    onClickBarbarian={this.onClickBarbarian}
                    onClickDruid={this.onClickDruid}
                    onClickPaladin={this.onClickPaladin}
                    onClickNecromancer={this.onClickNecromancer}
                    onClickSorceress={this.onClickSorceress}
                />
            </>
        );
    }
}

export default SearchItem;