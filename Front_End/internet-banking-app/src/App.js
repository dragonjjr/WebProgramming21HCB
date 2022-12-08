import "./App.css";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";

import Login from "./views/account/Login";
import ForgotPassword from "./views/account/ForgotPassword";
import Register from "./views/account/Register";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/forgotpassword" element={<ForgotPassword />} />
        <Route path="/register" element={<Register />} />
        <Route
          path="/"
          element={
            <RequireAuth>
              <div></div>
            </RequireAuth>
          }
        />
        <Route path="*" element={<Navigate to="/" />} />
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
