import React, {useState, useEffect} from 'react';

export default function Auth() {
  const [state, setState] = useState({
    users: '',
  });

  function validateUser() {
    fetch(`/api/users`)
      .then((res) => res.json())
      .then((result) => setState({users: result.users}));
  }

  return (
    <>
      <h1>Auth</h1>
    </>
  );
}
