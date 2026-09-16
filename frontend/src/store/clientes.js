import { defineStore } from 'pinia'
import { ref } from 'vue'
import { clienteService } from '../services/clienteService'

export const useClientesStore = defineStore('clientes', () => {
  const clientes = ref([])
  const cargando = ref(false)
  const error = ref('')

  async function cargar() {
    cargando.value = true
    error.value = ''
    try {
      clientes.value = await clienteService.obtenerTodos()
    } catch (e) {
      error.value = e.response?.data?.mensaje || 'Error al cargar clientes'
    } finally {
      cargando.value = false
    }
  }

  async function crear(cliente) {
    const nuevo = await clienteService.crear(cliente)
    clientes.value.push(nuevo)
    return nuevo
  }

  async function actualizar(id, cliente) {
    const actualizado = await clienteService.actualizar(id, cliente)
    const index = clientes.value.findIndex(c => c.id === id)
    if (index !== -1) clientes.value[index] = actualizado
    return actualizado
  }

  async function eliminar(id) {
    await clienteService.eliminar(id)
    clientes.value = clientes.value.filter(c => c.id !== id)
  }

  return { clientes, cargando, error, cargar, crear, actualizar, eliminar }
})