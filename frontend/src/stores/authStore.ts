import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { authApi } from '@/api/authApi'
import type { LoginDto, RegisterDto } from '@/types/auth'

export const useAuthStore = defineStore('auth', () => {
  const token = ref<string | null>(localStorage.getItem('token'))
  const username = ref<string | null>(localStorage.getItem('username'))

  const isAuthenticated = computed(() => !!token.value)

  async function login(dto: LoginDto) {
    const { data } = await authApi.login(dto)
    setSession(data.token, data.username)
  }

  async function register(dto: RegisterDto) {
    const { data } = await authApi.register(dto)
    setSession(data.token, data.username)
  }

  function setSession(t: string, u: string) {
    token.value = t
    username.value = u
    localStorage.setItem('token', t)
    localStorage.setItem('username', u)
  }

  function logout() {
    token.value = null
    username.value = null
    localStorage.removeItem('token')
    localStorage.removeItem('username')
  }

  return { token, username, isAuthenticated, login, register, logout }
})
