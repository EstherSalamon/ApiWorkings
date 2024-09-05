import React, {useState} from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Home.css';

const Home = () => {
    
    return (
        <div className="app-container">
            <div className="d-flex flex-column justify-content-center align-items-center">
                <h1>Welcome to the most awesome Jokes Site!</h1>
                <a href='/search' className='btn btn-primary'>Click here to search</a>
            </div>
        </div>
    );
};

export default Home;