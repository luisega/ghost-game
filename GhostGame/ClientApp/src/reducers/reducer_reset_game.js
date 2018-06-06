import { RESET_GAME } from '../actions';

const DEFAULT_STATE = null;

export default function (state = DEFAULT_STATE, action) {
	switch (action.type) {
	case RESET_GAME:
		if (!action.payload.data) {
			return [];
		}

		return action.payload.data;
	default:
		return state;
	}
}