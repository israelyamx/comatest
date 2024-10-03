<script setup>
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
    <h1>Clientes</h1>
    <div class="d-grid gap-2 d-sm-flex justify-content-sm-end">
    <button class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal" type="button"><i class="fa fa-add"></i> Agregar</button>
    </div>
    <div v-if="badrequest" class="alert alert-warning alert-dismissible fade show" role="alert">
        <strong>No autorizado!</strong> Se requieren permisos de administrador para eliminar clientes.
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
    <table class="table">
        <thead>
            <th>Nombre</th>
            <th>Apellido</th>
            <th>Correo</th>
            <th>Acciones</th>
        </thead>
        <tbody>
            <tr v-for="usuario in usuarios">
                <td>{{usuario.nombre}}</td>
                <td>{{usuario.apellido}}</td>
                <td>{{usuario.correo}}</td>
                <td><button type="button" class="btn btn-primary"><i class="fa fa-pencil"></i></button> | <button type="button" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#deleteModal" v-on:click="eliminar(usuario.id, usuario.nombre)"><i class="fa fa-times"></i></button></td>
            </tr>
        </tbody>
    </table>
    <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
            <div class="modal-header">
                <h1 class="modal-title fs-5" id="exampleModalLabel">Eliminar Cliente</h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <form @submit.prevent="onDelete">
                <div class="modal-body">       
                    <h5>¿Seguro que deseas eliminar al usuario {{ deleteuser.nombre }}?</h5>
                    <input type="hidden" name="id" v-model="deleteuser.id">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <button type="submit" class="btn btn-danger">Eliminar</button>    
                </div>
            </form>
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="exampleModalLabel">Agregar cliente</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <form @submit.prevent="onSubmit">
        <div class="modal-body">       
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <input type="text" v-model="adduser.nombre" class="form-control" name="nombre" required>
            </div>
            
            <div class="mb-3">
                <label class="form-label">Apellido</label>
                <input type="text" v-model="adduser.apellido" class="form-control" name="apellido" required>
            </div>
            <div class="mb-3">
                <label for="exampleInputEmail1" class="form-label">Correo</label>
                <input type="email" v-model="adduser.correo" name="correo" class="form-control" id="exampleInputEmail1" required>
            </div>
            <div class="mb-3">
                <label class="form-label">Edad</label>
                <input type="number" v-model="adduser.edad" class="form-control" name="edad" required>
            </div>
            <div class="mb-3">
                <label for="exampleInputEmail1" class="form-label">Tipo usuario</label>
                <select v-model="adduser.tipo_usuario" class="form-control" name="tipousuario" id="" required>
                    <option value="0">Seleccione una opción...</option>
                    <option value="1">Cliente</option>
                    <option value="2">Gerente</option>
                    <option value="3">Administrador</option>
                </select>
            </div>
            
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="submit" class="btn btn-primary">Save changes</button>    
      </div>
    </form>
    </div>
  </div>
</div>
</template>

<script>
import axios from 'axios';
axios.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('token')

        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }

        return config;
    },

    (error) => {
        return Promise.reject(error);
    }
);
var agregarUsuario = {nombre: '', apellido : '', correo:'', edad:0, tipo_usuario:0}
var eliminarUsuario = {nombre:'', id:0}
export default {
    name: 'consume-rest-api',
    data(){
        return{
            usuarios: [],
            adduser : agregarUsuario,
            deleteuser : eliminarUsuario,
            badrequest : false
        }
    },
    created() {
       this.cargar() 
    },

    methods: {
        eliminar(id, nombre){
            this.deleteuser.id = id
            this.deleteuser.nombre = nombre
        },
        onDelete(form){
            console.log('me estan enviando')
            console.log(this.addFactura)
            axios.delete('http://localhost:5287/api/Usuario/EliminarUsuarios?id=' + this.deleteuser.id).then((res)=>{
                console.log(res);
                location.reload();
            }).catch((error) => {
                // error.response.status Check status code
                if(error.status == 403){
                    $('#deleteModal').modal('hide');
                    this.badrequest = true
                }
            });
        },
        cargar() {
            axios.get(`http://localhost:5287/api/Usuario/ListaUsuarios`)
            .then(response => {
                // JSON responses are automatically parsed.
                this.usuarios = response.data
                console.log(response.data)
            })
            .catch(e => {
                this.errors.push(e)
            });
        },
        onSubmit(form){
            console.log('me estan enviando')
            console.log(this.adduser)
            axios.post('http://localhost:5287/api/Usuario/AgregarUsuarios', this.adduser).then((res)=>{
                console.log(res);
            }).catch((error) => {
                     // error.response.status Check status code
                     console.log(error)
                 }).finally(() => {
                     //Perform action in always
                     location.reload();
                 });
        }
    }
}
</script>
