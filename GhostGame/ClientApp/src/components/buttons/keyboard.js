import React, { Component } from 'react';
import ButtonGroup from './buttonGroup';

class Keyboard extends Component {
	render() {
		const { className } = this.props;
		
		return (
			<div className={className}>
                <ButtonGroup className='ghost-btn-group' buttons={['a', 'b', 'c', 'd', 'e', 'f', 'g']} />
                <ButtonGroup className='ghost-btn-group' buttons={['h', 'i', 'j', 'k', 'l', 'm', 'n']} />
                <ButtonGroup className='ghost-btn-group' buttons={['o', 'p', 'q', 'r', 's', 't', 'u']} />
                <ButtonGroup className='ghost-btn-group-last' buttons={['v', 'w', 'x', 'y', 'z']} />
			</div>
		);
	}
}

export default Keyboard;