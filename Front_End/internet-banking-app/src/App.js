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
import Customer from "./views/customer/Customer";
import InternalTranfer from "./views/customer/InternalTranfer";
import DebtManage from "./views/customer/DebtManage";
import RecipientManage from "./views/customer/RecipientManage";
import Transaction from "./views/customer/Transaction";
import ExternalTranfer from "./views/customer/ExternalTranfer";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/forgotpassword" element={<ForgotPassword />} />
        <Route
          path="/register"
          element={
            <RequireAuth>
              <Register />
            </RequireAuth>
          }
        />
        <Route
          path="/InternalTranfer"
          element={
            <RequireAuth>
              <InternalTranfer />
            </RequireAuth>
          }
        />
        <Route
          path="/ExternalTranfer"
          element={
            <RequireAuth>
              <ExternalTranfer />
            </RequireAuth>
          }
        />
        <Route
          path="/debtmanage"
          element={
            <RequireAuth>
              <DebtManage />
            </RequireAuth>
          }
        />
        <Route
          path="/recipientmanage"
          element={
            <RequireAuth>
              <RecipientManage />
            </RequireAuth>
          }
        />
        <Route
          path="/TransactionHistory"
          element={
            <RequireAuth>
              <Transaction />
            </RequireAuth>
          }
        />
        <Route
          path="/"
          element={
            <RequireAuth>
              <Customer />
            </RequireAuth>
          }
        />
        <Route path="/logout" element={<Logout />} />
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
  if (!localStorage.App_AccessToken) {
    return <Navigate to="/login" />;
  }

  return children;
}

function Logout() {
  localStorage.clear();
  return <Navigate to="/login" />;
}

export default App;
