import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Cliente } from '../model/cliente';
import { Observable } from 'rxjs';
import { environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http:HttpClient) { }


  getCustomer(): Observable<Cliente[]> {
    return  this.http.get<Cliente[]>(environment.URLSERVICIO+"Customer");
    }

  agregarCliente(cliente:Cliente):Observable<Cliente>{  
    /*var headers = new HttpHeaders({
      'Content-Type': 'application/x-www-form-urlencoded'
    });
    return this.http.post<any>("http://localhost:53128/crearCliente", cliente, { headers: headers });;*/
    let body = JSON.stringify(cliente);
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8'});
    return this.http.post<Cliente>(environment.URLSERVICIO+"Customer", cliente, { headers: headers });
  }


  actualizarCliente(Customer:Cliente):Observable<any>{  
    debugger;
    let body = JSON.stringify(Customer);
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8'});
    return this.http.put(environment.URLSERVICIO+'Customer/'+Customer.CustomerID,Customer,{ headers: headers });
  }



  eliminarCliente(Customer:Cliente):Observable<any>{  
    debugger;
    return  this.http.delete(environment.URLSERVICIO+"Customer/"+Customer.CustomerID);
  }

  getCustomerFilter(id: string): Observable<Cliente[]>{
    if(id == ""){
      return this.getCustomer();
    }else{
      return this.http.get<Cliente[]>(environment.URLSERVICIO+"Customer/"+id);
    }
  }
}
