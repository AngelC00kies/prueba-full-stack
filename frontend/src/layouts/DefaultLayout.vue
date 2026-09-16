<template>
  <div class="layout">
    <Sidebar />
    <div class="main">
      <Navbar />
      <main class="content">
        <router-view />
      </main>
    </div>
  </div>
</template>

<script setup>
import Sidebar from '../components/common/Sidebar.vue'
import Navbar from '../components/common/Navbar.vue'
</script>

<style scoped>
.layout {
  display: flex;
  min-height: 100vh;
}

.main {
  flex: 1;
  display: flex;
  flex-direction: column;
}

.content {
  flex: 1;
  padding: 1.5rem;
}

@media (max-width: 768px) {
  .layout {
    flex-direction: column;
  }
}
</style>