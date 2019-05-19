import React, { Component } from 'react';
import './Login.css'

export class Login extends Component {

  constructor(props) {
    super(props);
    this.state = { username: '', password: ''};

    this.handleLoginChange = this.handleLoginChange.bind(this);
    this.handleUsernameChange = this.handleUsernameChange.bind(this);
    this.loginCallback = this.loginCallback.bind(this);
    this.login = this.login.bind(this);
  }

  login() {
    var username = this.state.username;
    var password = this.state.password;
    fetch('api/Login?username=' + username + '&password=' + password, {method: 'POST'})
      .then(response => response.json())
      .then(data => {
        if (data != 'Failed') 
            this.loginCallback(data)
      });
  }

  loginCallback(user) {
    this.props.loginCallback(user);
  }

  handleLoginChange(event) {
    this.setState({username: event.target.value});
	}
	
	handleUsernameChange(event) {
		this.setState({password: event.target.value});
  }

  render() {
    return (
      <div className='loginForm'>
        <h1>Вход в систему</h1>

        <div className='login'>
            Логин
            <input className='loginInput' onChange={this.handleLoginChange}>
            </input>
        </div>

        <div className='password'>
            Пароль
            <input className='passwordInput' type='password' onChange={this.handleUsernameChange}>
            </input>
        </div>

        <button onClick={this.login}>Increment</button>
      </div>
    );
  }
}
