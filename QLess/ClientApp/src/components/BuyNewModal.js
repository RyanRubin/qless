import React, { useState, useEffect } from 'react';

const BuyNewModal = ({ setShowModal, cardId, setCardId }) => {
    const [cardBalance, setCardBalance] = useState('');
    const [isDisableActionButton, setIsDisableActionButton] = useState(false);

    useEffect(() => {
        setCardId('');
    }, []);

    const buyNew = async () => {
        const { id, balance } = await window.appFetch('transportcards', 'POST');
        setCardId(id);
        setCardBalance(balance);
        setIsDisableActionButton(true);
    };

    return <>
        <div className="modal show d-block" tabIndex="-1">
            <div className="modal-dialog modal-dialog-centered">
                <div className="modal-content">
                    <div className="modal-header">
                        <h3>Buy New Transport Card</h3>
                    </div>
                    <div className="modal-body">
                        <div className="p-1 form-group">
                            <label htmlFor="cardId">Card ID</label>
                            <input type="text" id="cardId" className="form-control" readOnly value={cardId} />
                        </div>
                        {cardId && <div className="p-1">
                            <div className="alert alert-info">
                                Card ID saved to app state and will be auto-populated in other forms.
                            </div>
                        </div>}
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
                        <button type="button" className="btn btn-secondary" disabled={isDisableActionButton} onClick={buyNew}>Buy New</button>
                        <button type="button" className="btn btn-outline-secondary" onClick={() => setShowModal('')}>Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div className="modal-backdrop show"></div>
    </>;
};

export default BuyNewModal;
