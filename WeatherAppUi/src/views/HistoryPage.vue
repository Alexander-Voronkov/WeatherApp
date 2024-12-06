<template>
  <div>
    <h1>Search history</h1>

    <Loader v-if="loading" />
    
    <div v-if="error" class="error">{{ error }}</div>

    <div v-if="history.length">
      <ul>
        <li v-for="(item, index) in history" :key="index">
          <strong>{{ item.city }}</strong> — {{ item.requestedAt }}
          <pre>{{ item.rawJsonData }}</pre>
        </li>
      </ul>
    </div>
    <div v-else>Search history is clear.</div>
  </div>
</template>

<script lang="ts">
import { ref } from 'vue';
import axios from 'axios';
import Loader from '../components/Loader.vue';

export default {
  name: 'SearchHistory',
  setup() {
    const history = ref<any[]>([]);
    const loading = ref(false);
    const error = ref<string | null>(null);

    const fetchHistory = async () => {
      loading.value = true;
      error.value = null; 

      try {
        const accesstoken = localStorage.getItem('access_token');
        console.log(accesstoken);
        const response = await axios.get('http://localhost:7082/api/v1/history', {
          headers: {
            'Authorization': `Bearer ${accesstoken}`,
          },
        });

        history.value = response.data.searchResults;
      } catch (err) {
        error.value = 'Error.';
      } finally {
        setTimeout(() => {
          loading.value = false;
        }, 2000); 
      }
    };

    fetchHistory();

    return {
      history,
      loading,
      error,
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