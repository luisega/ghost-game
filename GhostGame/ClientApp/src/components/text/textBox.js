import React, { Component } from 'react';

import '../../../styles/components/text/textBox.scss';

export default class TextBox extends Component{
	render() {
		const { className, childrenClassName, children } = this.props;

		return (
			<div className={className}>
				<p className={childrenClassName}>
					{this.renderContent(children)}
				</p>
			</div>
		);
	}

	renderContent(children) {
		return children;
	}
}