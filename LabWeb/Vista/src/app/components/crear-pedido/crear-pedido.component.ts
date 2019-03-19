import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { producto } from '../../model/producto';
import { Observable } from 'rxjs';
import { pedido } from '../../model/pedido';
import { Cliente } from '../../model/cliente';
import { CustomerService } from '../../services/customer.service';
import { OrderService } from '../../services/order.service';
import { BaseComponent } from '../../shared/base/base.component';
import { ToasterService } from 'angular2-toaster';

@Component({
  selector: 'crear-pedido',
  templateUrl: './crear-pedido.component.html',
  styleUrls: ['./crear-pedido.component.scss']
})
export class CrearPedidoComponent extends BaseComponent implements OnInit {
  modalReference: any;
  private listaProductos: producto[];
  private listaDetalles: any[];
  public registroPedido: pedido;
  private listaClientes: Cliente[];

  productoSeleccionado: producto;
  clienteSeleccionado: Cliente;
  precioUnitario: number;
  total: number;
  cantidad: number;
  totalPedido: number;

  constructor(private modalService: NgbModal, private service: OrderService, private customerService: CustomerService, toasterService:ToasterService) {
    super(toasterService);
    this.totalPedido = 0;
    this.listaDetalles = [];
    this.registroPedido = new pedido();    
  }

  ngOnInit() {
    this.getProductos();
    this.getCustomers();
  }
  //metodo para traer la lista de clientes 
  getCustomers(): void {
    this.customerService.getCustomer()
      .subscribe(data => { this.listaClientes = data },
        error => {
          console.log(error);
        }
      );
  }
  //metodo para traer la lista de productos 
  getProductos(): void {
    this.service.getProductos()
      .subscribe(data => {
        console.log(data);
        this.listaProductos = data
      },
        error => { console.log(error); });
  }
  //metodo para abrir el modal 
  open(content) {
    this.modalReference = this.modalService.open(content).result.then((result) => {
      console.log("closed");
    }, (reason) => {
      console.log("dismissed");
    });
  }
  //metodo para poner automaticamente el precio unitario 
  onChangeProducto(event) {
    this.precioUnitario = event.UnitPrice;
  }
  //metodo para el calculo automatico del total del pedido 
  onChangeTotal(valor) {
    this.cantidad = valor;
    this.total = valor * this.precioUnitario;
  }
  //metodo para insertar un producto al pedido 
  agregarProducto(product: producto): void {
    this.listaDetalles.push(
      {
        OrderlD: undefined,
        ProductID: `${product.ProductID}`,
        ProductName: product.ProductName,
        UnitPrice: product.UnitPrice,
        Quantity: this.cantidad,
        Total: this.total
      }
    );
    this.totalPedido = this.totalPedido + this.total;
  }
  //metodo para insertar un pedido 
  agregarPedido(unPedido: pedido): void {
    debugger;
    unPedido.Freight = this.totalPedido;
    unPedido.Order_Details = this.listaDetalles;
    unPedido.CustomerID = this.clienteSeleccionado.CustomerID;
    unPedido.OrderDate = this.registroPedido.OrderDate;
    unPedido.RequiredDate=this.registroPedido.RequiredDate;
    unPedido.ShippedDate=this.registroPedido.ShippedDate;
    
    if (this.registroPedido.OrderDate != undefined && this.registroPedido.RequiredDate != undefined && this.registroPedido.ShippedDate != undefined && this.registroPedido.Freight != undefined && this.totalPedido != 0) {
      if (unPedido != null) {
        this.service.agregarPedido(unPedido).subscribe(data => {
          console.log(data);
          this.showToast("success", "Pedido Agregado", "Registro procesado correctamente")
          this.registroPedido = new pedido();
          this.listaDetalles = [];
          this.totalPedido = 0;
        },
          error => {
            console.log(error);
          }
        );
      } else {
        this.showToast("error", "Registro con Error", "Pedido NO Registrado");
      }
    } else {
      this.showToast("error", "Registro con Error", "Registro debe contener todos los campos");
    }
  }

}
