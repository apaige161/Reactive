import React, { Component } from "react";
import "./App.css";
import axios from "axios";
import { Header, Icon, List } from "semantic-ui-react";
import "semantic-ui-css/semantic.min.css";

class App extends Component {
  //state
  state = {
    values: [],
  };

  //get data from api
  //update state
  componentDidMount() {
    //get from api
    //returns a promise
    axios.get("http://localhost:5000/api/values").then((response) => {
      //add things in the state
      this.setState({
        //set the response = values to use in the render method 
        values: response.data,
      });
    });
  }

  render() {
    return (
      <div>
        <Header as="h2">
          <Icon name="users" />
          <Header.Content>Reactive</Header.Content>
        </Header>
        <List>
          {this.state.values.map((value: any) => (
            <List.Item key={value.Id}>{value.name}</List.Item>
          ))}
        </List>
      </div>
    );
  }
}

export default App;
