// Requests

export type AuthRequest = {
  login: string;
  password: string;
}

export type RefreshTokenRequest = {
  refreshToken: string;
}

// Responses

export type JwtTokenPair = {
  accessToken: string;
  refreshToken: string;
}