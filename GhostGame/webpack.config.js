'use strict';

const ExtractTextPlugin = require('extract-text-webpack-plugin');

var bundleFileName = 'bundle',
	appFileName = 'index',
	paths = {
		webroot: __dirname + '/wwwroot/',
		approot: __dirname + '/ClientApp/'
	};

module.exports = {
	entry: {
		bundle: paths.approot + 'src/' + appFileName
	},
	output: {
		path: paths.webroot + 'js/',
		filename: bundleFileName + '.js'
	},
	module: {
		rules: [
			{
				test: /\.(js|jsx)?$/,
				use: {
					loader: 'babel-loader',
					options: {
						presets: ['env', 'react']
					}
				}
			},
			{
				test: /\.(css|scss)$/,
				use: ExtractTextPlugin.extract({
					fallback: 'style-loader',
					//resolve-url-loader may be chained before sass-loader if necessary
					use: ['css-loader', 'sass-loader']
				})
			}
		]
	},
	plugins: [
		new ExtractTextPlugin('../css/' + bundleFileName + '.css')
	]
};