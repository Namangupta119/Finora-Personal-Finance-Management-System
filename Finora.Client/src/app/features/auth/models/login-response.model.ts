export interface LoginResponse {
  accessToken: string;
  refreshToken: string;
  expiresOn: string;
  userId: string;
  email: string;
}