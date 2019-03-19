import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { pedido } from '../model/pedido';
import { producto } from '../model/producto';
import { environment} from '../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class OrderService { 
  constructor(private http:HttpClient) { } 
  //metodo para traer los pedidos 
  getPedidos(): Observable<pedido[]> { 
    return this.http.get<pedido[]>(environment.URLSERVICIO+"Order"); 
  } 
  //metodo para eliminar un pedido 
  eliminarPedido(order:pedido):Observable<any>{ 
    return this.http.delete(environment.URLSERVICIO+"Order/"+order.OrderID); 
  } 
  //metodo para buscar un pedido 
  getPedidoFilter(id: string): Observable<pedido[]>{ 
    debugger;
    if(id==""){ 
      return this.getPedidos(); 
    }else{ 
      return this.http.get<pedido[]>(environment.URLSERVICIO+"Order/"+id); 
    } 
  } 
  //metodo para traer la lista de productos de los pedidos 
  getProductos(): Observable<producto[]> { 
    return this.http.get<producto[]>(environment.URLSERVICIO+"Product"); 
  } 
  //metodo para insertar un pedido en la BD  
  agregarPedido(unPedido: pedido): any { 
    debugger; let body = JSON.stringify(unPedido); 
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8'}); 
    return this.http.post<pedido>(environment.URLSERVICIO+"Order", unPedido, { headers: headers }); 
    } 
  } 
  