import React, {useEffect} from 'react'
import {MemoCardsStatus} from "../../reducers/memoCardsStatus";
import {deleteMemoCard, initialize} from "../../actions";
import {connect} from "react-redux";
import {get} from "../../services/apiRequest";
import MemoCardList from "./MemoCardList";
import PropTypes from "prop-types";

const MemoCardsListProvider = ({user, memoCardsState, language, initialize, deleteMemoCard}) => {

    useEffect(() => {
        async function getMemoCards() {
            if (user.isValid && memoCardsState.status === MemoCardsStatus.NOT_INITIALIZED) {
                try {
                    initialize(await get('/memo'));
                } catch (e) {

                }
            }
        }
        getMemoCards();
    }, [user]);

    if(user.isValid){
        return (<div >
            <MemoCardList memoCards={memoCardsState.memoCards} deleteMemoCard={deleteMemoCard}/>
        </div>);
    }
    else {
        return (<div><h1>{language['pleaseLogin']}</h1></div>)
    }


};

MemoCardsListProvider.propTypes = {
    user: PropTypes.shape({
        email: PropTypes.string,
        token: PropTypes.shape({
            value: PropTypes.string,
            expires: PropTypes.string
        }),
    }),
    memoCardsState: PropTypes.shape({
        status: PropTypes.string.isRequired,
        memoCards: PropTypes.arrayOf(PropTypes.shape({
                name: PropTypes.string.isRequired,
                description: PropTypes.string.isRequired,
                created: PropTypes.string.isRequired,
                updated: PropTypes.string.isRequired,
                userId: PropTypes.string.isRequired,
            }).isRequired
        ).isRequired
    }).isRequired
};

const mapStateToProps = state => ({
    user: state.user,
    memoCardsState: state.memoCardsState,
    language: state.language
});

const mapDispatchToProps = dispatch => ({
    initialize: (memoCards) => dispatch(initialize(memoCards)),
    deleteMemoCard: (id) => dispatch(deleteMemoCard(id))
});

export default connect(mapStateToProps, mapDispatchToProps)(MemoCardsListProvider);