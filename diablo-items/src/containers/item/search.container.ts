import { connect } from 'react-redux'
import {searchItem} from '../../store/item/actions';
import SearchItem from "../../components/Items/SearchItem";

// Cette fonction est appelé à chaque fois que le store est mis à jour.
const mapStateToProps = state => ({ items: state.items});
const mapDispatchToProps = (dispatch, ownProps) => ({
    search: () => dispatch(searchItem(ownProps.searchQueryStringParameter))
})

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(SearchItem)