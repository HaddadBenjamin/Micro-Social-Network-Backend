import React from "react"
import
{
    MDBContainer,
    MDBRow,
    MDBCol,
    MDBFormInline,
    MDBBtn
} from "mdbreact";
import * as style from './ItemCategoriesFilter.css'


class ItemCategoriesFilters extends React.Component
{
    render() {
        return (
            <>
                <MDBFormInline style={{color : "white" }}>
                    <div>
                        <select className="browser-default custom-select">
                            <option>Normal Uniques</option>
                            <option>Exceptional Uniques</option>
                            <option>Elite Uniques</option>
                            <option>Legendary Uniques</option>
                        </select>
                    </div>
                    <MDBContainer>
                    <MDBRow>
                        <MDBCol md="3" className="font-weight-bold py-4">Weapons
                            <div className="font-weight-normal item-category-link">
                                <div>Arrows</div>
                                <div>Axes</div>
                                <div>Bows</div>
                                <div>Clubs</div>
                                <div>Crossbows</div>
                                <div>Daggers</div>
                                <div>Javelin</div>
                                <div>Masses</div>
                                <div>Polearms</div>
                                <div>Scepters</div>
                                <div>Spears</div>
                                <div>Staffs</div>
                                <div>Swords</div>
                                <div>Throwing</div>
                                <div>Wands</div>
                            </div >
                        </MDBCol>
                        <MDBCol md="3" className="font-weight-bold py-4">Armors
                            <div className="font-weight-normal item-category-link">
                                <div>Armors</div>
                                <div>Belts</div>
                                <div>Gloves</div>
                                <div>Helms</div>
                                <div>Shields</div>
                                <div>Shoes</div>
                            </div >
                        </MDBCol>
                        <MDBCol md="3" className="font-weight-bold py-4">Jewelry
                            <div className="font-weight-normal item-category-link">
                                <div>Amulets</div>
                                <div>Charms</div>
                                <div>Jewels</div>
                                <div>Rings</div>
                            </div >
                        </MDBCol>
                        <MDBCol md="3" className="font-weight-bold py-4">Classes
                            <div className="font-weight-normal item-category-link">
                                <div>Amazon</div>
                                <div>Assassin</div>
                                <div>Barbarian</div>
                                <div>Druid</div>
                                <div>Sorceress</div>
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