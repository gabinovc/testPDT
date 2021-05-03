import { HttpEventType, HttpClient } from '@angular/common/http';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { Contacto } from '../Contacto';
import { ContactoService } from '../contacto.service';

@Component({
  selector: 'app-listacontactos',
  templateUrl: './listacontactos.component.html',
  styleUrls: ['./listacontactos.component.css']
})
export class ListacontactosComponent implements OnInit {

  constructor(private contactoservice: ContactoService, private httpClient: HttpClient) { }

  criterio: string = "";
  public listcontactos: Contacto[];
  public progress: number;
  public message: string;
  @Output() public onUploadFinished = new EventEmitter();

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

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.httpClient.post('upload', formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Completado.';
          this.onUploadFinished.emit(event.body);
        }
      });
  }
}
