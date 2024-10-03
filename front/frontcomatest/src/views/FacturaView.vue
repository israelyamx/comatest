<script setup>
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
    <h1>Facturas</h1>
    <div class="d-grid gap-2 d-sm-flex justify-content-sm-end">
    <button v-if="!unauthorize" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal" type="button"><i class="fa fa-add"></i> Agregar</button>
    </div>
    <div v-if="badrequest" class="alert alert-warning alert-dismissible fade show" role="alert">
        <strong>No autorizado!</strong> Se requieren permisos de administrador para eliminar facturas.
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
    <div v-if="unauthorize" class="alert alert-warning alert-dismissible fade show" role="alert">
        <strong>No autorizado!</strong> Se requiere autorizacion para listar facturas.
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
    <table class="table">
        <thead>
            <th>folio</th>
            <th>saldo</th>
            <th>fecha creacion</th>
            <th>fecha facturacion</th>
        </thead>
        <tbody>
            <tr v-for="factura in facturas">
                <td>{{factura.folio}}</td>
                <td>{{factura.saldo}}</td>
                <td>{{factura.fecha_creacion}}</td>
                <td>{{factura.fecha_facturacion}}</td>
                <td><button type="button" class="btn btn-primary"><i class="fa fa-pencil"></i></button> | <button type="button" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#deleteModal" v-on:click="eliminar(factura.id, factura.folio)"><i class="fa fa-times"></i></button></td>
            </tr>
        </tbody>
    </table>
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
                        <label class="form-label">Folio</label>
                        <input type="text" v-model="addFactura.folio" class="form-control" name="nombre" required>
                    </div>
                    
                    <div class="mb-3">
                        <label class="form-label">Saldo</label>
                        <input type="text" v-model="addFactura.saldo" class="form-control" name="apellido" required>
                    </div>
                    <div class="mb-3">
                        <label for="exampleInputEmail1" class="form-label">Fecha creación</label>
                        <input type="date" v-model="addFactura.fecha_creacion" name="correo" class="form-control" id="exampleInputEmail1" required>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Fecha facturacion</label>
                        <input type="date" v-model="addFactura.fecha_facturacion" class="form-control" name="edad" required>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Cliente</label>
                        <input type="number" v-model="addFactura.id_usuario" class="form-control" name="edad" required>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <button type="submit" class="btn btn-primary">Agregar</button>    
                </div>
            </form>
            </div>
        </div>
    </div>
    <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
            <div class="modal-header">
                <h1 class="modal-title fs-5" id="exampleModalLabel">Eliminar factura</h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <form @submit.prevent="onDelete">
                <div class="modal-body">       
                    <h5>¿Seguro que deseas eliminar la factura {{ deleteFactura.folio }}?</h5>
                    <input type="hidden" name="id" v-model="deleteFactura.id">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <button type="submit" class="btn btn-danger">Eliminar</button>    
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
var agrergarFactura = {id: 0, folio: '', saldo : '', fecha_creacion:'', fecha_facturacion:'', id_usuario:0}
var eliminarFactura = {id: 0, folio : ''}

export default {
    name: 'consume-rest-api',
    data(){
        return{
            facturas: [],
            addFactura : agrergarFactura,
            deleteFactura : eliminarFactura,
            badrequest : false,
            unauthorize : false
        }
    },
    created() {
       this.cargar() 
    },
    methods: {
        eliminar(id, folio){
            this.deleteFactura.id = id
            this.deleteFactura.folio = folio
        },
        cargar() {
            axios.get(`http://localhost:5287/api/Factura/ListaFacturas`)
            .then(response => {
                // JSON responses are automatically parsed.
                this.facturas = response.data
                console.log(response.data)
            })
            .catch(e => {
                this.unauthorize = true
            });
        },
        onDelete(form){
            console.log('me estan enviando')
            console.log(this.addFactura)
            axios.delete('http://localhost:5287/api/Factura/EliminarFacturas?id=' + this.deleteFactura.id).then((res)=>{
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
        onSubmit(form){
            console.log('me estan enviando')
            console.log(this.addFactura)
            axios.post('http://localhost:5287/api/Factura/AgregarFacturas', this.addFactura).then((res)=>{
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
