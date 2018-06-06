import { POST_MOVE, UPDATE_CURRENT_WORD, RESET_GAME } from '../actions';

const DEFAULT_STATE = 'Press any letter to start...';

export default function (state = DEFAULT_STATE, action) {
	switch (action.type) {
	case POST_MOVE:
		if (!action.payload.data) {
			return 'There was an error with the server. Please, restart it.';
		}

		return action.payload.data.message;
	case UPDATE_CURRENT_WORD:
		return 'Computer\'s turn...';
	case RESET_GAME:
		return DEFAULT_STATE;
	default:
		return state;
	}
}