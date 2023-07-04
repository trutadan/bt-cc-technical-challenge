import "./App.css";
import { useLocation } from "react-router-dom";
import { Routes, Route, Outlet } from "react-router-dom";
import NavigationBar from "./components/application/NavigationBar";
import LoginPage from "./components/authentication/LoginPage";
import HomePage from "./components/application/HomePage";
import RegisterPage from "./components/authentication/RegisterPage";
import VerifyOTPPage from "./components/authentication/VerifyOTPPage";
import UnauthorizedPage from "./components/application/UnauthorizedPage";
import MissingPage from "./components/application/MissingPage";
import Layout from "./components/application/Layout";

function LayoutsBasedOnNavigationBar() {
  const includedPaths = ["/", "/login", "/register", "/verify-otp"];
  const location = useLocation();

  if (includedPaths.includes(location.pathname))
    return (
      <>
        <NavigationBar />
        <Outlet />
      </>
    );

  return <Layout />;
}

function App() {
  return (
    <Routes>
      <Route path="/" element={<LayoutsBasedOnNavigationBar />}>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/verify-otp" element={<VerifyOTPPage />} />
        <Route path="/unauthorized" element={<UnauthorizedPage />} />

        {/* catch all */}
        <Route path="*" element={<MissingPage />} />
      </Route>
    </Routes>
  );
}

export default App;
