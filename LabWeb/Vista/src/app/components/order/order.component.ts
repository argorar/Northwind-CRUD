import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { pedido } from '../../model/pedido';
import { detallePedido } from '../../model/detallePedido';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {
  private pedidos: pedido[];
  private lineas: detallePedido[];
  private accion: string;
  modalReference: any;

  constructor(private service: OrderService, private modalService: NgbModal) { }

  ngOnInit() {
    this.getPedidos();
  }
  //metodo para mostrar la lista de pedidos 
  getPedidos(): void {
    this.service.getPedidos()
      .subscribe(data => {
        debugger;
        console.log(data);
        this.pedidos = data
      },
        error => {
          console.log(error);
        });
  }
  //metodo para ver la lista de detalles 
  verDetalles(order: pedido): void {
    debugger;
    console.log(order);
    this.lineas = [];

    for (let i = 0; i < order.Order_Details.length; i++) {
      this.lineas.push({
        OrderID: order.Order_Details[i].OrderID,
        ProductID: order.Order_Details[i].ProductID,
        UnitPrice: order.Order_Details[i].UnitPrice,
        Quantity: order.Order_Details[i].Quantity
      });
    }
    console.log(this.lineas);
  }
  //metodo para abrir el modal 
  open(content, unPedido: pedido, estado: string) {
    this.accion = estado;
    if (unPedido) {
      debugger;
      this.verDetalles(unPedido);
    }
    else {
      this.lineas = [];
    }
    this.modalReference = this.modalService.open(content).result.then((result) => {
      console.log("closed");
    }, (reason) => {
      console.log("dismissed");
    });
  }
  //metodo para eliminar un pedido 
  eliminarPedido(unPedido: pedido) {
    this.service.eliminarPedido(unPedido).subscribe(
      (x) => x);
    this.pedidos = this.pedidos.filter(h => h !== unPedido);
  }
  //metodo para buscar un pedido especifico 
  buscarPedido(id: string): void {
    this.service.getPedidoFilter(id)
      .subscribe(data => {
        this.pedidos = data
      });
  }
}