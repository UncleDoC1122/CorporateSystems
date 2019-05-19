import React, { Component } from 'react';
import './TroopTypesGrid.css';

export class TroopTypesGrid extends Component {

    constructor(props) {
		super(props);
		this.state = {
			data: null,
			loading: true,
			order: '',
			orderby: '&$orderby=Id asc',
			filters: false,
			filterby: [],
			pageNo: 1,
			recordsPerPage: 25,
			recordsCount: 0,
			nameFilterValue: '',
			kindsFilterValue: ''
		};

		this.fetchData(1);

		this.handleNameFilterChange = this.handleNameFilterChange.bind(this);
		this.handleKindsFilterChange = this.handleKindsFilterChange.bind(this);
	}

	handleNameFilterChange(event) {
    this.setState({nameFilterValue: event.target.value});
	}
	
	handleKindsFilterChange(event) {
		this.setState({kindsFilterValue: event.target.value});
	}

	fetchData(pageNo) {
		var filters = '';

		this.state.filterby.map((f, index) => {
			index == 0 ? 
				filters = filters + '&$filter=contains(' + f[0] + ', \'' + f[1] + '\')'
				: filters = filters + 'and contains(' + f[0] + ', \'' + f[1] + '\')';
		});

		fetch('odata/TroopTypes?$expand=TroopKind&$skip=' + ((pageNo - 1) * this.state.recordsPerPage)
			+ '&$top=' + this.state.recordsPerPage
			+ '&$count=true'
			+ filters
			+ this.state.orderby)
			.then(response => response.json())
			.then(data => {
				this.setState({ 
					pageNo: pageNo,
					data: data,
					recordsCount: data["@odata.count"],
					loading: false });
			});
	} 

	renderTable() {
		var data = this.state.data;
		if (data == null) {
			return;
		}
		return (
			<table className='table'>
			  <thead>
				<tr>
				  <th onClick={() => this.enableSorting('&$orderby=Id')}>Идентификатор</th>
				  <th onClick={() => this.enableSorting('&$orderby=Name')}>Название</th>
				  <th onClick={() => this.enableSorting('&$orderby=TroopKind/Name')}>Вид войск</th>
				</tr>
			  </thead>
			  <tbody>
				{data['value'].map(forecast =>
				  <tr key={forecast.Id}>
					<td>{forecast.Id}</td>
					<td>{forecast.Name}</td>
					<td>{forecast.TroopKind.Name}</td>
				  </tr>
				)}
			  </tbody>
			</table>
		  );
	}

	enableSorting(column) {

		var columnSort = column;

		if (!this.state.orderby.includes('desc') && this.state.orderby.includes(columnSort))
		{
			columnSort = columnSort + ' desc';
		}

		this.setState({
			orderby: columnSort
		}, () => this.fetchData(1));
	}

	disableSorting() {
		this.setState({
			orderby: '&$orderby=Id asc'
		}, this.fetchData(1));
	}

	enableFiltering() {
		var filterQuery = [];

		if (this.state.nameFilterValue != '')
		{
			let query = ['Name', this.state.nameFilterValue];
			filterQuery.push(query);
		}

		if (this.state.kindsFilterValue != '')
		{
			let query = ['TroopKind/Name', this.state.kindsFilterValue];
			filterQuery.push(query);
		}

		this.setState({
			filters: true,
			filterby: this.state.filterby = filterQuery
		}, () => this.fetchData(1))
	}

	clearFilters() {
		this.setState({
			filters: false,
			filterby: [],
			kindsFilterValue: '',
			nameFilterValue: ''
		}, () => this.fetchData(1))
	}

	goToPage(pageNo) {
		this.fetchData(pageNo);
	}

	renderPaginator() {
		var pages = Math.ceil(this.state.recordsCount / this.state.recordsPerPage);
		var pagesArray = [];
		var currentPage = this.state.pageNo;

		for (var i = 0; i < pages; i ++) {
				pagesArray.push(i + 1);
		}

		return (
			<div className='pagination'>
				<button className='button' onClick={() => this.goToPage(1)}>
					{'<<'}
				</button>
				<button className='button' onClick={() => this.goToPage(currentPage == 1 ? 1 : currentPage - 1)}>
					{'<'}
				</button>
				{pagesArray.map(p => 
						<button 
							onClick={() => this.goToPage(p)}
							key={'page-' + p} 
							className={'button ' + (p == currentPage ? 'highlighted' : '')}
							>
								{p}
						</button>
				)}
				<button className='button' onClick={() => this.goToPage(currentPage == pages ? currentPage : currentPage + 1)}>
					{'>'}
				</button>
				<button className='button' onClick={() => this.goToPage(pages)}>
					{'>>'}
				</button>
			</div>
		)
	}

	renderFilters() {
		return (
			<div className="filters">
			<div className="filter">
				<div className="filterName">
					Название
				</div>
				<input className="filterBody" value={this.state.nameFilterValue} onChange={this.handleNameFilterChange}>
				</input>
			</div>
			<div className="filter">
				<div className="filterName">
					Вид войск
				</div>
				<input className="filterBody" value={this.state.kindsFilterValue} onChange={this.handleKindsFilterChange}>
				</input>
			</div>
			<button className="enableFilters filtersButton"
				onClick={() => this.enableFiltering()}>
				Применить фильтр
			</button>
			<button className="clearFilters filtersButton"
			onClick={() => this.clearFilters()}>
				Очистить фильтр
			</button>
		</div>
		)
	}

    render() {
        return (
            <div className='grid'>
				<h1 className='header'>Рода войск</h1>
				{this.renderFilters()}
				{this.renderTable()}
				{this.renderPaginator()}
            </div>
        );
    }
}
