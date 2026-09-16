import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { authService } from '../services/authService'
import router from '../router'

/**
 * Store de autenticación. Maneja el estado del usuario logueado
 * y persiste el token en localStorage.
 */
export const useAuthStore = defineStore('auth', () => {
  const token = ref(localStorage.getItem('token') || '')
  const usuario = ref(JSON.parse(localStorage.getItem('usuario') || 'null'))

  const isAuthenticated = computed(() => !!token.value)
  const username = computed(() => usuario.value?.username || '')

  async function login(credentials) {
    const data = await authService.login(credentials)
    token.value = data.token
    usuario.value = {
      username: data.username,
      rol: data.rol,
      expira: data.expira
    }
    localStorage.setItem('token', data.token)
    localStorage.setItem('usuario', JSON.stringify(usuario.value))
    return data
  }

  async function register(userData) {
    const data = await authService.register(userData)
    token.value = data.token
    usuario.value = {
      username: data.username,
      rol: data.rol,
      expira: data.expira
    }
    localStorage.setItem('token', data.token)
    localStorage.setItem('usuario', JSON.stringify(usuario.value))
    return data
  }

  function logout() {
    token.value = ''
    usuario.value = null
    localStorage.removeItem('token')
    localStorage.removeItem('usuario')
    router.push('/login')
  }

  return {
    token,
    usuario,
    isAuthenticated,
    username,
    login,
    register,
    logout
  }
})