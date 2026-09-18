<template>
  <div>
    <div class="header">
      <h1>
        <i class="bi bi-box-seam"></i>
        Productos
      </h1>
      <button class="btn btn-primary" @click="abrirFormulario()">
        <i class="bi bi-plus-circle"></i>
        Nuevo Producto
      </button>
    </div>

    <div v-if="error" class="alert alert-error">
      <i class="bi bi-exclamation-triangle"></i>
      {{ error }}
    </div>

    <!-- 🆕 Buscador -->
    <div class="search-box" v-if="!store.cargando && store.productos.length > 0">
      <i class="bi bi-search"></i>
      <input
        v-model="busqueda"
        type="text"
        placeholder="Buscar por nombre, descripción o ID..."
      />
      <button v-if="busqueda" @click="busqueda = ''" class="clear-btn">
        <i class="bi bi-x-lg"></i>
      </button>
    </div>

    <Loader v-if="store.cargando" />

    <div v-else-if="store.productos.length === 0" class="empty">
      <i class="bi bi-inbox"></i>
      <p>No hay productos registrados</p>
    </div>

    <div v-else-if="productosFiltrados.length === 0" class="empty">
      <i class="bi bi-search"></i>
      <p>No se encontraron productos para "{{ busqueda }}"</p>
    </div>

    <table v-else class="table">
      <thead>
        <tr>
          <th>ID</th>
          <th>Nombre</th>
          <th>Descripción</th>
          <th>Precio</th>
          <th>Stock</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="p in productosFiltrados" :key="p.id">
          <td>{{ p.id }}</td>
          <td>{{ p.nombre }}</td>
          <td>{{ p.descripcion }}</td>
          <td>${{ p.precio.toFixed(2) }}</td>
          <td>
            <span :class="{ 'stock-bajo': p.stock < 5 }">
              <i class="bi" :class="p.stock < 5 ? 'bi-exclamation-circle' : 'bi-check-circle'"></i>
              {{ p.stock }}
            </span>
          </td>
          <td>
            <button class="btn btn-secondary btn-sm" @click="abrirFormulario(p)">
              <i class="bi bi-pencil"></i>
              Editar
            </button>
            <button class="btn btn-danger btn-sm" @click="confirmarEliminar(p)">
              <i class="bi bi-trash"></i>
              Eliminar
            </button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Modal de formulario -->
    <div v-if="mostrarFormulario" class="modal-overlay" @click.self="cerrarFormulario">
      <div class="modal">
        <h2>
          <i class="bi" :class="editando ? 'bi-pencil-square' : 'bi-plus-square'"></i>
          {{ editando ? 'Editar' : 'Nuevo' }} Producto
        </h2>
        <form @submit.prevent="guardar">
          <div class="form-group">
            <label><i class="bi bi-tag"></i> Nombre</label>
            <input v-model="form.nombre" type="text" required maxlength="30" />
          </div>
          <div class="form-group">
            <label><i class="bi bi-text-paragraph"></i> Descripción</label>
            <textarea v-model="form.descripcion" required maxlength="250"></textarea>
          </div>
          <div class="form-group">
            <label><i class="bi bi-currency-dollar"></i> Precio</label>
            <input v-model.number="form.precio" type="number" step="0.01" min="0.01" required />
          </div>
          <div class="form-group">
            <label><i class="bi bi-boxes"></i> Stock</label>
            <input v-model.number="form.stock" type="number" min="0" required />
          </div>
          <div class="modal-actions">
            <button type="button" class="btn btn-secondary" @click="cerrarFormulario">
              <i class="bi bi-x-lg"></i> Cancelar
            </button>
            <button type="submit" class="btn btn-primary" :disabled="guardando">
              <i class="bi" :class="guardando ? 'bi-hourglass-split' : 'bi-check-lg'"></i>
              {{ guardando ? 'Guardando...' : 'Guardar' }}
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, reactive, computed, onMounted } from 'vue'
import { useProductosStore } from '../store/productos'
import Loader from '../components/common/Loader.vue'

const store = useProductosStore()
const mostrarFormulario = ref(false)
const editando = ref(null)
const guardando = ref(false)
const error = ref('')

// 🆕 Estado del buscador
const busqueda = ref('')

// 🆕 Lista filtrada con computed
const productosFiltrados = computed(() => {
  const termino = busqueda.value.trim().toLowerCase()
  if (!termino) return store.productos

  return store.productos.filter(p =>
    p.nombre.toLowerCase().includes(termino) ||
    p.descripcion.toLowerCase().includes(termino) ||
    p.id.toString().includes(termino)
  )
})

const form = reactive({
  nombre: '',
  descripcion: '',
  precio: 0,
  stock: 0
})

onMounted(() => store.cargar())

function abrirFormulario(producto = null) {
  error.value = ''
  if (producto) {
    editando.value = producto
    Object.assign(form, producto)
  } else {
    editando.value = null
    Object.assign(form, { nombre: '', descripcion: '', precio: 0, stock: 0 })
  }
  mostrarFormulario.value = true
}

function cerrarFormulario() {
  mostrarFormulario.value = false
  editando.value = null
  error.value = ''
}

async function guardar() {
  guardando.value = true
  error.value = ''
  try {
    if (editando.value) {
      await store.actualizar(editando.value.id, { ...form })
    } else {
      await store.crear({ ...form })
    }
    cerrarFormulario()
  } catch (e) {
    error.value = e.response?.data?.mensaje || 'Error al guardar el producto'
  } finally {
    guardando.value = false
  }
}

async function confirmarEliminar(producto) {
  if (!confirm(`¿Eliminar el producto "${producto.nombre}"?`)) return
  try {
    await store.eliminar(producto.id)
  } catch (e) {
    error.value = e.response?.data?.mensaje || 'Error al eliminar'
  }
}
</script>

<style scoped>
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1.5rem;
}

.header h1 {
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.header h1 i {
  color: #3b82f6;
  font-size: 1.5rem;
}

/* 🆕 Estilos del buscador */
.search-box {
  position: relative;
  margin-bottom: 1rem;
  max-width: 400px;
}

.search-box input {
  width: 100%;
  padding: 0.6rem 2.5rem 0.6rem 2.5rem;
  border: 1px solid #d1d5db;
  border-radius: 8px;
  font-size: 0.95rem;
  transition: border-color 0.2s;
}

.search-box input:focus {
  outline: none;
  border-color: #3b82f6;
  box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1);
}

.search-box > i.bi-search {
  position: absolute;
  left: 0.75rem;
  top: 50%;
  transform: translateY(-50%);
  color: #9ca3af;
  pointer-events: none;
}

.clear-btn {
  position: absolute;
  right: 0.5rem;
  top: 50%;
  transform: translateY(-50%);
  background: none;
  border: none;
  color: #9ca3af;
  cursor: pointer;
  padding: 0.25rem;
  border-radius: 4px;
  transition: all 0.2s;
}

.clear-btn:hover {
  background-color: #f3f4f6;
  color: #374151;
}

.btn-sm {
  padding: 0.25rem 0.5rem;
  font-size: 0.8rem;
  margin-right: 0.25rem;
}

.btn i {
  margin-right: 0.35rem;
  vertical-align: middle;
}

.empty {
  text-align: center;
  padding: 3rem;
  color: #6b7280;
  background-color: white;
  border-radius: 8px;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 0.5rem;
}

.empty i {
  font-size: 2.5rem;
  color: #cbd5e1;
}

.stock-bajo {
  color: #ef4444;
  font-weight: 700;
  display: flex;
  align-items: center;
  gap: 0.25rem;
}

.stock-bajo i {
  font-size: 1rem;
}

td span:not(.stock-bajo) {
  display: inline-flex;
  align-items: center;
  gap: 0.25rem;
  color: #059669;
  font-weight: 600;
}

td span:not(.stock-bajo) i {
  font-size: 1rem;
}

.modal-overlay {
  position: fixed;
  inset: 0;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 1rem;
  z-index: 100;
}

.modal {
  background-color: white;
  padding: 2rem;
  border-radius: 12px;
  width: 100%;
  max-width: 500px;
  max-height: 90vh;
  overflow-y: auto;
}

.modal h2 {
  margin-bottom: 1.5rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.modal h2 i {
  color: #3b82f6;
  font-size: 1.3rem;
}

.form-group label {
  display: flex;
  align-items: center;
  gap: 0.35rem;
  font-weight: 500;
  margin-bottom: 0.25rem;
}

.form-group label i {
  color: #6b7280;
  font-size: 0.95rem;
}

.modal-actions {
  display: flex;
  justify-content: flex-end;
  gap: 0.5rem;
  margin-top: 1rem;
}

textarea {
  resize: vertical;
  min-height: 80px;
  font-family: inherit;
}

.alert i {
  margin-right: 0.4rem;
  vertical-align: middle;
}
</style>