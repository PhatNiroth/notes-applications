import { http } from './http'
import type { LoginDto, RegisterDto, AuthResponse } from '@/types/auth'

export const authApi = {
  register: (dto: RegisterDto) => http.post<AuthResponse>('/auth/register', dto),
  login: (dto: LoginDto) => http.post<AuthResponse>('/auth/login', dto),
}
