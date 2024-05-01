import React from 'react'

import '../styles/messageBox.css'
import '../styles/index.css'

function MessageBox(props) {
  return (
    <div className="info-box">
        <h3>{props.header}</h3>
        <p>{props.message}</p>
    </div>
  )
}

export default MessageBox