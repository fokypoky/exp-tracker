export type JwtPayload = {
  login: string;
  guid: string;
  exp: number;
  iss: string;
}