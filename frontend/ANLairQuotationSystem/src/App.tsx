import { Navigate, Route, Routes } from "react-router";
import LoginPage from "./components/pages/auth/LoginPage";
import LandingPage from "./components/pages/LandingPage";
import RegistrationPage from "./components/pages/auth/RegistrationPage";
import ProtectedRoute from "./components/utilities/ProtectedRoute";
import HomePage from "./components/pages/HomePage";

export default function App() {
  return (
    <Routes>
      <Route path="/" element={<LandingPage />} />
      <Route path="auth">
        <Route index element={<Navigate to={"login"} />} />
        <Route path="login" element={<LoginPage />} />
        <Route path="register" element={<RegistrationPage />} />
      </Route>
      <Route element={<ProtectedRoute />}>
        <Route path="home" element={<HomePage />} />
      </Route>
    </Routes>
  );
}
