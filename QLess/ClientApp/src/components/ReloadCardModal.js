import React, { useState } from 'react';

const ReloadCardModal = ({ setShowModal, cardId, setCardId }) => {
    const CASH_VALUE_VALIDATION_DEFAULT = 'Cash Value must be greater or equal to Load Amount.';

    const [cashValue, setCashValue] = useState(0);
    const [cashValueValidation, setCashValueValidation] = useState(CASH_VALUE_VALIDATION_DEFAULT);
    const [loadAmount, setLoadAmount] = useState(100);
    const [change, setChange] = useState(0);
    const [cardBalance, setCardBalance] = useState('');
    const [isDisableActionButton, setIsDisableActionButton] = useState(false);

    const doSetCashValueValidation = (changeVal) => {
        if (changeVal < 0) {
            setCashValueValidation(CASH_VALUE_VALIDATION_DEFAULT);
        } else {
            setCashValueValidation('');
        }
    };

    const doSetCashValue = (val, isGuard) => {
        let value = parseFloat(val);
        if (isGuard) {
            if (value < 0) {
                value = 0;
            }
        }
        setCashValue(value);
        const changeVal = value - loadAmount;
        setChange(changeVal);
        doSetCashValueValidation(changeVal);
    };

    const doSetLoadAmount = (val, isGuard) => {
        let amount = parseFloat(val);
        if (isGuard) {
            if (amount < 100) {
                amount = 100;
            }
            if (amount > 10000) {
                amount = 10000;
            }
        }
        setLoadAmount(amount);
        const changeVal = cashValue - amount;
        setChange(changeVal);
        doSetCashValueValidation(changeVal);
    };

    const reloadCard = async () => {
        if (!cardId) {
            alert('Card ID required.');
            return;
        }
        const balance = await window.appFetch(`transportcards/${cardId}/reload/${loadAmount}`, 'POST');
        setCardBalance(balance);
        setIsDisableActionButton(true);
    };

    return <>
        <div className="modal show d-block" tabIndex="-1">
            <div className="modal-dialog modal-dialog-centered">
                <div className="modal-content">
                    <div className="modal-header">
                        <h3>Reload Transport Card</h3>
                    </div>
                    <div className="modal-body">
                        <div className="p-1 form-group">
                            <label htmlFor="cardId">Card ID</label>
                            <input type="text" id="cardId" className="form-control" value={cardId} onChange={(e) => setCardId(e.target.value)} />
                        </div>
                        <div className="p-1 form-group">
                            <label htmlFor="cashValue">Cash Value</label>
                            <div className="input-group">
                                <div className="input-group-prepend">
                                    <span className="input-group-text">P</span>
                                </div>
                                <input type="number" id="cashValue" className="form-control" min="0" value={cashValue} onChange={(e) => doSetCashValue(e.target.value)} onBlur={(e) => doSetCashValue(e.target.value, true)} />
                            </div>
                            {cashValueValidation && <div className="text-danger">{cashValueValidation}</div>}
                        </div>
                        <div className="p-1 form-group">
                            <label htmlFor="loadAmount">Load Amount</label>
                            <div className="input-group">
                                <div className="input-group-prepend">
                                    <span className="input-group-text">P</span>
                                </div>
                                <input type="number" id="loadAmount" className="form-control" min="100" max="10000" value={loadAmount} onChange={(e) => doSetLoadAmount(e.target.value)} onBlur={(e) => doSetLoadAmount(e.target.value, true)} />
                            </div>
                        </div>
                        <div className="p-1 form-group">
                            <label htmlFor="change">Change</label>
                            <div className="input-group">
                                <div className="input-group-prepend">
                                    <span className="input-group-text">P</span>
                                </div>
                                <input type="number" id="change" className="form-control" readOnly value={change} />
                            </div>
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
                        <button type="button" className="btn btn-secondary" disabled={isDisableActionButton || cashValueValidation} onClick={reloadCard}>Reload Card</button>
                        <button type="button" className="btn btn-outline-secondary" onClick={() => setShowModal('')}>Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div className="modal-backdrop show"></div>
    </>;
};

export default ReloadCardModal;
