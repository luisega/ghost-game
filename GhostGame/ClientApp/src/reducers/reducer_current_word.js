import { POST_MOVE, UPDATE_CURRENT_WORD, RESET_GAME } from '../actions';

const DEFAULT_STATE = '';

export default function (state = DEFAULT_STATE, action) {
	switch (action.type) {
	case POST_MOVE:
		if (!action.payload.data) {
			return '';
		}

		return action.payload.data.currentWord;
	case UPDATE_CURRENT_WORD:
		return action.meta;
	case RESET_GAME:
		return DEFAULT_STATE;
	default:
		return state;
	}
}