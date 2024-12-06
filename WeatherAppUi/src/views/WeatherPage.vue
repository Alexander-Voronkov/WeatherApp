<template>
  <div>
    <h1>Search weather</h1>
    
    <input class="form-control"  v-model="city" type="text" placeholder="Enter city name" />
    <button class="btn btn-primary w-100 mt-3" @click="handleSearch">Search</button>

    <Loader v-if="loading" />
  
    <div v-if="error" class="error">{{ error }}</div>
  
    <div v-if="weatherData">
      <h2>Weather in the city: {{ weatherData.name }}</h2>
      <p>Temperature: {{ weatherData.main.temp }}°C</p>
      <p>Description: {{ weatherData.weather[0].description }}</p>
      <p>Humidity: {{ weatherData.main.humidity }}%</p>
      <p>Wind speed: {{ weatherData.wind.speed }} м/с</p>
    </div>
  </div>
</template>

<script lang="ts">
import { ref } from 'vue';
import axios from 'axios';
import Loader from '../components/Loader.vue';

export default {
  name: 'WeatherSearch',
  components: {
    Loader, // Регистрируем компонент Loader
  },
  setup() {
    const city = ref('');
    const weatherData = ref<any>(null);
    const loading = ref(false);
    const error = ref<string | null>(null);

    const handleSearch = async () => {
      if (!city.value) {
        error.value = 'Enter city name';
        return;
      }

      loading.value = true;
      error.value = null;

      try {
        const accesstoken = localStorage.getItem('access_token');
        const response = await axios.get(`http://localhost:7082/api/v1/search/${city.value}`, {
          headers: {
            'Authorization': `Bearer ${accesstoken}`,
          },
        });
        weatherData.value = response.data;
      } catch (err) {
        error.value = 'Error fetching data.';
      } finally {
        setTimeout(() => {
          loading.value = false;
        }, 2000); 
      }
    };

    return {
      city,
      weatherData,
      loading,
      error,
      handleSearch,
    };
  },
};
</script>

<style scoped>
.error {
  color: red;
  font-weight: bold;
}
</style>