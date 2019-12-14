import React, {Component} from 'react';
import MemoCardsListProvider from "./MemoCards/MemoCardsListProvider";

export class Home extends Component {
    static displayName = Home.name;
    
    render() {
        return (
            <div>
                <MemoCardsListProvider/>
            </div>
        );
    }
}
