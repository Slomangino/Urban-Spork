const path = require('path');
const ExtractTextPlugin = require('extract-text-webpack-plugin');


module.exports = (env) => {
    const isProduction = env === 'production';
    const CSSExtract = new ExtractTextPlugin('styles.css');


    return {
        entry: path.join(__dirname, "src", "app.js"),
        // entry: "./src/app.js",
        // entry: "./src/playground/dev-play.js",
        output: {
            path: path.join(__dirname, "public", "dist"),
            filename: "bundle.js"
        },
        module: {
            rules: [{
                loader: "babel-loader",
                test: /\.js$/,
                exclude: /node_modules/
            }, {
                use: CSSExtract.extract({
                    use: [
                        {
                            loader: "css-loader",
                            options: {
                                sourceMap: true
                            }
                        },
                        {
                            loader: 'sass-loader',
                            options: {
                                sourceMap: true
                            }
                        }
                    ]
                }),
                test: /\.s?css$/
            }]
        },
        plugins: [CSSExtract],
        devtool: isProduction? 'source-map': 'inline-source-map',

        // Development server
        devServer: {
            contentBase: path.join(__dirname, "public"),
            historyApiFallback: true,
            publicPath: '/dist/'
        },

        resolve: {
            modules: ['app', 'node_modules'],
            extensions: [
                '.js',
                '.jsx',
                '.react.js',
            ],
            mainFields: [
                'jsnext:main',
                'main',
            ],
            alias: {
                // required for moment to work properly
                moment: 'moment/moment.js',
            }
        }

    }
};