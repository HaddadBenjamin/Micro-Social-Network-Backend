import React from "react"
import
{
    MDBContainer,
    MDBRow,
    MDBCol,
    MDBFormInline,
} from "mdbreact";
import * as style from './ItemCategoriesFilter.css'


class ItemCategoriesFilters extends React.Component
{
render() {
        return (
            <>
                <MDBFormInline className="item-category-filter" style={style}>
                    <div>
                        <select className="browser-default custom-select">
                            <option onChange={() => this.props.onClickNormalUniques()}>Normal Uniques</option>
                            <option onChange={() => this.props.onClickExceptionalUniques()}>Exceptional Uniques</option>
                            <option onChange={() => this.props.onClickEliteUniques()}>Elite Uniques</option>
                            <option onChange={() => this.props.onClickLegendaryUniques()}>Legendary Uniques</option>
                        </select>
                    </div>
                    <MDBContainer>
                    <MDBRow>
                        <MDBCol md="3" className="font-weight-bold py-4">Weapons
                            <div className="font-weight-normal item-category-link">
                                <div onClick={() => this.props.onClickArrows()}>Arrows</div>
                                <div onClick={() => this.props.onClickAxes()}>Axes</div>
                                <div onClick={() => this.props.onClickBows()}>Bows</div>
                                <div onClick={() => this.props.onClickClubs()}>Clubs</div>
                                <div onClick={() => this.props.onClickCrossbows()}>Crossbows</div>
                                <div onClick={() => this.props.onClickDaggers()}>Daggers</div>
                                <div onClick={() => this.props.onClickJavelins()}>Javelins</div>
                                <div onClick={() => this.props.onClickMasses()}>Masses</div>
                                <div onClick={() => this.props.onClickPolearms()}>Polearms</div>
                                <div onClick={() => this.props.onClickScepters()}>Scepters</div>
                                <div onClick={() => this.props.onClickSpears()}>Spears</div>
                                <div onClick={() => this.props.onClickStaffs()}>Staffs</div>
                                <div onClick={() => this.props.onClickSwords()}>Swords</div>
                                <div onClick={() => this.props.onClickThrowingWeapons()}>Throwing</div>
                            </div >
                        </MDBCol>
                        <MDBCol md="3" className="font-weight-bold py-4">Armors
                            <div className="font-weight-normal item-category-link">
                                <div onClick={() => this.props.onClickBodyArmors()}>Armors</div>
                                <div onClick={() => this.props.onClickBelts()}>Belts</div>
                                <div onClick={() => this.props.onClickGloves()}>Gloves</div>
                                <div onClick={() => this.props.onClickHelms()}>Helms</div>
                                <div onClick={() => this.props.onClickShields()}>Shields</div>
                                <div onClick={() => this.props.onClickShoes()}>Shoes</div>
                            </div >
                        </MDBCol>
                        <MDBCol md="3" className="font-weight-bold py-4">Jewelry
                            <div className="font-weight-normal item-category-link">
                                <div onClick={() => this.props.onClickAmulets()}>Amulets</div>
                                <div onClick={() => this.props.onClickCharms()}>Charms</div>
                                <div onClick={() => this.props.onClickJewels()}>Jewels</div>
                                <div onClick={() => this.props.onClickRings()}>Rings</div>
                            </div >
                        </MDBCol>
                        <MDBCol md="3" className="font-weight-bold py-4">Classes
                            <div className="font-weight-normal item-category-link">
                                <div onClick={() => this.props.onClickAmazon()}>Amazon</div>
                                <div onClick={() => this.props.onClickAssassin()}>Assassin</div>
                                <div onClick={() => this.props.onClickBarbarian()}>Barbarian</div>
                                <div onClick={() => this.props.onClickDruid()}>Druid</div>
                                <div onClick={() => this.props.onClickNecromancer()}>Sorceress</div>
                                <div onClick={() => this.props.onClickSorceress()}>Sorceress</div>
                            </div >
                        </MDBCol>
                    </MDBRow>
                    </MDBContainer>
                    {/*<MDBBtn outline color="white ">Search</MDBBtn>*/}
                </MDBFormInline>
            </>
        );
    }
}

export default ItemCategoriesFilters;