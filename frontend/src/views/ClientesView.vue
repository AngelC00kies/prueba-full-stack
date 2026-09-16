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

    <div v-if="error" class="alert alert-error">{{ error }}</div>

    <Loader v-if="store.cargando" />

    <div v-else-if="store.clientes.length === 0" class="empty">
      <i class="bi bi-inbox"></i>
      <p>No hay clientes registrados</p>
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
        <tr v-for="c in store.clientes" :key="c.id">
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
            <label>Nombre</label>
            <input v-model="form.nombre" type="text" required maxlength="100" />
          </div>
          <div class="form-group">
            <label>Email</label>
            <input v-model="form.email" type="email" required maxlength="100" />
          </div>
          <div class="form-group">
            <label>Teléfono</label>
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
import { ref, reactive, onMounted } from 'vue'
import { useClientesStore } from '../store/clientes'
import Loader from '../components/common/Loader.vue'

const store = useClientesStore()
const mostrarFormulario = ref(false)
const editando = ref(null)
const guardando = ref(false)
const error = ref('')

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

.modal-actions {
  display: flex;
  justify-content: flex-end;
  gap: 0.5rem;
  margin-top: 1rem;
}
</style>