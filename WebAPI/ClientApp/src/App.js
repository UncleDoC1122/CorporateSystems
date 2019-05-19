import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { TroopTypesGrid } from './components/Grids/TroopTypesGrid';
import { TroopKindsGrid } from './components/Grids/TroopKindsGrid';
import { Login } from './Login'
import { UserPage } from './components/UserPage' 

export default class App extends Component {
	displayName = App.name

	constructor(props) {
		super(props);
		this.state = {userName: ''};
		this.login = this.login.bind(this);
		this.logout = this.logout.bind(this);
	  }

	login(user) {
		localStorage.setItem('loggedUserId', user.userId);
		localStorage.setItem('loggedUserName', user.username);
		localStorage.setItem('loggedUserToken', user.token);
		localStorage.setItem('loggedUserIsAdmin', user.isAdmin);
		this.setState({userName: user.username});
	}

	logout() {
		localStorage.clear()
		this.setState({userName: ''});
	}

	render() {
		let isUserLoggedIn = localStorage.getItem('loggedUserToken') == null ? false : true
		return ( 
			!isUserLoggedIn ? 
				<Login loginCallback={(user) => this.login(user)}>
				</Login>
			:
				<Layout>
					<Route exact path='/' component={Home} />
					<Route path='/counter' component={Counter} />
					<Route path='/fetchdata' component={FetchData} />
					<Route path='/trooptypespage' component={TroopTypesGrid} />
					<Route path='/troopkindspage' component={TroopKindsGrid} />
					<Route path='/userpage' component={UserPage} />
					<div className='disabledDiv'>
						{this.state.userName}
					</div>
				</Layout>
		);
	}
}
