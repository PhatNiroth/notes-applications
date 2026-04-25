import { createRouter, createWebHistory } from 'vue-router'
import login from '../views/Login.vue'
import register from '../views/Register.vue'
import notes from '../views/Notes.vue'
import { useAuthStore } from '@/stores/authStore'

const router = createRouter({
    history: createWebHistory(),
    routes: [
        { path: '/', redirect: '/notes' },
        { path: '/login', component: login, meta: { guestOnly: true } },
        { path: '/register', component: register, meta: { guestOnly: true } },
        { path: '/notes', component: notes, meta: { requiresAuth: true } },
    ],
});

router.beforeEach((to) => {
    const auth = useAuthStore()

    if (to.meta.requiresAuth && !auth.isAuthenticated) {
        return '/login'
    }

    if (to.meta.guestOnly && auth.isAuthenticated) {
        return '/notes'
    }
})

export default router;
