import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, NgForm } from '@angular/forms';
import { CustomerService } from '../../services/customer.service';
import { Cliente } from '../../model/cliente';
import {NgbModal, ModalDismissReasons, NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import { supportsWebAnimations } from '@angular/animations/browser/src/render/web_animations/web_animations_driver';
import { BaseComponent } from '../../shared/base/base.component';
import { ToasterService } from 'angular2-toaster';

@Component({
  selector: 'cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.scss']
})
export class ClienteComponent extends BaseComponent implements OnInit {

  clienteSeleccionado:Cliente = new Cliente();

  private clientes: Cliente[];
  private accion : string;

  modalReference:any; 
  pacienteSeleccionado:Cliente = {
    CustomerID: '',
    CompanyName: '',
    ContactName: '',
    City:'',
    Phone: ''   
  };;

  constructor(private service:CustomerService,private modalService: NgbModal,private activeModal: NgbActiveModal, toasterService:ToasterService) { 
   super(toasterService);
  }

  ngOnInit() {
    this.getClientes();
  }
  getClientes(): void {
    this.service.getCustomer()
    .subscribe(data =>{
      console.log(data);
      this.clientes = data},
      error => {
        console.log(error);
      });
  }

  open(content, unCliente:Cliente,estado:string) {
  
    this.accion = estado;

    if (unCliente)
    {
      this.clienteSeleccionado = unCliente;
    }
    else
    this.clienteSeleccionado = new Cliente();
    console.log(this.clienteSeleccionado);
    this.modalReference = this.modalService.open(content).result.then((result) => {
       console.log("closed");
    }, (reason) => {
       console.log("dismissed" );
    });
 }


  actualizarCliente(content, unCliente:Cliente) {
    this.service.actualizarCliente(unCliente).subscribe(z => this.showToast("success", "Cliente Actualizado", "Registro procesado correctamente"));
 }


  seleccionarPaciente(unCliente:Cliente){
    this.pacienteSeleccionado = unCliente;   
    
  }

  buscarCliente(id: string): void{
    this.service.getCustomerFilter(id)
    .subscribe(data =>{
      this.clientes = data
    });
  } 
  eliminarCliente(unCliente:Cliente){
    debugger;
    this.service.eliminarCliente(unCliente).subscribe(    
      x => this.showToast("success", "Cliente Eliminado", "Registro procesado correctamente")     
      );     
    this.clientes = this.clientes.filter(h => h !== unCliente);
  }

  Save(form: NgForm, c){      
        if(this.clienteSeleccionado!=null)
        {
          debugger;

          if(this.accion=='Guardar'){
            if(this.clienteSeleccionado.CustomerID!=null && this.clienteSeleccionado.CompanyName!=null && this.clienteSeleccionado.ContactName!=null && this.clienteSeleccionado.City!=null && this.clienteSeleccionado.Phone!=null){
              this.service.agregarCliente(this.clienteSeleccionado)
              .subscribe(  
                data =>{
                  this.getClientes(); 
                  this.showToast("success", "Registro Guardado", "Registro procesado correctamente");
                }, 
                
                error =>{
                  console.log(error);
                }
                  
                ); 
                
            }  
            else 
              this.showToast("error", "Registro con Error", "Registro debe contener todos los campos");
          }
          else{
            this.service.actualizarCliente(this.clienteSeleccionado)
            .subscribe(    
              data => {
                this.service.getCustomer();
                this.showToast("success", "Registro Actualizado", "Registro procesado correctamente");
              }, 
              error =>{
                console.log(error);
              }   
            );   
            
          }
        
    c('close modal');        
  }
}
}

