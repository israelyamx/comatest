<template>    
    <main class="form-signin w-100 m-auto">
      <form>
        <h1 class="h3 mb-3 fw-normal">Registro de usuarios</h1>

        <div class="form-floating">
          <input type="text" class="form-control" id="floatingInput">
          <label for="floatingInput">Nombres</label>
        </div>
        <div class="form-floating">
          <input type="text" class="form-control" id="floatingInput">
          <label for="floatingInput">Apellidos</label>
        </div>
    
        <div class="form-floating">
          <input type="email" class="form-control" id="floatingInput" placeholder="name@example.com">
          <label for="floatingInput">Email</label>
        </div>
        <div class="form-floating">
          <input type="password" class="form-control" id="floatingPassword" placeholder="Password">
          <label for="floatingPassword">Contraseña</label>
        </div>

        <div class="form-floating">
          <select name="rolid" class="form-control" id="rol">
            <option value="0" selected>Seleccione una opción....</option>
            <option value="1">Cliente</option>
            <option value="2">Gerente</option>
            <option value="3">Administrador</option>
          </select>
          <label for="floatingPassword">Rol</label>
        </div>
        
        <button class="btn btn-primary w-100 py-2" type="submit">Registrarme</button>
      </form>
    </main>
    </template>
    
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
    