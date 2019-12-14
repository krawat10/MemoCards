import React, {useState} from 'react';
import PropTypes from 'prop-types';
import {connect} from "react-redux";
import {del} from "../../services/apiRequest";

const MemoCardItem = ({memoCard, deleteMemoCard}) => {
    const [error, setError] = useState('')

    async function remove() {
        try {
            await del('/memo/' + memoCard.id);
            deleteMemoCard(memoCard.id);
        }
        catch (e) {
            setError(e.message);
        }
    }

    return (
        <div className="card" style={{width: '18rem'}}>
            <div className="card-body">
                <h5 className="card-title">{memoCard.name}</h5>
                <div className="card-text">{memoCard.description}</div>
                <a onClick={remove} className="card-link">Remove</a>
                <div className="alert alert-danger" role="alert" style={{display: error === '' ? 'none' : ''}}>
                    {error}
                </div>
            </div>
        </div>)
};

MemoCardItem.propTypes = {
    memoCard: PropTypes.shape({
            name: PropTypes.string.isRequired,
            description: PropTypes.string.isRequired,
            created: PropTypes.string.isRequired,
            updated: PropTypes.string.isRequired,
            userId: PropTypes.string.isRequired,
        }
    )
};

export default MemoCardItem