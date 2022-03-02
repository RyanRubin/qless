import React, { useState } from 'react';
import BuyNewModal from './BuyNewModal';
import CheckBalanceModal from './CheckBalanceModal';
import ReloadCardModal from './ReloadCardModal';
import DiscountedCardRegistrationModal from './DiscountedCardRegistrationModal';
import TapInModal from './TapInModal';
import TapOutModal from './TapOutModal';

const Home = () => {
    const [showModal, setShowModal] = useState('');
    const [cardId, setCardId] = useState('');

    return <>
        <div className="p-1 text-center">
            <h1 className="display-1">Q-LESS<br /><small className="text-muted">Transport</small></h1>
        </div>
        <div className="p-1">
            <div className="card">
                <div className="p-1 card-header text-center">
                    <h2>Kiosk</h2>
                </div>
                <div className="p-1 card-body">
                    <div className="d-flex">
                        <div className="p-1 flex-fill">
                            <button type="button" className="btn btn-secondary btn-lg btn-block p-4" onClick={() => setShowModal('BUY_NEW')}>Buy New</button>
                        </div>
                        <div className="p-1 flex-fill">
                            <button type="button" className="btn btn-secondary btn-lg btn-block p-4" onClick={() => setShowModal('CHECK_BALANCE')}>Check Balance</button>
                        </div>
                    </div>
                    <div className="d-flex">
                        <div className="p-1 flex-fill">
                            <button type="button" className="btn btn-secondary btn-lg btn-block p-4" onClick={() => setShowModal('RELOAD_CARD')}>Reload Card</button>
                        </div>
                        <div className="p-1 flex-fill">
                            <button type="button" className="btn btn-secondary btn-lg btn-block p-4" onClick={() => setShowModal('DISCOUNTED_CARD_REGISTRATION')}>Discounted Card Registration</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div className="p-1">
            <div className="card">
                <div className="p-1 card-header text-center">
                    <h2>Turnstile</h2>
                </div>
                <div className="p-1 card-body">
                    <div className="d-flex">
                        <div className="p-1 flex-fill">
                            <button type="button" className="btn btn-secondary btn-lg btn-block p-4" onClick={() => setShowModal('TAP_IN')}>Tap In</button>
                        </div>
                        <div className="p-1 flex-fill">
                            <button type="button" className="btn btn-secondary btn-lg btn-block p-4" onClick={() => setShowModal('TAP_OUT')}>Tap Out</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        {showModal === 'BUY_NEW' && <BuyNewModal
            setShowModal={setShowModal}
            cardId={cardId}
            setCardId={setCardId}
        />}
        {showModal === 'CHECK_BALANCE' && <CheckBalanceModal
            setShowModal={setShowModal}
            cardId={cardId}
            setCardId={setCardId}
        />}
        {showModal === 'RELOAD_CARD' && <ReloadCardModal
            setShowModal={setShowModal}
            cardId={cardId}
            setCardId={setCardId}
        />}
        {showModal === 'DISCOUNTED_CARD_REGISTRATION' && <DiscountedCardRegistrationModal
            setShowModal={setShowModal}
            cardId={cardId}
            setCardId={setCardId}
        />}
        {showModal === 'TAP_IN' && <TapInModal
            setShowModal={setShowModal}
            cardId={cardId}
            setCardId={setCardId}
        />}
        {showModal === 'TAP_OUT' && <TapOutModal
            setShowModal={setShowModal}
            cardId={cardId}
            setCardId={setCardId}
        />}
    </>;
};

export default Home;
