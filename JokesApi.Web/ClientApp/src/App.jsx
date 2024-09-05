import React from 'react';
import { Route, Routes } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './Pages/Home';
import { AuthenticationComponent } from './AuthenticationContext';
import SignUp from './Pages/SignUp';
import LogIn from './Pages/LogIn';
import LogOut from './Pages/LogOut';
import PrivateRoute from './components/PrivateRoute';
import SearchJokes from './Pages/SearchJokes';
import Favorites from './Pages/Favorites';

const App = () => {
    return (
        <AuthenticationComponent>
            <Layout>
                <Routes>
                    <Route path='/' element={<Home />} />
                    <Route path='/search' element={<SearchJokes />} />
                    <Route path='/signup' element={<SignUp />} />
                    <Route path='/login' element={<LogIn />} />
                    <Route path='/logout' element={<PrivateRoute><LogOut /></PrivateRoute>} />
                    <Route path='/faves' element={<PrivateRoute><Favorites /></PrivateRoute>} />
                </Routes>
            </Layout>
        </AuthenticationComponent>
    );
}

export default App;