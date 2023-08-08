import React from 'react';


const MakeSelect = ({tabIndex, onChange, state}) => {
    return (
        <select tabIndex={tabIndex} value={state.product?.make_id   } id="make"
                className="form-select"
                required
                onClick={(e) => onChange(e, )}
        >
            {
                state.makes.list && state.makes.list.length > 0 && state.makes.list.map((m, i) =>
                    <option key={i} value={m.id}>{m.name}</option>)
            }
        </select>
    )
};

export default MakeSelect;
