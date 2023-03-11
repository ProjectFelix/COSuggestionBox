import React, { useState, useEffect } from 'react';

export const Home = () => {
    const [state, setState] = useState({
        text: 'Loading...',
        loading: true
    })

    useEffect(() => {
        const getLoginData = async () => {
            let response = await fetch("api/suggestion");
            if (response.ok) {
                let data = await response.text();
                setState(p => ({...p, loading: false, text: data}))
            }
        }
        getLoginData();
    }, [])

    useEffect(() => {
        console.log(state.text);
    }, [state])

    const handleCommentUpdate = e => {
        if (e.target.value > 256) return;
        setState(p => ({ ...p, text: e.target.value }))
    }

    const handleSubmit = async () => {
        setState(p => ({...p, loading: true}))
        let response = await fetch('/api/suggestion', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                comment: state.text,
                userId: 1
            })
        });
        if (response.ok) {
            let data = response.json();
            if (data) {
                setState(p => ({ ...p, loading: false, text: ''}))
            }
        }
    }

    const resetState = () => {
        setState(p => ({ ...p, text: '' }));
    }

    return (
        <div>
            <h1>CO's Suggestion Box</h1>
            <div className="home-body">
                <span>{state.text.length}/256</span>
                <textarea className="suggestion-text-area" cols={100} rows={10} value={state.text} onChange={handleCommentUpdate} />
                <div className="button-box">
                    <button className="reset-button" onClick={() => resetState()} disabled={state.loading}>Reset</button>
                    <button className="submit-button" onClick={() => handleSubmit()} disabled={state.loading}>Submit</button>
                </div>
            </div>
        </div>
    )
}
