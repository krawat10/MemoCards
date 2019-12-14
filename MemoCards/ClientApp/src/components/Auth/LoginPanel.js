import React, {useState} from 'react';
import {Button, Form, FormGroup, Input} from 'reactstrap';
import {history} from "../../App";

const LoginPanel = ({onSubmit, title, description, language}) => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, showError] = useState('');

    const authHandler = async () => {
        showError('');
        setLoading(true);
        const success = await onSubmit(email, password, showError);
        setLoading(false);
        if(success) history.push('/');
    };

    return (
        <div className="card">
            <div className="card-body">
                <Form onSubmit={e => {
                    e.preventDefault();
                    authHandler();
                }}>
                    <h5 className="card-title">{title}</h5>
                    <h6 className="card-subtitle mb-2 text-muted">{description}</h6>
                    <br/>
                    <FormGroup>
                        <Input
                            type="email"
                            name="email"
                            value={email}
                            placeholder="abc@gmail.com"
                            onChange={e => setEmail(e.target.value)}
                        />
                    </FormGroup>
                    <FormGroup>
                        <Input
                            type="password"
                            name="password"
                            value={password}
                            onChange={e => setPassword(e.target.value)}
                        />
                    </FormGroup>
                    <div className="alert alert-danger" role="alert" style={{display: error === '' ? 'none' : ''}}>
                        {error}
                    </div>
                    <Button type="submit" disabled={loading} block={true}>
                        {loading ? language['wait'] : language['login']}
                    </Button>
                </Form>
            </div>
        </div>)
};

export default LoginPanel;

