import React, {useState} from 'react';
import PropTypes from 'prop-types';
import {connect} from "react-redux";
import {del, put} from "../../services/apiRequest";
import {Input} from "reactstrap";

const MemoCardItem = ({memoCard, deleteMemoCard}) => {
    const [error, setError] = useState('');
    const [editState, setEditState] = useState(false);
    let [name, setName] = useState(memoCard.name);
    let [description, setDescription] = useState(memoCard.description);
    console.log(editState)

    async function remove() {
        try {
            await del('/memo/' + memoCard.id);
            deleteMemoCard(memoCard.id);
        }
        catch (e) {
            setError(e.message);
        }
    }

    async function submit() {
        try {
            memoCard.name = name;
            memoCard.description = description;
            await put('/memo/' + memoCard.id, memoCard);
        }
        catch (e) {
            setError(e.message);
        }
        setEditState(false);
    }

    if(editState)
    {
        return (
            <div className="card" style={{width: '18rem'}}>
                <div className="card-body">
                    <Input
                        name="name"
                        bsSize="sm"
                        value={name}
                        onChange={e => setName(e.target.value)}
                        placeholder="title"
                    />
                    <Input
                        name="description"
                        type="textarea"
                        bsSize="sm"
                        value={description}
                        onChange={e => setDescription(e.target.value)}
                        placeholder="description"
                    />
                    <a onClick={submit} className="card-link">Submit</a>
                    <div className="alert alert-danger" role="alert" style={{display: error === '' ? 'none' : ''}}>
                        {error}
                    </div>
                </div>
            </div>)
    }
    else {
        return (
            <div className="card" style={{width: '18rem'}}>
                <div className="card-body">
                    <h5 className="card-title">{name}</h5>
                    <div className="card-text">{description}</div>
                    <a onClick={remove} className="card-link">Remove</a>
                    <a onClick={() => setEditState(true)} className="card-link">Edit</a>
                    <div className="alert alert-danger" role="alert" style={{display: error === '' ? 'none' : ''}}>
                        {error}
                    </div>
                </div>
            </div>)
    }
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