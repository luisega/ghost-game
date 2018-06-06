import React, { Component } from 'react';
import { Provider } from 'react-redux';
import store from './store';

import GamePanel from './components/gamePanel';

class App extends Component {
	render() {
		return (
			<Provider store={store}>
				<GamePanel />
			</Provider>
		);
	}
}

export default App;