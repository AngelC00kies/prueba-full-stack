import api from './api'

/**
 * Servicio para registrar y consultar ventas.
 */
export const ventaService = {
  async obtenerTodas() {
    const { data } = await api.get('/ventas')
    return data
  },

  async obtenerPorId(id) {
    const { data } = await api.get(`/ventas/${id}`)
    return data
  },

  async crear(venta) {
    const { data } = await api.post('/ventas', venta)
    return data
  }
}