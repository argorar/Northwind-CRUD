import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsuarioComponent } from './usuario/usuario.component';
import { ComponentRoutingModule } from './component-rounting';
import { ThemeModule } from '../@theme/theme.module';
import { ClienteComponent } from './cliente/cliente.component';
import { ToasterModule, ToasterService } from 'angular2-toaster';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { OrderComponent } from './order/order.component';
import { CrearPedidoComponent } from './crear-pedido/crear-pedido.component';
import { CategoriasComponent } from './categorias/categorias.component';
import { ProductoComponent } from './producto/producto.component';

@NgModule({
  imports: [
    CommonModule,
    ComponentRoutingModule,
    ThemeModule,
    ToasterModule,
    Ng2SmartTableModule
  ],
  declarations: [UsuarioComponent, ClienteComponent, OrderComponent, CrearPedidoComponent, CategoriasComponent, ProductoComponent],
  providers: [ToasterService]
})
export class ComponentModule { }
