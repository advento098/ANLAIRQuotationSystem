import { Outlet, useNavigate } from "react-router";
import { useAuth } from "../../contexts/AuthProvider";
import { useEffect } from "react";

export default function ProtectedRoute() {
  const { isAuthenticated } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (!isAuthenticated) navigate("auth/login");
  }, []);

  return <Outlet />;
}
