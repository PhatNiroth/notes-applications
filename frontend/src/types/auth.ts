export interface RegisterDto {
  username: string
  password: string
}

export interface LoginDto {
  username: string
  password: string
}

export interface AuthResponse {
  userId: number
  username: string
  token: string
}
