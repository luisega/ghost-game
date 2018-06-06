import React, { Component } from 'react';
import { connect } from 'react-redux';

import { postMove, updateCurrentWord } from '../../actions';

import '../../../styles/components/buttons/buttonGroup.scss';

class ButtonGroup extends Component {
	render() {
		const { className, buttons } = this.props;

		const btnGroupClass = `btn-group ${className || ''}`;
		
		return (
			<div className={btnGroupClass}>
				{buttons.map((button) => { return this.renderButton(button); })}
			</div>
		);
	}

	renderButton(button) {
		const btnClass = 'btn ghost-letter-btn';

		return ( 
			<button key={button}
					name={button}
					value={button}
					className={btnClass}
					disabled={this.props.gameFinished || !button}
					onClick={this.onClick.bind(this, button)}>
					{button.toUpperCase() || ''}
			</button>
		);
	}

	onClick(value){
		const nextWord = this.props.currentWord ? this.props.currentWord.concat(value) : value;
		this.props.updateCurrentWord(nextWord);
		
		this.props.postMove({'currentWord': nextWord});
	}
}

const mapStateToProps = (state) => {
	const currentWord = state.currentWord;
	const gameFinished = state.gameFinished;

	return {
		currentWord: currentWord,
		gameFinished: gameFinished
	};
};

export default connect(mapStateToProps, {
	postMove,
	updateCurrentWord
})(ButtonGroup);