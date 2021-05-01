import { Component, OnInit } from '@angular/core';
import { Contacto } from '../Contacto';
import { ContactoService } from '../contacto.service';

@Component({
  selector: 'app-listacontactos',
  templateUrl: './listacontactos.component.html',
  styleUrls: ['./listacontactos.component.css']
})
export class ListacontactosComponent implements OnInit {

  constructor(private contactoservice: ContactoService) { }

  public listcontactos: Contacto[];
  criterio: string = "";

  ngOnInit() {
    this.listadocontactos();
  }

  listadocontactos() {
    this.contactoservice.getContactos()
      .subscribe(contactos =>
        this.listcontactos = contactos,
        error => console.error(error)
      );
  }

  buscacontactos() {
    this.contactoservice.getBuscaContactos(this.criterio)
      .subscribe(contactos =>
        this.listcontactos = contactos,
        error => console.error(error)
      );
  }

  deletecontacto(contacto: Contacto) {
    this.contactoservice.deleteContacto(contacto.id.toString())
      .subscribe(listadocontactos => this.listadocontactos(),
        error => console.error(error));
  }
}
