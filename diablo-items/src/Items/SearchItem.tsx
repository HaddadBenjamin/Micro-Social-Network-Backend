import React from "react";
import SearchItemDto from "src/Items/SearchItemDto";

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
    }

    public onClickNormalUnique = ()  => this.props.search.MaximumLevelRequired = 30;
    public onClickExceptionalUnique = ()  => this.props.search.MaximumLevelRequired = 60;
    public onClickEliteUnique = ()  => this.props.search.MaximumLevelRequired = 90;
    public onClickLegendaryUnique = ()  => this.props.search.MaximumLevelRequired = Math.max();

    render()
    {
        return ('');
    }
}

export default SearchItem;