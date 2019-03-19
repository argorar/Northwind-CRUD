import { Injectable } from '@angular/core';
import { Categoria } from '../model/categoria';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  getCategorias(): Observable<Categoria[]> {
    return this.http.get<Categoria[]>(environment.URLSERVICIO + "Categories");
  }

  //metodo para buscar un pedido 
  getCategoriasFilter(id: string): Observable<Categoria[]> {
    debugger;
    if (id == "") {
      return this.getCategorias();
    } else {
      return this.http.get<Categoria[]>(environment.URLSERVICIO + "Categories/" + id);
    }
  }
}
