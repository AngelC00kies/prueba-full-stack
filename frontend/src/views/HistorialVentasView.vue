<template>
  <div>
    <h1>
      <i class="bi bi-clock-history"></i>
      Historial de Ventas
    </h1>

    <div v-if="store.error" class="alert alert-error">
      <i class="bi bi-exclamation-triangle"></i>
      {{ store.error }}
    </div>

    <Loader v-if="store.cargando" />

    <div v-else-if="store.ventas.length === 0" class="empty">
      <i class="bi bi-inbox"></i>
      <p>No hay ventas registradas</p>
    </div>

    <div v-else class="ventas-list">
      <div v-for="v in store.ventas" :key="v.id" class="venta-card">
        <div class="venta-header">
          <div>
            <h3>
              <i class="bi bi-receipt"></i>
              Venta #{{ v.id }}
            </h3>
            <p class="fecha">
              <i class="bi bi-calendar3"></i>
              {{ formatearFecha(v.fecha) }}
            </p>
          </div>
          <div class="total">${{ v.total.toFixed(2) }}</div>
        </div>

        <div class="venta-info">
          <span>
            <i class="bi bi-person-circle"></i>
            <strong>{{ v.nombreCliente }}</strong>
          </span>
          <span>
            <i class="bi bi-box-seam"></i>
            {{ v.detalles.length }} producto(s)
          </span>
        </div>

        <table class="table detalles">
          <thead>
            <tr>
              <th>Producto</th>
              <th>Cantidad</th>
              <th>Precio</th>
              <th>Subtotal</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(d, i) in v.detalles" :key="i">
              <td>{{ d.nombreProducto || 'Producto #' + d.idProducto }}</td>
              <td>{{ d.cantidad }}</td>
              <td>${{ d.precioUnitario.toFixed(2) }}</td>
              <td>${{ (d.cantidad * d.precioUnitario).toFixed(2) }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script setup>
import { onMounted } from 'vue'
import { useVentasStore } from '../store/ventas'
import Loader from '../components/common/Loader.vue'

const store = useVentasStore()

onMounted(() => store.cargar())

function formatearFecha(fecha) {
  return new Date(fecha).toLocaleString('es-DO', {
    dateStyle: 'medium',
    timeStyle: 'short'
  })
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

.ventas-list {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.venta-card {
  background-color: white;
  padding: 1.25rem;
  border-radius: 10px;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.08);
  transition: box-shadow 0.2s;
}

.venta-card:hover {
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
}

.venta-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 0.75rem;
  padding-bottom: 0.75rem;
  border-bottom: 1px solid #e5e7eb;
}

.venta-header h3 {
  margin-bottom: 0.15rem;
  display: flex;
  align-items: center;
  gap: 0.4rem;
  color: #1f2937;
}

.venta-header h3 i {
  color: #3b82f6;
  font-size: 1.1rem;
}

.fecha {
  color: #6b7280;
  font-size: 0.85rem;
  display: flex;
  align-items: center;
  gap: 0.3rem;
}

.fecha i {
  font-size: 0.8rem;
}

.total {
  font-size: 1.5rem;
  font-weight: 700;
  color: #059669;
}

.venta-info {
  display: flex;
  gap: 1.5rem;
  margin-bottom: 1rem;
  font-size: 0.9rem;
}

.venta-info span {
  display: flex;
  align-items: center;
  gap: 0.35rem;
}

.venta-info i {
  color: #6b7280;
  font-size: 1rem;
}

.detalles {
  box-shadow: none;
  background-color: #f9fafb;
}

.detalles th,
.detalles td {
  padding: 0.5rem;
  font-size: 0.875rem;
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

.alert i {
  margin-right: 0.35rem;
  vertical-align: middle;
}
</style>