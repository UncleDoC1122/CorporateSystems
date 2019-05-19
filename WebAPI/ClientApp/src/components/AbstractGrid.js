import React, { Component } from 'react';
import PropTypes from 'prop-types'

export class AbstractGrid extends Component {
	displayName = AbstractGrid.name

    constructor(props) {
        super(props);
		this.state = {
			forecasts: [],
			loading: true,
			order: false,
			orderby: '',
			filters: false,
			filterby: [],
			pageType: ''
		};

		fetch('odata/' + this.state.pageType)
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data, loading: false });
            });
    }

    static renderForecastsTable(forecasts) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Temp. (C)</th>
                        <th>Temp. (F)</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
					{forecasts.value.map(forecast =>
						<tr key={Object.values(forecast)[0]}>
							<td>{Object.values(forecast)[1]}</td>
                            //<td>{forecast.temperatureC}</td>
                            //<td>{forecast.temperatureF}</td>
                            //<td>{forecast.summary}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
			: AbstractGrid.renderForecastsTable(this.state.forecasts);

        return (
            <div>
                <h1>Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
            </div>
        );
    }
}
