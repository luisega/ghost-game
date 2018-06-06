
import React, { Component } from 'react';
import { connect } from 'react-redux';

import { resetGame } from '../actions';

import Button from './buttons/button';
import Keyboard from './buttons/keyboard';
import TextBox from './text/textBox';

import '../../styles/components/gamePanel.scss';

class GamePanel extends Component{
	render() {
		return (
			<div className='ghost-panel'>
				{this.renderInfoSection()}
				{this.renderCurrentWordSection()}
				{this.renderMessageSection()}
				{this.renderKeyboard()}
				{this.renderResetButton()}
			</div>
		);
	}

	renderInfoSection(){
		return (
			<div className='ghost-info-box'>
				<p className='ghost-info-title'>Ghost game</p>
			</div>
		);
	}

	renderCurrentWordSection(){
		return (
			<TextBox className='ghost-current-word-text' childrenClassName='ghost-current-word'>
				{this.props.currentWord.toUpperCase()}
			</TextBox>
		);
	}

	renderMessageSection(){
		return (
			<TextBox className='ghost-message-text' childrenClassName='ghost-message'>
				{this.props.message}
			</TextBox>
		);
	}

	renderKeyboard(){
		return <Keyboard className='col-md-6 col-md-offset-3' />;
	}

	renderResetButton(){
		return (
			<Button onClick={this.props.resetGame}> 
				Reset game 
			</Button>
		);
	}
	
	componentWillMount(){
		this.props.resetGame();
	}
}

const mapStateToProps = (state) => {
	const currentWord = state.currentWord;
	const message = state.message;

	return {
		currentWord: currentWord,
		message: message
	};
};

export default connect(mapStateToProps, 
	{resetGame})(GamePanel);