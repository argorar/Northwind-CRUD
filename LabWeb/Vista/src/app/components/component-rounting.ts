import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { UsuarioComponent } from './usuario/usuario.component';
import { ProductoComponent } from './producto/producto.component';
import { ClienteComponent } from './cliente/cliente.component';
import { OrderComponent} from './order/order.component';
import { CategoriasComponent} from './categorias/categorias.component';
import { CrearPedidoComponent} from './crear-pedido/crear-pedido.component';

const routes: Routes = [
    { path: 'usuarios', component: UsuarioComponent },
    { path: 'productos', component: ProductoComponent },
    { path: 'cliente', component: ClienteComponent },
    { path: 'pedido', component: OrderComponent },
    { path: 'crear-pedido', component: CrearPedidoComponent },
    { path: 'categorias', component: CategoriasComponent },
    { path: '', pathMatch:'full',redirectTo:'usuarios' },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ComponentRoutingModule {} 
