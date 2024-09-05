import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useAuthentication } from '../AuthenticationContext';

const SearchJokes = () => {

    const [jokes, setJokes] = useState([]);
    const [type, setType] = useState('');
    const [amount, setAmount] = useState('1');
    const { user } = useAuthentication();
    const [faves, setFaves] = useState([]);
    const [loading, setLoading] = useState(false);

    const loadData = async () => {
        if (!user) {
            return;
        } else {
            const { data } = await axios.get('/api/jokes/getall');
            const array = data.map(j => {
                return j.jokeId;
            });
            setFaves(array);
        }
    };

    useEffect(() => {

        if (user) {
            loadData();
        } else {
            setFaves(null);
        }


    }, []);

    const onSearchClick = async () => {
        setLoading(true);
        const { data } = await axios.get(`/api/jokes/search?amount=${amount}&type=${type}`);
        setJokes(data);
        setLoading(false);
    }

    function caps(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }

    function getGuid() {
        return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
            (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
        );
    }

    const markFavorite = async joke => {
        if (faves.includes(joke.id)) {
            await axios.post('/api/jokes/remove', { Int: joke.id });
            loadData();
        } else {
            await axios.post('/api/jokes/addtofave', joke);
            loadData();
        }
    }

    const getMessage = id => {
        if (!user) {
            return "Sign in to Add to Favorites";
        } else if (faves.includes(id)) {
            return "Remove from Favorites";
        } else {
            return "Add to Favorites";
        }
    }

    return (
        <div className='container' style={{ marginTop: 80 }}>
            <h1>This is where you can search for jokes!</h1>
            <h6>Note: There may be a limited amount of specific types jokes. Please bear with us.</h6>
            <hr />
            <div className='row'>
                <div className='col-md-2 offset-6'>
                    <select className='form-control' value={type} onChange={e => setType(e.target.value)}>
                        <option value='-1'>--Choose--</option>
                        <option value='dad'>Dad jokes</option>
                        <option value='general'>General</option>
                        <option value='knock-knock'>Knock Knock</option>
                        <option value='programming'>Programming</option>
                        <option value='random'>Random</option>
                    </select>
                </div>
                <div className='col-md-2'>
                    <input type='number' min='1' max='52' className='form-control' value={amount} onChange={e => setAmount(e.target.value)} placeholder="Amount" />
                </div>
                <div className='col-md-2'>
                    <button className='btn btn-primary w-100' disabled={type === '-1' || type === ''} onClick={onSearchClick}>{loading ? 'Loading...' : 'Search'}</button>
                </div>
            </div>
            <br />
            <div name='cards'>
                <div className="row">
                    {!!jokes.length && jokes.map(j =>
                        <div className="card col-3" key={getGuid()}>
                            <div className="card-body">
                                <h5 className="card-title">Joke #{j.id} - {caps(j.type)}</h5>
                                <p className="card-text">{caps(j.setup)}</p>
                                <br />
                                <h6>{caps(j.punchline)}</h6>
                            </div>
                            <div className="card-footer">
                                <button className={`btn btn-${faves && faves.includes(j.id) ? 'danger' : 'success'} w-100`} disabled={!user} onClick={_ => markFavorite(j)}>{getMessage(j.id)}</button>
                            </div>
                        </div>
                    )}
                </div>
            </div>
        </div>
    )
};

export default SearchJokes;