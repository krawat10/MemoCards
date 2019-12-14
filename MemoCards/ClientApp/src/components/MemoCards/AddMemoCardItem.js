import React, {useState} from 'react';
import {addMemoCard} from "../../actions";
import {connect} from "react-redux";
import {Button, ButtonGroup, Input} from "reactstrap";
import {post} from "../../services/apiRequest";

const AddMemoCardItem = ({user, dispatch}) => {
    let [name, setName] = useState('');
    let [description, setDescription] = useState('');

    const clearInput = () => {
        setName('');
        setDescription('');
    };

    function add() {
        let memoCard = {
            name,
            description,
            created: new Date(),
            updated: new Date(),
            userId: user.id
        };
        dispatch(addMemoCard(memoCard));

        post('/memo', memoCard).then(value => console.log(value));

        clearInput();
    }

    return (<div className="card" style={{width: '18rem'}}>
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
            <br/>
            <ButtonGroup>
                <Button size="sm" color="primary" onClick={add}>Add</Button>
                <Button size="sm" color="secondary" onClick={clearInput}>Clear</Button>
            </ButtonGroup>
        </div>
    </div>);
};

const mapStateToProps = state => ({
    user: state.user
});



export default connect(mapStateToProps)(AddMemoCardItem);