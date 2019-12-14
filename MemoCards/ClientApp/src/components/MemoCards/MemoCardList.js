import React from 'react'
import PropTypes from 'prop-types'
import MemoCardItem from "./MemoCardItem";
import {connect} from 'react-redux';
import MemoCardItemAdd from "./AddMemoCardItem";

const MemoCardList = ({memoCards, deleteMemoCard}) => {
    return (
        <div className="container">
            <div className="row">
                <div className="md-4">
                    <MemoCardItemAdd key="MemoCardItemAdd"/>
                </div>
                {memoCards.map(memoCard => (
                    <div key={memoCard.created} className="md-4"><MemoCardItem  memoCard={memoCard} deleteMemoCard={deleteMemoCard} /></div>
                ))}
            </div>
        </div>
    )
};

MemoCardList.propTypes = {
    memoCards: PropTypes.arrayOf(PropTypes.shape({
        name: PropTypes.string.isRequired,
        description: PropTypes.string.isRequired,
        created: PropTypes.string.isRequired,
        updated: PropTypes.string.isRequired,
        userId: PropTypes.string.isRequired,
        }).isRequired
    ).isRequired
};

export default connect()(MemoCardList);
