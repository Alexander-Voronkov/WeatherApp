<template>
    <div class="container mt-5">
      <div class="row justify-content-center">
          <h3 class="text-center">Register</h3>
          <form @submit.prevent="register">
            <div class="form-group mb-3">
              <label for="username" class="form-label">Email</label>
              <input
                type="text"
                id="username"
                class="form-control"
                v-model="email"
                required
              />
            </div>
            <div class="form-group mb-3">
              <label for="password" class="form-label">Password</label>
              <input
                type="password"
                id="password"
                class="form-control"
                v-model="pass"
                required
              />
            </div>
            <div class="form-group mb-3">
              <label for="confirmPassword" class="form-label">Confirm Password</label>
              <input
                type="password"
                id="confirmPassword"
                class="form-control"
                v-model="confirmPassword"
                required
              />
            </div>
            <button type="submit" class="btn btn-primary w-100">Register</button>
          </form>
          <div class="mt-3 text-center">
            <p>
              Already have an account? 
              <router-link to="/login">Login here</router-link>
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
    name: 'RegisterPage',
    setup() {
      const router = useRouter();
      const email = ref('');
      const pass = ref('');
      const confirmPassword = ref('');
  
      const register = async () => {
        if (pass.value !== confirmPassword.value) {
          alert('Passwords do not match!');
          return;
        }
  
        try {
          await axios.post('http://localhost:7082/api/v1/register', {
            email: email.value,
            pass: pass.value,
          });
  
          alert('Registration successful. You can now log in.');
          router.push('/login');
        } catch (error) {
          console.error('Registration failed', error);
          alert('Registration failed. Please try again.');
        }
      };
  
      return {
        email,
        pass,
        confirmPassword,
        register,
      };
    },
  });
  </script>
  
  <style scoped>
  .container {
    margin-top: 50px;
  }
  </style>