import axios from 'axios'

/**
 * Instancia centralizada de Axios para consumir la API.
 * Incluye interceptores para adjuntar el token JWT automáticamente
 * y manejar errores 401 de forma global.
 */
const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: {
    'Content-Type': 'application/json'
  }
})

// ========== Interceptor de petición ==========
// Adjunta el token JWT en cada petición si existe
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token')
    if (token) {
      config.headers.Authorization = `Bearer ${token}`
    }
    return config
  },
  (error) => Promise.reject(error)
)

// ========== Interceptor de respuesta ==========
// Maneja errores globales (401 = token expirado → logout)
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token')
      localStorage.removeItem('usuario')
      if (window.location.pathname !== '/login') {
        window.location.href = '/login'
      }
    }
    return Promise.reject(error)
  }
)

export default api