import React, { useState } from 'react';

const CheckBalanceModal = ({ setShowModal, cardId, setCardId }) => {
    const [cardBalance, setCardBalance] = useState('');

    const checkBalance = async () => {
        if (!cardId) {
            alert('Card ID required.');
            return;
        }
        const balance = await window.appFetch(`transportcards/${cardId}/balance`, 'GET');
        setCardBalance(balance);
    };

    return <>
        <div className="modal show d-block" tabIndex="-1">
            <div className="modal-dialog modal-dialog-centered">
                <div className="modal-content">
                    <div className="modal-header">
                        <h3>Check Balance of Transport Card</h3>
                    </div>
                    <div className="modal-body">
                        <div className="p-1 form-group">
                            <label htmlFor="cardId">Card ID</label>
                            <input type="text" id="cardId" className="form-control" value={cardId} onChange={(e) => setCardId(e.target.value)} />
                        </div>
                        {cardBalance && <>
                            <div className="p-1 text-center">
                                Card Balance
                            </div>
                            <div className="p-1 text-center">
                                <h1 className="display-1">P {cardBalance.toFixed(2)}</h1>
                            </div>
                        </>}
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" onClick={checkBalance}>Check Balance</button>
                        <button type="button" className="btn btn-outline-secondary" onClick={() => setShowModal('')}>Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div className="modal-backdrop show"></div>
    </>;
};

export default CheckBalanceModal;
