import React, { Component } from 'react';
import './UserPage.css'

export class UserPage extends Component {

  constructor(props) {
    super(props);
    let userId = localStorage.getItem('loggedUserId');

    this.state = { 
        userId: userId,
        username: '', 
        readOnly: true,
        lastName: '',
        firstName: '',
        middleName: '',
        email: '',
        password: '',
        passwordChange: false
    };

    this.fetchData(userId);

    this.handleLoginChange = this.handleLoginChange.bind(this);
    this.handleLastNameChange = this.handleLastNameChange.bind(this);
    this.handleFirstNameChange = this.handleFirstNameChange.bind(this);
    this.handleMiddleNameChange = this.handleMiddleNameChange.bind(this);
    this.handleEmailChange = this.handleEmailChange.bind(this);
    this.handlePasswordChange = this.handlePasswordChange.bind(this);
    this.saveChanges = this.saveChanges.bind(this);
  }

  fetchData(userId) {
		fetch('api/users/' + userId)
			.then(response => response.json())
			.then(data => {
				this.setState({ 
					username: data.userName,
					lastName: data.lastName,
          firstName: data.firstName,
          middleName: data.middleName,
          email: data.email});
			});
	} 

  startUserFieldsEditing() {
    this.setState({readOnly: false})
  }

  startPasswordEditing() {
    this.setState({passwordChange: true})
  }

  cancelPasswordEditing() {
    this.setState({passwordChange: false})
  }

  savePassword() {
    let userId = localStorage.getItem('loggedUserId');
    const formData = this.state.password

    fetch('api/Users/change-password/' + userId + '/' + formData, {
        method: 'PUT', 
      })
			.then(response => response.json())
			.then(data => {
				this.setState({ 
          readOnly: true,
					passwordChange: false});
			});
  }

  saveChanges() {
    let userId = localStorage.getItem('loggedUserId');
    const formData = {
      userName: this.state.username,
      email: this.state.email,
      lastName: this.state.lastName,
      firstName: this.state.firstName,
      middleName: this.state.middleName,
      password: ''
    }

    fetch('api/Users/' + userId, {
      method: 'PUT', 
      body: JSON.stringify(formData),
      headers: new Headers({
        'Content-Type': 'application/json'
        }),
      })
			.then(response => response.json())
			.then(data => {
				this.setState({ 
          readOnly: true,
					username: data.userName,
					lastName: data.lastName,
          firstName: data.firstName,
          middleName: data.middleName,
          email: data.email});
			});
  }

  handleLoginChange(event) {
    this.setState({username: event.target.value});
	}
	
	handleLastNameChange(event) {
		this.setState({lastName: event.target.value});
  }
  
  handleFirstNameChange(event) {
    this.setState({firstName: event.target.value});
	}
	
	handleMiddleNameChange(event) {
		this.setState({middleName: event.target.value});
  }
  
  handleEmailChange(event) {
    this.setState({email: event.target.value});
  }
  
  handlePasswordChange(event) {
    this.setState({password: event.target.value});
	}

  render() {
    let readOnly = this.state.readOnly;
    let className = this.state.readOnly ? 'loginInput' : 'loginInput disabledInput'
    return (
      <div className='userPage'>
        <h1 style={{'align-self': 'center'}}>Данные о текущем пользователе</h1>

        {this.state.passwordChange ? 
            <div className='inputs'>
              <div className='dataField'>
                  Новый пароль:
                  <input className={className} value={this.state.password} onChange={this.handlePasswordChange}>
                  </input>
              </div>
              <button className='editButton' onClick={() => this.savePassword()}>
                Сохранить новый пароль
              </button>
              <button className='editButton' onClick={() => this.cancelPasswordEditing()}>
                Отмена
              </button>
            </div>
            :
            <div className='inputs'>
            <div className='dataField'>
                Логин:
                <input className={className} readOnly={readOnly} value={this.state.username} onChange={this.handleLoginChange}>
                </input>
            </div>
            <div className='dataField'>
                E-mail:
                <input className={className} readOnly={readOnly} value={this.state.email} onChange={this.handleEmailChange}>
                </input>
            </div>
            <div className='dataField'>
                Фамилия:
                <input className={className} readOnly={readOnly} value={this.state.lastName} onChange={this.handleLastNameChange}>
                </input>
            </div>
            <div className='dataField'>
                Имя:
                <input className={className} readOnly={readOnly} value={this.state.firstName} onChange={this.handleFirstNameChange}>
                </input>
            </div>
            <div className='dataField'>
                Отчество:
                <input className={className} readOnly={readOnly} value={this.state.middleName} onChange={this.handleMiddleNameChange}>
                </input>
            </div>
          </div>
        }

          {!this.state.passwordChange ? 
            <div className='buttonGroup'>
              {this.state.readOnly ? 
                <button className='editButton' onClick={() => this.startUserFieldsEditing()}>
                  Редактировать данные
                </button> :
                <button className='editButton' onClick={() => this.saveChanges()}>
                  Сохранить данные
                </button>
              }
              <button className='editButton' onClick={() => this.startPasswordEditing()}>
                Изменить пароль
              </button>
            </div> 
            :
            null
          }
          
        </div>
    );
  }
}
