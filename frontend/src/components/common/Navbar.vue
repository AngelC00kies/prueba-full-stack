<template>
  <header class="navbar">
    <div class="user-info">
      <span>
        <i class="bi bi-person-circle"></i>
        {{ auth.username }}
      </span>
      <span class="rol">{{ auth.usuario?.rol }}</span>
    </div>
    <button @click="auth.logout()" class="btn btn-secondary">
      <i class="bi bi-box-arrow-right"></i>
      Cerrar sesión
    </button>
  </header>
</template>

<script setup>
import { useAuthStore } from '../../store/auth'
const auth = useAuthStore()
</script>

<style scoped>
.navbar {
  background-color: white;
  padding: 0.75rem 1.5rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-bottom: 1px solid #e5e7eb;
}

.user-info {
  display: flex;
  align-items: center;
  gap: 0.75rem;
}

.user-info i {
  margin-right: 0.35rem;
  vertical-align: middle;
  color: #3b82f6;
  font-size: 1.1rem;
}

.rol {
  background-color: #dbeafe;
  color: #1e40af;
  padding: 0.15rem 0.5rem;
  border-radius: 999px;
  font-size: 0.75rem;
  text-transform: uppercase;
}

button i {
  margin-right: 0.35rem;
  vertical-align: middle;
}
</style>