import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ContactoService } from '../contacto.service';
import { Contacto } from '../Contacto';


@Component({
  selector: 'app-nuevocontacto',
  templateUrl: './nuevocontacto.component.html',
  styleUrls: ['./nuevocontacto.component.css'],
  providers: [DatePipe]
})
export class NuevocontactoComponent implements OnInit {

  constructor(
      private datePipe: DatePipe,
      private router: Router,
      private contactoserv: ContactoService,
      private fb: FormBuilder,
      private activatedRoute: ActivatedRoute) {

    this.myDateString = this.datePipe.transform(this.myDate, 'dd-MM-yyyy');
  }

  myDateString: string;
  myDate: number = Date.now();
  modoEdicion: boolean = false;
  formGroup: FormGroup;
  contactoId: number;
  ignorarExistenCambiosPendientes: boolean = false;

  public contacto: Contacto;

  ngOnInit() {
    this.formGroup = this.fb.group({
      nombre: '',
      direccion: '',
      telefono: '',
      curp: '',
      fechaRegistro: null
    });

    this.activatedRoute.params.subscribe(params => {
      if (params["id"] == undefined) {
        this.modoEdicion = false;
        return;
      }
      this.modoEdicion = true;
      this.contactoId = params["id"];

      this.contactoserv.getContacto(this.contactoId.toString())
        .subscribe(c =>
          console.table(c), error => console.error(error)
      );

      this.contactoserv.getContacto(this.contactoId.toString())
        .subscribe(c => 
          this.formGroup.patchValue({
            nombre: c.nombre,
            direccion: c.direccion,
            telefono: c.telefono,
            curp: c.curp,
            fechaRegistro: this.datePipe.transform(c.fechaRegistro, 'dd-MM-yyyy') //c.fechaRegistro
          }),
          error => console.error(error)
        );
    });
  }

  existenCambiosPendientes(): boolean {
    if (this.ignorarExistenCambiosPendientes) { return false; };
    return !this.formGroup.pristine;
  }

  addContacto() {
    this.ignorarExistenCambiosPendientes = true;

    let contactodatos: Contacto = Object.assign({}, this.formGroup.value);

    console.table(contactodatos);

    if (this.modoEdicion) {
      //actualizar con los datos modificados
      contactodatos.id = this.contactoId;
      this.contactoserv.updateContacto(contactodatos)
        .subscribe(c => this.router.navigate(["/"])),
        error => console.error(error)
    }
    else {
      this.contactoserv.addContacto(contactodatos)
        .subscribe(c =>
          this.router.navigateByUrl('/'),
          error => console.error(error)
        );
    }
  }
}
