import React, { useState, useEffect } from 'react';

const COBox = () => {
    const [state, setState] = useState({
        suggestions: [],
        loading: true
    });

    useEffect(() => {
        const getSuggestions = async () => {
            let response = await fetch("api/suggestion/GetSuggestions");
            if (response.ok) {
                let data = await response.json();
                setState(p => ({ ...p, suggestions: data, loading: false }));
            }
        }
        getSuggestions();
    }, [])

    useEffect(() => {
        console.log(state.suggestions)
    }, [state])

    const handleUpdateStatus = async (suggestionId, statusId) => {
        let response = await fetch('/api/suggestion/UpdateStatus', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                suggestionId: suggestionId,
                statusId: statusId
            })
        });
        if (response.ok) {
            console.log("success")
            let data = await response.text();
            const index = state.suggestions.findIndex((suggestion) => suggestion.suggestionId === suggestionId);

            // If the suggestion exists, update its status property
            if (index !== -1) {
                const updatedSuggestions = [...state.suggestions];
                updatedSuggestions[index] = {
                    ...updatedSuggestions[index],
                    status: data,
                };
                console.log(updatedSuggestions);
                setState(p => ({...p, suggestions: updatedSuggestions }));
            }
        }
    }

    return (
        <div>
            CO's Page
            <div className="suggestions-div">
                {state.loading ?
                    "Loading..."
                    : state.suggestions.map(s => {
                        return (
                            <div className="suggestion-item" key={s.suggestionId }>
                                <div className={`item-header${s.status === "Unread" ? " unread" :
                                    s.status === "InProgress" ? " inprog" :
                                        s.status === "Completed" ? " completed" :
                                            s.status === "Cancelled" ? " cancelled" :
                                                ''}`}>
                                    {s.status }
                                </div>
                                <div className="item-body">
                                    {s.comment }
                                </div>
                                <div className="item-actions">
                                    {

                                        (s.status !== "Cancelled" && s.status !== "Completed" && s.status !== "Finished") && <button className="cancel-button" onClick={() => handleUpdateStatus(s.suggestionId, 4)}>Cancel</button>

                                    }
                                    {
                                        (s.status === "Unread" || s.status === "Read") && <button className="start-button" onClick={() => handleUpdateStatus(s.suggestionId, 5)}>Mark In-progress</button>
                                    }
                                    {
                                        s.status === "InProgress" && <button className="complete-button" onClick={() => handleUpdateStatus(s.suggestionId, 3)}>Complete</button>
                                    }
                                    {
                                        s.status === "Completed" && <button className="complete-button" onClick={() => handleUpdateStatus(s.suggestionId, 6)}>Finish</button>
                                    }
                                    {
                                        s.status === "Cancelled" && <button className="complete-button" onClick={() => handleUpdateStatus(s.suggestionId, 6)}>Finish</button>
                                    }
                                </div>
                            </div>

                            )
                    })
                    }
            </div>
        </div>
        )
}

export default COBox;