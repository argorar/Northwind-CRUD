import { Component, OnInit } from '@angular/core';
import { ToasterService } from 'angular2-toaster';
import { CustomerService } from '../../services/customer.service';
import { Cliente } from '../../model/cliente';
import { BaseComponent } from '../../shared/base/base.component';

@Component({
  selector: 'usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.scss']
})
export class UsuarioComponent extends BaseComponent implements OnInit {

  clienteSeleccionado: Cliente = new Cliente();


  constructor(private service: CustomerService, toasterService: ToasterService) {
    super(toasterService);
  }

  ngOnInit() {
  }


  registrarCliente() {
    if (this.clienteSeleccionado.CustomerID != null && this.clienteSeleccionado.CompanyName != null && this.clienteSeleccionado.ContactName != null && this.clienteSeleccionado.City != null && this.clienteSeleccionado.Phone != null) {
      this.service.agregarCliente(this.clienteSeleccionado)
        .subscribe(
          data => {
            console.log(data);
            this.showToast("success", "Cliente Creado", "Registro procesado correctamente");
          },

          error => {
            console.log(error);
            this.showToast("error", "Error", "Registro con error");
          }

        );
      this.clienteSeleccionado = new Cliente();
    }
    else
    this.showToast("error", "Error", "Debes llenar todos los campos");
  }

}
