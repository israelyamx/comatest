<script setup>
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
    <nav class="navbar navbar-expand-lg bg-body-tertiary">
      <div class="container-fluid">
        <router-link to="/" class="navbar-brand" href="#">COMA</router-link>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav">
            <li class="nav-item">
              <router-link to="/login" class="nav-link active" aria-current="page">Login</router-link>
            </li>
            <li class="nav-item">
              <router-link to="/clientes" class="nav-link active" aria-current="page">Clientes</router-link>
            </li>
            <li class="nav-item">
              <router-link to="/facturas" class="nav-link active" aria-current="page">Facturas</router-link>
            </li>
          </ul>
        </div>
      </div>
    </nav>
    <div class="container"><RouterView /></div>
  
</template>

<style scoped>
</style>
