import { useEffect } from "react";
import api from "../../api/axios";
import { type Result } from "../../types/common/Result";
import { useAuth } from "../../contexts/AuthProvider";

export default function HomePage() {
  const { logout } = useAuth();

  return (
    <button onClick={logout} className="bg-alabaster-grey-500 p-5">
      Logout
    </button>
  );
}
