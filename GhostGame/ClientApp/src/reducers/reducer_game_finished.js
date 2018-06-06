import { POST_MOVE, RESET_GAME } from '../actions';

const DEFAULT_STATE = false;

export default function (state = DEFAULT_STATE, action) {
	switch (action.type) {
	case POST_MOVE:
		if (!action.payload.data) {
			return true;
		}

		return action.payload.data.gameStatus != 'Playing';
	case RESET_GAME:
		return DEFAULT_STATE;
	default:
		return state;
	}
}