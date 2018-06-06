import axios from 'axios';

const ROOT_URL = 'api';

export const POST_MOVE = 'POST_MOVE';
export const RESET_GAME = 'RESET_GAME';
export const UPDATE_CURRENT_WORD = 'UPDATE_CURRENT_WORD';

export function postMove(currentWord) {
	const request = axios.post(`${ROOT_URL}/GhostGame`, currentWord);

	return {
		type: POST_MOVE,
		payload: request
	};
}

export function resetGame() {
	const request = axios.post(`${ROOT_URL}/GhostGame/reset`);

	return {
		type: RESET_GAME,
		payload: request
	};
}

export function updateCurrentWord(currentWord) {

	return {
		type: UPDATE_CURRENT_WORD,
		meta: currentWord
	};
}