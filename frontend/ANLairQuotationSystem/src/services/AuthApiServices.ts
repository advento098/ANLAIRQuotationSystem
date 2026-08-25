import api from "../api/axios";
import type { LoginPayload } from "../types/auth/LoginPayload";
import type { RegistrationPayload } from "../types/auth/RegistrationPayload";
import type { Result } from "../types/common/Result";

export async function Login(userEmail: string, password: string) {
  const res = await api.post<Result<string>>("auth/login", {
    UserIdentifier: userEmail,
    Password: password,
  } as LoginPayload);

  return res.data;
}

export async function Logout() {
  const res = await api.post<Result<boolean>>("auth/logout");

  return res.data;
}

export async function Register(payload: Partial<RegistrationPayload>) {
  const res = await api.post<Result<boolean>>("auth/register", payload);

  return res;
}
