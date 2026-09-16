<template>
  <div>
    <h1>
      <i class="bi bi-speedometer2"></i>
      Dashboard
    </h1>
    <p class="welcome">
      Bienvenido, <strong>{{ auth.username }}</strong>
    </p>

    <div class="stats">
      <div class="stat-card" v-for="stat in stats" :key="stat.label">
        <span class="icon" :class="stat.colorClass">
          <i :class="stat.icon"></i>
        </span>
        <div>
          <p class="label">{{ stat.label }}</p>
          <p class="value">{{ stat.value }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { onMounted, ref } from 'vue'
import { useAuthStore } from '../store/auth'
import { useProductosStore } from '../store/productos'
import { useClientesStore } from '../store/clientes'
import { useVentasStore } from '../store/ventas'

const auth = useAuthStore()
const productosStore = useProductosStore()
const clientesStore = useClientesStore()
const ventasStore = useVentasStore()

const stats = ref([
  {
    icon: 'bi bi-box-seam',
    colorClass: 'icon-blue',
    label: 'Productos',
    value: '...'
  },
  {
    icon: 'bi bi-people',
    colorClass: 'icon-green',
    label: 'Clientes',
    value: '...'
  },
  {
    icon: 'bi bi-cash-coin',
    colorClass: 'icon-amber',
    label: 'Ventas',
    value: '...'
  }
])

onMounted(async () => {
  try {
    await Promise.all([
      productosStore.cargar(),
      clientesStore.cargar(),
      ventasStore.cargar()
    ])
    stats.value[0].value = productosStore.productos.length
    stats.value[1].value = clientesStore.clientes.length
    stats.value[2].value = ventasStore.ventas.length
  } catch (e) {
    console.error(e)
  }
})
</script>

<style scoped>
h1 {
  margin-bottom: 0.5rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

h1 i {
  color: #3b82f6;
  font-size: 1.5rem;
}

.welcome {
  color: #6b7280;
  margin-bottom: 2rem;
}

.stats {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 1rem;
}

.stat-card {
  background-color: white;
  padding: 1.5rem;
  border-radius: 10px;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.08);
  display: flex;
  align-items: center;
  gap: 1rem;
  transition: transform 0.2s, box-shadow 0.2s;
}

.stat-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
}

.icon {
  font-size: 1.75rem;
  width: 52px;
  height: 52px;
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: 10px;
}

.icon-blue {
  background-color: #dbeafe;
  color: #2563eb;
}

.icon-green {
  background-color: #d1fae5;
  color: #059669;
}

.icon-amber {
  background-color: #fef3c7;
  color: #d97706;
}

.label {
  color: #6b7280;
  font-size: 0.875rem;
}

.value {
  font-size: 1.75rem;
  font-weight: 700;
}
</style>