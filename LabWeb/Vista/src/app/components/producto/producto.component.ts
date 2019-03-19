import { Component, OnInit } from '@angular/core';
import { producto } from '../../model/producto';
import { OrderService } from '../../services/order.service';

@Component({
  selector: 'producto',
  templateUrl: './producto.component.html',
  styleUrls: ['./producto.component.scss']
})
export class ProductoComponent implements OnInit {
  private productos: producto[];
  constructor(private service:OrderService) { }

  ngOnInit() {
    this.getProductos();
  }

  getProductos(): void{
    this.service.getProductos().subscribe( data => this.productos=data);
  }
}
