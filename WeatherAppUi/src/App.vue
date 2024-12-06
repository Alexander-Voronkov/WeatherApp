<template>
  <div>
    <!-- Фиксированный Навбар -->
    <nav class="navbar navbar-expand-lg navbar-light bg-light fixed-top">
      <div class="container-fluid">
        <a class="navbar-brand" href="#">My App</a>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav">
            <li class="nav-item">
              <RouterLink to="/weather" class="nav-link">Weather</RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink to="/history" class="nav-link">Search History</RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink to="/login" class="nav-link">Login</RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink to="/register" class="nav-link">Register</RouterLink>
            </li>
          </ul>
        </div>
      </div>
    </nav>

    <!-- Отступ для фиксации содержимого под навбаром -->
    <div class="content-wrapper">
      <RouterView />
    </div>
  </div>
</template>

<script setup lang="ts"></script>

<style scoped>
/* Задаём фиксированный отступ для контента */
.content-wrapper {
  margin-top: 56px; /* Высота навбара, чтобы он не перекрывал содержимое */
}

/* Убираем любые подёргивания навбара */
body {
  margin: 0;
}

.navbar {
  width: 100%; /* Занимает всю ширину экрана */
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); /* Лёгкая тень для эстетики */
}

html,
body,
#app {
  height: 100%; /* Высота на весь экран для адаптации */
}

</style>