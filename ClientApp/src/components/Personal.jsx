import React from 'react';
import {useState, useEffect} from 'react';
export default function Personal() {
  const [display, setDisplay] = useState({
    exercises: [],
    username: '',
    error: false,
  });
  const ifValid = () => {
    try {
      const url = 'https://localhost:7000/api/';
      fetch(url, {method: 'GET'})
        .then((response) => response.json())
        .then((data) => {
          console.log(data);
        });
    } catch (error) {
      console.error(error, 'Error occured');
      setDisplay({...display, error: true});
    }
  };
  return (
    <>
      <h1>{display.username}</h1>

      <div>
        {display.exercises.map((exercise) => (
          <li key={exercise.id}>{exercise}</li>
        ))}
      </div>
    </>
  );
}
