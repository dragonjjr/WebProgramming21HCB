import React, { createContext, useState } from "react";

export const StoreContext = createContext(null);

export default ({ children }) => {
  const [transactionId, setTransactionId] = useState(null);

  const store = {
    transaction: [transactionId, setTransactionId],
  };

  return (
    <StoreContext.Provider value={store}>{children}</StoreContext.Provider>
  );
};
