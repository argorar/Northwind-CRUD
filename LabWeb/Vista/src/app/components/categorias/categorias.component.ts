import { Component, OnInit } from '@angular/core';
import { Categoria } from '../../model/categoria';
import { CategoryService } from '../../services/category.service';
@Component({
  selector: 'categorias',
  templateUrl: './categorias.component.html',
  styleUrls: ['./categorias.component.scss']
})
export class CategoriasComponent implements OnInit {

  private categorias: Categoria[];
  constructor(private service:CategoryService) { }

  ngOnInit() {
    this.getCategorias();
  }

  getCategorias(): void{
    debugger;
    this.service.getCategorias().subscribe( data => this.categorias=data);
  }

  buscarCategorias(id: string): void{
    this.service.getCategoriasFilter(id)
    .subscribe(data =>{
      this.categorias = data
    });
  } 

}
