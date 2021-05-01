import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ListacontactosComponent } from './listacontactos/listacontactos.component';
import { NuevocontactoComponent } from './nuevocontacto/nuevocontacto.component';
import { InfoComponent } from './info/info.component';
import { ContactoService } from './contacto.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,

    ListacontactosComponent,

    NuevocontactoComponent,

    InfoComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: ListacontactosComponent, pathMatch: 'full' },
      { path: 'nuevocontacto', component: NuevocontactoComponent },
      { path: 'editcontacto/:id', component: NuevocontactoComponent },
      { path: 'info', component: InfoComponent },
    ])
  ],
  providers: [ContactoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
