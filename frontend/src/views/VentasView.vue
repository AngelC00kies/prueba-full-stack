<template>
  <div>
    <h1>
      <i class="bi bi-cart-plus"></i>
      Nueva Venta
    </h1>

    <div v-if="error" class="alert alert-error">
      <i class="bi bi-exclamation-triangle"></i>
      {{ error }}
    </div>
    <div v-if="exito" class="alert alert-success">
      <i class="bi bi-check-circle"></i>
      {{ exito }}
    </div>

    <div class="venta-grid">
      <!-- Columna izquierda: selección de cliente y productos -->
      <div class="card">
        <h2>
          <i class="bi bi-1-circle"></i>
          Selecciona el cliente
        </h2>
        <div class="form-group">
          <select v-model.number="idCliente" required>
            <option :value="null" disabled>-- Selecciona un cliente --</option>
            <option v-for="c in clientesStore.clientes" :key="c.id" :value="c.id">
              {{ c.nombre }} ({{ c.email }})
            </option>
          </select>
        </div>

        <h2>
          <i class="bi bi-2-circle"></i>
          Agrega productos
        </h2>
        <div class="form-group producto-selector">
          <select v-model.number="productoSeleccionado">
            <option :value="null" disabled>-- Selecciona un producto --</option>
            <option
              v-for="p in productosStore.productos"
              :key="p.id"
              :value="p.id"
              :disabled="p.stock === 0"
            >
              {{ p.nombre }} - ${{ p.precio.toFixed(2) }} (Stock: {{ p.stock }})
            </option>
          </select>
          <input
            v-model.number="cantidadSeleccionada"
            type="number"
            min="1"
            placeholder="Cantidad"
          />
          <button class="btn btn-primary" @click="agregarDetalle" :disabled="!puedeAgregar">
            <i class="bi bi-plus-circle"></i>
            Agregar
          </button>
        </div>

        <h2>
          <i class="bi bi-3-circle"></i>
          Productos en la venta
        </h2>
        <div v-if="detalles.length === 0" class="empty-detalles">
          <i class="bi bi-cart-x"></i>
          Aún no has agregado productos
        </div>
        <table v-else class="table">
          <thead>
            <tr>
              <th>Producto</th>
              <th>Cantidad</th>
              <th>Precio</th>
              <th>Subtotal</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(d, i) in detalles" :key="i">
              <td>{{ d.nombreProducto }}</td>
              <td>{{ d.cantidad }}</td>
              <td>${{ d.precioUnitario.toFixed(2) }}</td>
              <td>${{ (d.cantidad * d.precioUnitario).toFixed(2) }}</td>
              <td>
                <button class="btn btn-danger btn-sm" @click="quitarDetalle(i)">
                  <i class="bi bi-x-lg"></i>
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <!-- Columna derecha: resumen -->
      <div class="card resumen">
        <h2>
          <i class="bi bi-receipt"></i>
          Resumen
        </h2>
        <div class="resumen-linea">
          <span>
            <i class="bi bi-person-circle"></i>
            Cliente:
          </span>
          <strong>{{ clienteSeleccionado?.nombre || 'No seleccionado' }}</strong>
        </div>
        <div class="resumen-linea">
          <span>
            <i class="bi bi-box-seam"></i>
            Productos:
          </span>
          <strong>{{ detalles.length }}</strong>
        </div>
        <div class="resumen-total">
          <span>TOTAL:</span>
          <strong>${{ total.toFixed(2) }}</strong>
        </div>

        <button
          class="btn btn-primary btn-block"
          :disabled="!puedeGuardar || guardando"
          @click="guardarVenta"
        >
          <i class="bi" :class="guardando ? 'bi-hourglass-split' : 'bi-save'"></i>
          {{ guardando ? 'Registrando...' : 'Registrar Venta' }}
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useClientesStore } from '../store/clientes'
import { useProductosStore } from '../store/productos'
import { useVentasStore } from '../store/ventas'

const router = useRouter()
const clientesStore = useClientesStore()
const productosStore = useProductosStore()
const ventasStore = useVentasStore()

const idCliente = ref(null)
const productoSeleccionado = ref(null)
const cantidadSeleccionada = ref(1)
const detalles = ref([])
const guardando = ref(false)
const error = ref('')
const exito = ref('')

onMounted(async () => {
  await Promise.all([
    clientesStore.cargar(),
    productosStore.cargar()
  ])
})

const clienteSeleccionado = computed(() =>
  clientesStore.clientes.find(c => c.id === idCliente.value)
)

const puedeAgregar = computed(() =>
  productoSeleccionado.value != null && cantidadSeleccionada.value > 0
)

const total = computed(() =>
  detalles.value.reduce((sum, d) => sum + d.cantidad * d.precioUnitario, 0)
)

const puedeGuardar = computed(() =>
  idCliente.value != null && detalles.value.length > 0
)

function agregarDetalle() {
  const producto = productosStore.productos.find(p => p.id === productoSeleccionado.value)
  if (!producto) return

  if (producto.stock < cantidadSeleccionada.value) {
    error.value = `Stock insuficiente. Disponible: ${producto.stock}`
    return
  }

  detalles.value.push({
    idProducto: producto.id,
    nombreProducto: producto.nombre,
    cantidad: cantidadSeleccionada.value,
    precioUnitario: producto.precio
  })

  productoSeleccionado.value = null
  cantidadSeleccionada.value = 1
  error.value = ''
}

function quitarDetalle(index) {
  detalles.value.splice(index, 1)
}

async function guardarVenta() {
  guardando.value = true
  error.value = ''
  exito.value = ''
  try {
    await ventasStore.crear({
      idCliente: idCliente.value,
      detalles: detalles.value.map(d => ({
        idProducto: d.idProducto,
        cantidad: d.cantidad
      }))
    })
    exito.value = '¡Venta registrada exitosamente!'
    idCliente.value = null
    detalles.value = []
    await productosStore.cargar()

    setTimeout(() => router.push('/historial-ventas'), 1500)
  } catch (e) {
    error.value = e.response?.data?.mensaje || 'Error al registrar la venta'
  } finally {
    guardando.value = false
  }
}
</script>

<style scoped>
h1 {
  margin-bottom: 1.5rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

h1 i {
  color: #3b82f6;
  font-size: 1.5rem;
}

.venta-grid {
  display: grid;
  grid-template-columns: 2fr 1fr;
  gap: 1.5rem;
}

.card {
  background-color: white;
  padding: 1.5rem;
  border-radius: 10px;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.08);
}

.card h2 {
  font-size: 1.1rem;
  margin: 1rem 0 0.75rem;
  display: flex;
  align-items: center;
  gap: 0.4rem;
  color: #1f2937;
}

.card h2:first-child { margin-top: 0; }

.card h2 i {
  color: #3b82f6;
  font-size: 1.1rem;
}

.producto-selector {
  display: grid;
  grid-template-columns: 2fr 100px auto;
  gap: 0.5rem;
}

.empty-detalles {
  padding: 1.5rem;
  text-align: center;
  color: #9ca3af;
  background-color: #f9fafb;
  border-radius: 6px;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 0.5rem;
}

.empty-detalles i {
  font-size: 2rem;
  color: #cbd5e1;
}

.resumen {
  position: sticky;
  top: 1rem;
  height: fit-content;
}

.resumen-linea {
  display: flex;
  justify-content: space-between;
  padding: 0.5rem 0;
  border-bottom: 1px solid #f3f4f6;
}

.resumen-linea span {
  display: flex;
  align-items: center;
  gap: 0.35rem;
}

.resumen-linea span i {
  color: #6b7280;
  font-size: 0.95rem;
}

.resumen-total {
  display: flex;
  justify-content: space-between;
  padding: 1rem 0;
  font-size: 1.25rem;
  border-top: 2px solid #3b82f6;
  margin-top: 0.5rem;
}

.btn-block {
  width: 100%;
  padding: 0.75rem;
  font-weight: 600;
  margin-top: 1rem;
}

.btn i {
  margin-right: 0.35rem;
  vertical-align: middle;
}

.btn-sm {
  padding: 0.2rem 0.5rem;
}

.btn-sm i {
  margin-right: 0;
}

.alert i {
  margin-right: 0.4rem;
  vertical-align: middle;
}

@media (max-width: 900px) {
  .venta-grid {
    grid-template-columns: 1fr;
  }

  .producto-selector {
    grid-template-columns: 1fr;
  }
}
</style>