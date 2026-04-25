<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/authStore'

const router = useRouter()
const auth = useAuthStore()

const username = ref('')
const password = ref('')
const error = ref('')
const loading = ref(false)

async function submit() {
  error.value = ''
  loading.value = true
  try {
    await auth.register({ username: username.value, password: password.value })
    router.push('/notes')
  } catch (err: any) {
    error.value = err.response?.data?.message || err.response?.data?.errors?.Username?.[0] || 'Registration failed'
  } finally {
    loading.value = false
  }
}
</script>

<template>
  <div class="min-h-screen bg-gray-50 flex items-center justify-center px-4">
    <div class="bg-white rounded-[5px] shadow-md w-full max-w-md p-8">
      <h1 class="text-2xl font-bold text-gray-800 mb-6 text-center">Create Account</h1>

      <form @submit.prevent="submit" class="space-y-4">
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">Username</label>
          <input
            v-model="username"
            type="text"
            required
            minlength="3"
            placeholder="your_username"
            class="w-full border border-gray-300 rounded-[5px] px-4 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">Password</label>
          <input
            v-model="password"
            type="password"
            required
            minlength="6"
            placeholder="Min. 6 characters"
            class="w-full border border-gray-300 rounded-[5px] px-4 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>

        <p v-if="error" class="text-red-500 text-sm">{{ error }}</p>

        <button
          type="submit"
          :disabled="loading"
          class="w-full bg-indigo-600 hover:bg-indigo-700 text-white font-semibold py-2 rounded-[5px] transition disabled:opacity-60"
        >
          {{ loading ? 'Creating...' : 'Create Account' }}
        </button>
      </form>

      <p class="mt-4 text-center text-sm text-gray-500">
        Already have an account?
        <router-link to="/login" class="text-indigo-600 hover:underline">Sign In</router-link>
      </p>
    </div>
  </div>
</template>
