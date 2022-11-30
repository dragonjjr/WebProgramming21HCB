import "./App.css";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import { Component } from "react";
import Login from "./views/account/Login";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route
          path="/"
          element={
            <RequireAuth>
              <Component />
            </RequireAuth>
          }
        />
      </Routes>
    </Router>
  );
}
function RequireAuth({ children }) {
  // if (!localStorage.App_AccessToken) {
  //   return <Navigate to="/login" />;
  // }

  return children;
}

export default App;
