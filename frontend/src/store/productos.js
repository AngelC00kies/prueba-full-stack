import { defineStore } from 'pinia'
import { ref } from 'vue'
import { productoService } from '../services/productoService'

export const useProductosStore = defineStore('productos', () => {
  const productos = ref([])
  const cargando = ref(false)
  const error = ref('')

  async function cargar() {
    cargando.value = true
    error.value = ''
    try {
      productos.value = await productoService.obtenerTodos()
    } catch (e) {
      error.value = e.response?.data?.mensaje || 'Error al cargar productos'
    } finally {
      cargando.value = false
    }
  }

  async function crear(producto) {
    const nuevo = await productoService.crear(producto)
    productos.value.push(nuevo)
    return nuevo
  }

  async function actualizar(id, producto) {
    const actualizado = await productoService.actualizar(id, producto)
    const index = productos.value.findIndex(p => p.id === id)
    if (index !== -1) productos.value[index] = actualizado
    return actualizado
  }

  async function eliminar(id) {
    await productoService.eliminar(id)
    productos.value = productos.value.filter(p => p.id !== id)
  }

  return { productos, cargando, error, cargar, crear, actualizar, eliminar }
})