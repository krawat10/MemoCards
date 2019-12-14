import {login} from "../../actions";
import {connect} from "react-redux";
import {post} from "../../services/apiRequest";
import LoginPanel from "./LoginPanel";

const mapDispatchToProps = dispatch => ({
    onSubmit: async (email, password, onError) => {
        try {
            const user = await post('/User/Login', {email, password});
            dispatch(login(user))
        } catch (e) {
            onError(e.message);
            return false;
        }
        return true;
    }
});

const mapStateToProps = state => ({
    title: state.language['login'],
    description: state.language['typeEmailAndPassword'],
    language: state.language
});

export default connect(mapStateToProps, mapDispatchToProps)(LoginPanel);