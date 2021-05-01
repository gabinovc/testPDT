import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Contacto } from './Contacto';

@Injectable({
  providedIn: 'root'
})
export class ContactoService {

  constructor(private httpClient: HttpClient) { }

  //Leer los contactos
  getContactos(): Observable<Contacto[]> {
    return this.httpClient.get<Contacto[]>('Contactos')
  }

  getBuscaContactos(criterio: string): Observable<Contacto[]> {
    return this.httpClient.get<Contacto[]>('Contactos/Busca/' + criterio)
  }

  //Lee solo 1
  getContacto(contactoId: string): Observable<Contacto> {
    return this.httpClient.get<Contacto>('Contactos/' + contactoId);
  }


  //Agrega contacto
  addContacto(contacto: Contacto): Observable<Contacto> {
    return this.httpClient.post<Contacto>('Contactos', contacto);
  }

  //Actualizar contacto
  updateContacto(contacto: Contacto): Observable<Contacto> {
    return this.httpClient.put<Contacto>('Contactos/' + contacto.id.toString(), contacto);
  }

  //Borra contacto
  deleteContacto(contactoId: string): Observable<Contacto> {
    return this.httpClient.delete<Contacto>('Contactos/' + contactoId);
  }
}
