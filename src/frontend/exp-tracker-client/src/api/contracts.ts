// Requests

export type AuthRequest = {
  login: string;
  password: string;
}

// Responses

export type JwtTokenPair = {
  accessToken: string;
  refreshToken: string;
}