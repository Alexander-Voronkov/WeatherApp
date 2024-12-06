<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
        <h3 class="text-center mb-4">Login</h3>
        <form @submit.prevent="login">
          <div class="form-group mb-3">
            <label for="username" class="form-label">Username</label>
            <input
              type="text"
              id="username"
              class="form-control"
              v-model="username"
              required
            />
          </div>
          <div class="form-group mb-3">
            <label for="password" class="form-label">Password</label>
            <input
              type="password"
              id="password"
              class="form-control"
              v-model="password"
              required
            />
          </div>
          <button type="submit" class="btn btn-primary w-100">Login</button>
        </form>
        <div class="mt-3 text-center">
          <p>
            Don't have an account?
            <router-link to="/register">Register here</router-link>
          </p>
        </div>
      </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import axios from 'axios';
import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'LoginPage',
  setup() {
    const router = useRouter();
    const username = ref('');
    const password = ref('');

    const login = async () => {
      try {
        const response = await axios.post(
          'http://localhost:7082/connect/token',
          new URLSearchParams({
            grant_type: 'password',
            username: username.value,
            password: password.value,
            client_id: 'WeatherUIClientId',
            client_secret: 'WEATHERUISECRET',
          }),
          { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }
        );

        localStorage.setItem('access_token', response.data.access_token);
        router.push('/');
      } catch (error) {
        console.error('Login failed', error);
        alert('Login failed. Please check your credentials.');
      }
    };

    return {
      username,
      password,
      login,
    };
  },
});
</script>

<style scoped>
.container {
  margin-top: 50px;
}
</style>