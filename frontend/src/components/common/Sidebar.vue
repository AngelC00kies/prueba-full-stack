<template>
  <aside class="sidebar">
    <h2 class="logo">
      <i class="bi bi-graph-up-arrow"></i>
      Gestión
    </h2>
    <nav>
      <router-link to="/dashboard" class="nav-link">
        <i class="bi bi-house-door"></i>
        Dashboard
      </router-link>
      <router-link to="/productos" class="nav-link">
        <i class="bi bi-box-seam"></i>
        Productos
      </router-link>
      <router-link to="/clientes" class="nav-link">
        <i class="bi bi-people"></i>
        Clientes
      </router-link>
      <router-link to="/ventas" class="nav-link">
        <i class="bi bi-cart-plus"></i>
        Nueva Venta
      </router-link>
      <router-link to="/historial-ventas" class="nav-link">
        <i class="bi bi-clock-history"></i>
        Historial
      </router-link>
    </nav>
  </aside>
</template>

<script setup>
// El sidebar solo renderiza los enlaces de navegación
</script>

<style scoped>
.sidebar {
  width: 220px;
  background-color: #1e293b;
  color: white;
  padding: 1.5rem 1rem;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.logo {
  font-size: 1.25rem;
  margin-bottom: 1rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.logo i {
  color: #60a5fa;
  font-size: 1.4rem;
}

nav {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.nav-link {
  color: #cbd5e1;
  text-decoration: none;
  padding: 0.5rem 0.75rem;
  border-radius: 6px;
  transition: all 0.2s;
  display: flex;
  align-items: center;
  gap: 0.6rem;
}

.nav-link i {
  font-size: 1.05rem;
  width: 20px;
  text-align: center;
}

.nav-link:hover {
  background-color: #334155;
  color: white;
}

.nav-link.router-link-active {
  background-color: #3b82f6;
  color: white;
}

.nav-link.router-link-active i {
  color: white;
}

@media (max-width: 768px) {
  .sidebar {
    width: 100%;
    flex-direction: row;
    overflow-x: auto;
    padding: 0.75rem;
  }

  .logo {
    display: none;
  }

  nav {
    flex-direction: row;
    gap: 0.25rem;
  }

  .nav-link {
    white-space: nowrap;
    font-size: 0.875rem;
    padding: 0.4rem 0.6rem;
  }

  .nav-link i {
    font-size: 1rem;
  }
}
</style>