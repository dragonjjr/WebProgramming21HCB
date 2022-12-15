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
import Employee from "./views/employee/Employee";

import Admin from "./views/admin/Admin";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/forgotpassword" element={<ForgotPassword />} />
        <Route path="/register" element={<Register />} />
        <Route
          path="/employee"
          element={
            <RequireAuth>
              <Employee />
            </RequireAuth>
          }
        />
        <Route
          path="/admin"
          element={
            <RequireAuth>
              <Admin />
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
