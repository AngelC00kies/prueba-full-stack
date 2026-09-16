import api from './api'

/**
 * Servicio CRUD para la gestión de clientes.
 */
export const clienteService = {
  async obtenerTodos() {
    const { data } = await api.get('/clientes')
    return data
  },

  async obtenerPorId(id) {
    const { data } = await api.get(`/clientes/${id}`)
    return data
  },

  async crear(cliente) {
    const { data } = await api.post('/clientes', cliente)
    return data
  },

  async actualizar(id, cliente) {
    const { data } = await api.put(`/clientes/${id}`, cliente)
    return data
  },

  async eliminar(id) {
    await api.delete(`/clientes/${id}`)
  }
}