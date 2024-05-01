import React from 'react'

function MessageBox(props) {
  return (
    <div className="info-box">
        <h1>{props.header}</h1>
        <p>{props.message}</p>
    </div>
  )
}

export default MessageBox