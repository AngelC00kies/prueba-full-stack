<template>
  <div class="login-page">
    <div class="login-card">
      <h1>
        <i class="bi bi-shield-lock-fill"></i>
        Iniciar Sesión
      </h1>
      <p class="subtitle">Sistema de Gestión de Ventas</p>

      <div v-if="error" class="alert alert-error">
        <i class="bi bi-exclamation-triangle-fill"></i>
        {{ error }}
      </div>

      <form @submit.prevent="handleLogin">
        <div class="form-group">
          <label>
            <i class="bi bi-person"></i>
            Usuario
          </label>
          <input
            v-model="form.username"
            type="text"
            required
            autocomplete="username"
            placeholder="Ingresa tu usuario"
          />
        </div>

        <div class="form-group">
          <label>
            <i class="bi bi-key"></i>
            Contraseña
          </label>
          <input
            v-model="form.password"
            type="password"
            required
            autocomplete="current-password"
            placeholder="Ingresa tu contraseña"
          />
        </div>

        <button type="submit" class="btn btn-primary btn-block" :disabled="cargando">
          <i class="bi" :class="cargando ? 'bi-hourglass-split' : 'bi-box-arrow-in-right'"></i>
          {{ cargando ? 'Ingresando...' : 'Iniciar Sesión' }}
        </button>
      </form>

      <div class="register-link">
        ¿No tienes cuenta?
        <a href="#" @click.prevent="mostrarRegistro = !mostrarRegistro">
          {{ mostrarRegistro ? 'Iniciar sesión' : 'Regístrate' }}
        </a>
      </div>

      <form v-if="mostrarRegistro" @submit.prevent="handleRegister" class="register-form">
        <h3>
          <i class="bi bi-person-plus-fill"></i>
          Crear cuenta
        </h3>

        <div class="form-group">
          <label>
            <i class="bi bi-person"></i>
            Usuario
          </label>
          <input
            v-model="registerForm.username"
            type="text"
            required
            minlength="3"
            placeholder="Mínimo 3 caracteres"
          />
        </div>

        <div class="form-group">
          <label>
            <i class="bi bi-key"></i>
            Contraseña
          </label>
          <input
            v-model="registerForm.password"
            type="password"
            required
            minlength="6"
            placeholder="Mínimo 6 caracteres"
          />
        </div>

        <button type="submit" class="btn btn-primary btn-block" :disabled="cargando">
          <i class="bi bi-person-check-fill"></i>
          Registrarse
        </button>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../store/auth'

const router = useRouter()
const auth = useAuthStore()

const form = reactive({ username: '', password: '' })
const registerForm = reactive({ username: '', password: '', rol: 'user' })
const cargando = ref(false)
const error = ref('')
const mostrarRegistro = ref(false)

async function handleLogin() {
  cargando.value = true
  error.value = ''
  try {
    await auth.login(form)
    router.push('/dashboard')
  } catch (e) {
    error.value = e.response?.data?.mensaje || 'Error al iniciar sesión'
  } finally {
    cargando.value = false
  }
}

async function handleRegister() {
  cargando.value = true
  error.value = ''
  try {
    await auth.register(registerForm)
    router.push('/dashboard')
  } catch (e) {
    error.value = e.response?.data?.mensaje || 'Error al registrar'
  } finally {
    cargando.value = false
  }
}
</script>

<style scoped>
.login-page {
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background: linear-gradient(135deg, #3b82f6 0%, #1e40af 100%);
  padding: 1rem;
}

.login-card {
  background-color: white;
  padding: 2.5rem;
  border-radius: 12px;
  box-shadow: 0 20px 25px -5px rgba(0, 0, 0, 0.2);
  width: 100%;
  max-width: 400px;
}

h1 {
  margin-bottom: 0.25rem;
  font-size: 1.5rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
  color: #1e293b;
}

h1 i {
  color: #3b82f6;
  font-size: 1.5rem;
}

.subtitle {
  color: #6b7280;
  margin-bottom: 1.5rem;
  font-size: 0.9rem;
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

.btn-block {
  width: 100%;
  padding: 0.75rem;
  font-weight: 600;
}

.btn-block i {
  margin-right: 0.4rem;
  vertical-align: middle;
}

.register-link {
  margin-top: 1rem;
  text-align: center;
  font-size: 0.9rem;
  color: #6b7280;
}

.register-link a {
  color: #3b82f6;
  text-decoration: none;
  font-weight: 600;
  margin-left: 0.25rem;
}

.register-link a:hover {
  text-decoration: underline;
}

.register-form {
  margin-top: 1.5rem;
  padding-top: 1.5rem;
  border-top: 1px solid #e5e7eb;
}

.register-form h3 {
  margin-bottom: 1rem;
  font-size: 1.1rem;
  display: flex;
  align-items: center;
  gap: 0.4rem;
  color: #1e293b;
}

.register-form h3 i {
  color: #3b82f6;
}

.alert i {
  margin-right: 0.4rem;
  vertical-align: middle;
}
</style>