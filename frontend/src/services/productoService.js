import api from './api'

/**
 * Servicio CRUD para la gestión de productos.
 */
export const productoService = {
  async obtenerTodos() {
    const { data } = await api.get('/productos')
    return data
  },

  async obtenerPorId(id) {
    const { data } = await api.get(`/productos/${id}`)
    return data
  },

  async crear(producto) {
    const { data } = await api.post('/productos', producto)
    return data
  },

  async actualizar(id, producto) {
    const { data } = await api.put(`/productos/${id}`, producto)
    return data
  },

  async eliminar(id) {
    await api.delete(`/productos/${id}`)
  }
}