import { createRouter, createWebHistory } from 'vue-router'
import { useAuthStore } from '../store/auth'

const routes = [
  {
    path: '/login',
    name: 'login',
    component: () => import('../views/LoginView.vue'),
    meta: { public: true }
  },
  {
    path: '/',
    component: () => import('../layouts/DefaultLayout.vue'),
    children: [
      { path: '', redirect: '/dashboard' },
      { path: 'dashboard', name: 'dashboard', component: () => import('../views/DashboardView.vue') },
      { path: 'productos', name: 'productos', component: () => import('../views/ProductosView.vue') },
      { path: 'clientes', name: 'clientes', component: () => import('../views/ClientesView.vue') },
      { path: 'ventas', name: 'ventas', component: () => import('../views/VentasView.vue') },
      { path: 'historial-ventas', name: 'historial-ventas', component: () => import('../views/HistorialVentasView.vue') }
    ]
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

// Guard: redirige a /login si no hay sesión
router.beforeEach((to) => {
  const auth = useAuthStore()
  if (!to.meta.public && !auth.isAuthenticated) {
    return { name: 'login' }
  }
  if (to.name === 'login' && auth.isAuthenticated) {
    return { name: 'dashboard' }
  }
})

export default router