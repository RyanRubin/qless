import React, { useState } from 'react';

const DiscountedCardRegistrationModal = ({ setShowModal, cardId, setCardId }) => {
    const [seniorCitizenControlNumber, setSeniorCitizenControlNumber] = useState('');
    const [pwdIdNumber, setPwdIdNumber] = useState('');
    const [isDisableActionButton, setIsDisableActionButton] = useState(false);

    const register = async () => {
        if (!cardId) {
            alert('Card ID required.');
            return;
        }
        await window.appFetch(`transportcards/${cardId}/regdiscount?seniorCitizenControlNumber=${seniorCitizenControlNumber}&pwdIdNumber=${pwdIdNumber}`, 'POST');
        setIsDisableActionButton(true);
        alert('Register successful.');
    };

    return <>
        <div className="modal show d-block" tabIndex="-1">
            <div className="modal-dialog modal-dialog-centered">
                <div className="modal-content">
                    <div className="modal-header">
                        <h3>Discounted Transport Card Registration</h3>
                    </div>
                    <div className="modal-body">
                        <div className="p-1 form-group">
                            <label htmlFor="cardId">Card ID</label>
                            <input type="text" id="cardId" className="form-control" value={cardId} onChange={(e) => setCardId(e.target.value)} />
                        </div>
                        <div className="p-1">
                            <div className="alert alert-info">
                                Please provide either Senior Citizen Control Number or PWD ID Number.
                            </div>
                        </div>
                        <div className="p-1 form-group">
                            <label htmlFor="seniorCitizenControlNumber">Senior Citizen Control Number</label>
                            <input type="text" id="seniorCitizenControlNumber" className="form-control" value={seniorCitizenControlNumber} onChange={(e) => setSeniorCitizenControlNumber(e.target.value)} />
                        </div>
                        <div className="p-1 form-group">
                            <label htmlFor="pwdIdNumber">PWD ID Number</label>
                            <input type="text" id="pwdIdNumber" className="form-control" value={pwdIdNumber} onChange={(e) => setPwdIdNumber(e.target.value)} />
                        </div>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" disabled={isDisableActionButton || (!seniorCitizenControlNumber && !pwdIdNumber)} onClick={register}>Register</button>
                        <button type="button" className="btn btn-outline-secondary" onClick={() => setShowModal('')}>Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div className="modal-backdrop show"></div>
    </>;
};

export default DiscountedCardRegistrationModal;
