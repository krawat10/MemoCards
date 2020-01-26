import React, {useState, useEffect} from 'react';
import {addMemoCard} from "../../actions";
import {connect} from "react-redux";
import {Button, ButtonGroup, Input} from "reactstrap";
import {post} from "../../services/apiRequest";

const addPlus = (id) => {
    const c = document.getElementById(id); // Adds to DOM
    c.style.marginLeft = '4px';
    c.style.marginBottom = '-7px';
    // c.style.width = '27px';
    const ctx = c.getContext("2d");
    ctx.canvas.width = 25;
    ctx.canvas.height = 27;
    ctx.moveTo(0, 12);
    ctx.lineTo(10, 12);
    ctx.lineTo(10, 2);
    ctx.lineTo(15, 2);
    ctx.lineTo(15, 12);
    ctx.lineTo(25, 12);
    ctx.lineTo(25, 17);
    ctx.lineTo(15, 17);
    ctx.lineTo(15, 27);
    ctx.lineTo(10, 27);
    ctx.lineTo(10, 17);
    ctx.lineTo(0, 17);
    ctx.lineTo(0, 12);
    ctx.fillStyle = "black";
    ctx.fill();

    ctx.stroke();
};

const AddMemoCardItem = ({user, dispatch}) => {
    let [name, setName] = useState('');
    let [description, setDescription] = useState('');

    useEffect(() => {
        addPlus('plus-icon');
    });

    const clearInput = () => {
        setName('');
        setDescription('');
    };

    async function add() {
        let memoCard = {
            name,
            description,
            created: new Date(),
            updated: new Date(),
            userId: user.id
        };
        ;

        const memo = await post('/memo', memoCard);
        dispatch(addMemoCard(memo))
            // .then(value => dispatch(addMemoCard(value)));

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
                <Button size="sm" color="primary" onClick={add}>
                    Add <canvas id="plus-icon"/>
                </Button>
                <Button size="sm" color="secondary" onClick={clearInput}>Clear</Button>
            </ButtonGroup>
        </div>
    </div>);
};

const mapStateToProps = state => ({
    user: state.user
});


export default connect(mapStateToProps)(AddMemoCardItem);