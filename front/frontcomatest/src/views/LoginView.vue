<template>    
<main class="form-signin w-100 m-auto">
  <form @submit.prevent="login">
    <h1 class="h3 mb-3 fw-normal">Por favor, inicia sesión</h1>
    <div v-if="success" class="alert alert-success alert-dismissible fade show" role="alert">
        <strong>Sesión iniciada con éxito</strong>.
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>

    <div class="form-floating">
      <select v-model="tipo" type="tipo" class="form-control">
        <option value="0">Seleccione una opcion...</option>
        <option value="1">Anonimo</option>
        <option value="2">Gerente</option>
        <option value="3">Administrador</option>
      </select>
      <label for="floatingInput">Tipo de usuario</label>
    </div>
    <button class="btn btn-primary w-100 py-2" type="submit">Iniciar sesión</button>
  </form>
</main>
</template>

<script>
import axios from 'axios';
var logintipo =  {tipo:0}
export default{
  name:'login',
  data(){
      return{
          tipo: logintipo.tipo,
          success : false
      }
  },
  methods : {
    login(){
      axios.get(`http://localhost:5287/api/Usuario/GenerarTokens?id=` + this.tipo)
            .then(response => {
                // JSON responses are automatically parsed.
                var token = response.data;
                console.log(token)
                localStorage.setItem('token', token)
              this.success = true
            })
            .catch(e => {
                console.log(e)
            });
    }
  }
}
</script>

<style scoped>


.form-signin {
  max-width: 330px;
  padding: 1rem;
}

.form-signin .form-floating:focus-within {
  z-index: 2;
}

.form-signin input[type="email"] {
  margin-bottom: -1px;
  border-bottom-right-radius: 0;
  border-bottom-left-radius: 0;
}

.form-signin input[type="password"] {
  margin-bottom: 10px;
  border-top-left-radius: 0;
  border-top-right-radius: 0;
}
</style>
