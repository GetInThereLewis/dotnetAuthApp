import React, {useState, useEffect} from 'react';
import {Form, FormGroup, Button, Label, Input} from 'reactstrap';
import './register.css';
export default function Register() {
  const [input, setInput] = useState({
    username: '',
    email: '',
    password: '',
    password2: '',
  });

  const handleSubmit = (e) => {
    e.preventDefault();

    if (input.username && input.email && input.password2 && input.password) {
      const userData = {
        username: input.username,
        email: input.email,
        password: input.password,
        password2: input.password2,
      };
      const url = 'https://localhost:7000/api/user/register';
      fetch(url, {
        metehod: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(userData),
      })
        .then((response) => response.json())
        .then((data) => {
          console.log(data);
        });
    }
  };
  return (
    <>
      <Form onSubmit={handleSubmit}>
        <FormGroup controllId="username">
          <Label>Username</Label>
          <Input
            type="text"
            placeholder="Enter username"
            value={input.username}
            onChange={(e) => setInput({...input, username: e.target.value})}
          />
        </FormGroup>
        <FormGroup controllId="mail">
          <Label>E-Mail</Label>
          <Input
            type="E-Mail"
            placeholder="mail@mail.mail"
            value={input.email}
            onChange={(e) => setInput({...input, email: e.target.value})}
          ></Input>
        </FormGroup>
        <FormGroup controllId="Password">
          <Label>Password</Label>
          <Input
            type="Password"
            value={input.password}
            onChange={(e) => setInput({...input, email: e.target.value})}
          ></Input>
        </FormGroup>
        <FormGroup controllId="Password">
          <Label>Confirm Password</Label>
          <Input
            type="Password"
            value={input.password2}
            onChange={(e) => setInput({...input, email: e.target.value})}
          ></Input>
        </FormGroup>
        <Button variant="primary" type="submit">
          Submit
        </Button>
      </Form>
    </>
  );
}
