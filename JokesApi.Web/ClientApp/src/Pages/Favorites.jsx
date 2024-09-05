import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Favorites = () => {

    const [favorites, setFavorites] = useState([{
        id: '',
        setUp: '',
        punchLine: '',
        type: '',
        jokeId: '',
        note: '',
        showNote: false
    }]);
    const [editing, setEditing] = useState([{
        jokeId: '',
        note: ''
    }]);

    const loadData = async () => {
        const { data } = await axios.get('/api/jokes/getall');
        setFavorites(data);
    };

    useEffect(() => {

        loadData();

    }, []);

    function caps(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }

    const onRemoveClick = async jokeId => {
        await axios.post('/api/jokes/remove', { Int: jokeId });
        loadData();
    }

    const onShowClick = (index) => {
        const copy = [...favorites];
        let joke = { ...copy[index] };
        joke.showNote = !joke.showNote;
        copy[index] = joke;
        setFavorites(copy);
        onCancelClick(joke.jokeId);
    }

    const onEditClick = (jokeId, note) => {
        const copy = [...editing];
        copy.push({ jokeId, note });
        setEditing(copy);
    }

    const onCancelClick = (jokeId) => {
        const copy = editing.filter(e => e.jokeId !== jokeId);
        setEditing(copy);
    }

    const onNoteChange = (jokeId, e) => {
        let joke = editing.find(e => e.jokeId === jokeId);
        joke.note = e.target.value;
        const copy = editing.filter(e => e.jokeId !== jokeId);
        copy.push(joke);
        setEditing(copy);
    };

    const onSaveClick = async (jokeId) => {
        const joke = editing.find(e => e.jokeId === jokeId);
        await axios.post('/api/jokes/update', joke);
        loadData();
    }

    return (
        <div className='container' style={{ marginTop: 80 }}>
            {!favorites ? <h1>You have not favored any jokes yet.</h1> :
                <div>
                    <h1>These are your {favorites.length} favorites</h1>
                    <hr />
                    <br />
                    <div className='row'>
                        {favorites.map((f, index) =>
                            <div className='card col-3' key={f.id}>
                                <div className="card-header">
                                    <h6 className="card-title">Joke #{f.jokeId} - {caps(f.type)}
                                        <span>
                                            <button className='btn btn-danger' onClick={_ => onRemoveClick(f.jokeId)}>
                                                <i className="bi bi-trash"></i>
                                            </button>
                                        </span>
                                    </h6>
                                </div>
                                <div className='card-body'>
                                    <p className="card-text">{f.setUp}</p>
                                    <br />
                                    <h6>{f.punchLine}</h6>
                                </div>
                                <div className="card-footer"><br />
                                    {f.showNote && <>
                                        {!editing.find(e => e.jokeId === f.jokeId) ?
                                            <>
                                                <h6>{f.note ? f.note : 'No note yet'}</h6>
                                                <button className='btn btn-info w-100' onClick={_ => onEditClick(f.jokeId, f.note)}>Edit</button>
                                            </> : <>
                                                <textarea type='text' name='note' className='form-control' rows='3' value={editing.find(e => e.jokeId === f.jokeId).note} onChange={e => onNoteChange(f.jokeId, e)}></textarea>
                                                <button className='btn btn-warning w-50 mt-2' onClick={_ => onSaveClick(f.jokeId)}>Save</button>
                                                <button className='btn btn-dark w-50 mt-2' onClick={_ => onCancelClick(f.jokeId)}>Cancel</button>
                                        </>}
                                    </>}
                                    <button className='btn btn-success w-100 mt-2' data-bs-toggle="button" onClick={_ => onShowClick(index)}>{f.showNote ? 'Hide Note' : 'Show Note'}</button>
                                </div>
                            </div>
                        )}
                    </div>
                </div>}
        </div>
    )

};

export default Favorites;