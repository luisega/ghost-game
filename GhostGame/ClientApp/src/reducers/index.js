import { combineReducers } from 'redux';

import currentWordReducer from './reducer_current_word';
import resetGameReducer from './reducer_reset_game';
import messageReducer from './reducer_message';
import gameFinishedReducer from './reducer_game_finished';


const rootReducer = combineReducers({
	currentWord: currentWordReducer,
	reset: resetGameReducer,
	message: messageReducer,
	gameFinished: gameFinishedReducer
});

export default rootReducer;