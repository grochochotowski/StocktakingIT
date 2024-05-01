import React from 'react'

function MessageBox(props) {
  return (
    <div className="info-box">
        <h3>{props.header}</h3>
        <p>{props.message}</p>
    </div>
  )
}

export default MessageBox