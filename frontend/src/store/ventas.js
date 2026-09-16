import { defineStore } from 'pinia'
import { ref } from 'vue'
import { ventaService } from '../services/ventaService'

export const useVentasStore = defineStore('ventas', () => {
  const ventas = ref([])
  const cargando = ref(false)
  const error = ref('')

  async function cargar() {
    cargando.value = true
    error.value = ''
    try {
      ventas.value = await ventaService.obtenerTodas()
    } catch (e) {
      error.value = e.response?.data?.mensaje || 'Error al cargar ventas'
    } finally {
      cargando.value = false
    }
  }

  async function crear(venta) {
    const nueva = await ventaService.crear(venta)
    ventas.value.unshift(nueva)
    return nueva
  }

  return { ventas, cargando, error, cargar, crear }
})