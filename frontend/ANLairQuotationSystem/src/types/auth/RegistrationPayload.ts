export type RegistrationPayload = {
  Username: string;
  Password: string;
  ConfirmPassword: string;
  Firstname: string;
  Middlename?: string;
  Surname: string;
  ContactNumber: string;
  Email: string;
  ExtensionName?: string;
};
