import React, { Component } from 'react';

import '../../../styles/components/buttons/button.scss';

class Button extends Component {
	render() {
		const { className, style, children, onClick } = this.props;

		const classes = `btn ghost-btn ${className || ''}`;
		const finalOnClick = onClick instanceof Function ? this.onClickHandler.bind(this, onClick) : undefined;

		return (
			<div className='col-md-6 col-md-offset-3' style={{'textAlign':'center'}}>
				<button type="button" className={classes} style={style}	onClick={finalOnClick}>
					<div className='ghost-btn-label'>
						{this.renderContent(children)}
					</div>
				</button>
			</div>
		);
	}

	renderContent(children) {
		return children;
	}

	onClickHandler(callback, event) {
		callback(event);
	}
}

export default Button;