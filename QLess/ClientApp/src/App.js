import React from 'react';
import Home from './components/Home'

const App = () => {
    return <>
        <div className="d-flex flex-column justify-content-center h-100">
            <div>
                <div className="mx-auto" style={{ maxWidth: '750px' }}>
                    <Home />
                </div>
            </div>
        </div>
    </>;
};

export default App;
