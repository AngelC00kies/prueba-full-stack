<template>
  <div>
    <div class="header">
      <h1>
        <i class="bi bi-people"></i>
        Clientes
      </h1>
      <button class="btn btn-primary" @click="abrirFormulario()">
        <i class="bi bi-plus-circle"></i>
        Nuevo Cliente
      </button>
    </div>

    <div v-if="error" class="alert alert-error">
      <i class="bi bi-exclamation-triangle"></i>
      {{ error }}
    </div>

    <!-- 🆕 Buscador -->
    <div class="search-box" v-if="!store.cargando && store.clientes.length > 0">
      <i class="bi bi-search"></i>
      <input
        v-model="busqueda"
        type="text"
        placeholder="Buscar por nombre, email, teléfono o ID..."
      />
      <button v-if="busqueda" @click="busqueda = ''" class="clear-btn">
        <i class="bi bi-x-lg"></i>
      </button>
    </div>

    <Loader v-if="store.cargando" />

    <div v-else-if="store.clientes.length === 0" class="empty">
      <i class="bi bi-inbox"></i>
      <p>No hay clientes registrados</p>
    </div>

    <div v-else-if="clientesFiltrados.length === 0" class="empty">
      <i class="bi bi-search"></i>
      <p>No se encontraron clientes para "{{ busqueda }}"</p>
    </div>

    <table v-else class="table">
      <thead>
        <tr>
          <th>ID</th>
          <th>Nombre</th>
          <th>Email</th>
          <th>Teléfono</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="c in clientesFiltrados" :key="c.id">
          <td>{{ c.id }}</td>
          <td>{{ c.nombre }}</td>
          <td>{{ c.email }}</td>
          <td>{{ c.telefono }}</td>
          <td>
            <button class="btn btn-secondary btn-sm" @click="abrirFormulario(c)">
              <i class="bi bi-pencil"></i>
              Editar
            </button>
            <button class="btn btn-danger btn-sm" @click="confirmarEliminar(c)">
              <i class="bi bi-trash"></i>
              Eliminar
            </button>
          </td>
        </tr>
      </tbody>
    </table>

    <div v-if="mostrarFormulario" class="modal-overlay" @click.self="cerrarFormulario">
      <div class="modal">
        <h2>
          <i class="bi" :class="editando ? 'bi-pencil-square' : 'bi-person-plus'"></i>
          {{ editando ? 'Editar' : 'Nuevo' }} Cliente
        </h2>
        <form @submit.prevent="guardar">
          <div class="form-group">
            <label><i class="bi bi-person"></i> Nombre</label>
            <input v-model="form.nombre" type="text" required maxlength="100" />
          </div>
          <div class="form-group">
            <label><i class="bi bi-envelope"></i> Email</label>
            <input v-model="form.email" type="email" required maxlength="100" />
          </div>
          <div class="form-group">
            <label><i class="bi bi-telephone"></i> Teléfono</label>
            <input v-model="form.telefono" type="text" required maxlength="20" />
          </div>
          <div class="modal-actions">
            <button type="button" class="btn btn-secondary" @click="cerrarFormulario">
              <i class="bi bi-x-lg"></i>
              Cancelar
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
import { useClientesStore } from '../store/clientes'
import Loader from '../components/common/Loader.vue'

const store = useClientesStore()
const mostrarFormulario = ref(false)
const editando = ref(null)
const guardando = ref(false)
const error = ref('')

// 🆕 Estado del buscador
const busqueda = ref('')

// 🆕 Lista filtrada con computed
const clientesFiltrados = computed(() => {
  const termino = busqueda.value.trim().toLowerCase()
  if (!termino) return store.clientes

  return store.clientes.filter(c =>
    c.nombre.toLowerCase().includes(termino) ||
    c.email.toLowerCase().includes(termino) ||
    c.telefono.toLowerCase().includes(termino) ||
    c.id.toString().includes(termino)
  )
})

const form = reactive({ nombre: '', email: '', telefono: '' })

onMounted(() => store.cargar())

function abrirFormulario(cliente = null) {
  error.value = ''
  if (cliente) {
    editando.value = cliente
    Object.assign(form, cliente)
  } else {
    editando.value = null
    Object.assign(form, { nombre: '', email: '', telefono: '' })
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
    error.value = e.response?.data?.mensaje || 'Error al guardar el cliente'
  } finally {
    guardando.value = false
  }
}

async function confirmarEliminar(cliente) {
  if (!confirm(`¿Eliminar el cliente "${cliente.nombre}"?`)) return
  try {
    await store.eliminar(cliente.id)
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

.alert i {
  margin-right: 0.4rem;
  vertical-align: middle;
}
</style>