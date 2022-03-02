import React, { useState, useEffect } from 'react';

const TapOutModal = ({ setShowModal, cardId, setCardId }) => {
    const [transportLines, setTransportLines] = useState([]);
    const [transportLineId, setTransportLineId] = useState('');
    const [transportStations, setTransportStations] = useState([]);
    const [transportStationId, setTransportStationId] = useState('');
    const [cardBalance, setCardBalance] = useState('');

    const populateLines = async () => {
        const lines = await window.appFetch('transportlines', 'GET');
        setTransportLines(lines);
        setTransportLineId(lines[0].id);
        await populateStations(lines[0].id);
    };

    const populateStations = async (lineId) => {
        const stations = await window.appFetch(`transportstations/${lineId}/line`, 'GET');
        setTransportStations(stations);
        setTransportStationId(stations[0].id);
    };

    useEffect(() => {
        populateLines();
    }, []);

    const transportLineChange = async (e) => {
        setTransportLineId(e.target.value);
        await populateStations(e.target.value);
    };

    const tapOut = async () => {
        if (!cardId) {
            alert('Card ID required.');
            return;
        }
        const balance = await window.appFetch(`transportcards/${cardId}/tapout/${transportLineId}/${transportStationId}`, 'POST');
        setCardBalance(balance);
    };

    return <>
        <div className="modal show d-block" tabIndex="-1">
            <div className="modal-dialog modal-dialog-centered">
                <div className="modal-content">
                    <div className="modal-header">
                        <h3>Tap Out Transport Card</h3>
                    </div>
                    <div className="modal-body">
                        <div className="p-1 form-group">
                            <label htmlFor="cardId">Card ID</label>
                            <input type="text" id="cardId" className="form-control" value={cardId} onChange={(e) => setCardId(e.target.value)} />
                        </div>
                        <div className="p-1 form-group">
                            <label htmlFor="transportLine">Transport Line</label>
                            <select id="transportLine" className="form-control" value={transportLineId} onChange={transportLineChange}>
                                {transportLines.map((transportLine) => <option key={transportLine.id} value={transportLine.id}>{transportLine.name}</option>)}
                            </select>
                        </div>
                        <div className="p-1 form-group">
                            <label htmlFor="transportStation">Transport Station (select transport line to populate this)</label>
                            <select id="transportStation" className="form-control" value={transportStationId} onChange={(e) => setTransportStationId(e.target.value)}>
                                {transportStations.map((transportStation) => <option key={transportStation.id} value={transportStation.id}>{transportStation.name}</option>)}
                            </select>
                        </div>
                        {cardBalance && <>
                            <div className="p-1 text-center">
                                New Card Balance
                            </div>
                            <div className="p-1 text-center">
                                <h1 className="display-1">P {cardBalance.toFixed(2)}</h1>
                            </div>
                        </>}
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" onClick={tapOut}>Tap Out</button>
                        <button type="button" className="btn btn-outline-secondary" onClick={() => setShowModal('')}>Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div className="modal-backdrop show"></div>
    </>;
};

export default TapOutModal;
