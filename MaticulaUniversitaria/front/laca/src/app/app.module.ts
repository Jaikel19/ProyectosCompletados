import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HomeComponent } from './ui-components/home/home.component';
import {HttpClientModule} from "@angular/common/http";
import {FormsModule} from "@angular/forms";
import {NgbModule} from "@ng-bootstrap/ng-bootstrap";
import { EstudianteHomeComponent } from './ui-components/estudiante/estudiante-home.component';
import { EstudianteListComponent } from './ui-components/estudiante/estudiante-list/estudiante-list.component';
import { EstudianteFormComponent } from './ui-components/estudiante/estudiante-form/estudiante-form.component';
import { EstudianteEditModalComponent } from './ui-components/estudiante/estudiante-edit-modal/estudiante-edit-modal.component';
import { CursoHomeComponent } from './ui-components/curso/curso-home.component';
import { CursoListComponent } from './ui-components/curso/curso-list/curso-list.component';
import { CursoFormComponent } from './ui-components/curso/curso-form/curso-form.component';
import { CursoEditModalComponent } from './ui-components/curso/curso-edit-modal/curso-edit-modal.component';
import { MatriculaHomeComponent } from './ui-components/matricula/matricula-home.component';
import { MatriculaListComponent } from './ui-components/matricula/matricula-list/matricula-list.component';
import { MatriculaFormComponent } from './ui-components/matricula/matricula-form/matricula-form.component';
import { MatriculaEditModalComponent } from './ui-components/matricula/matricula-edit-modal/matricula-edit-modal.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    EstudianteHomeComponent,
    EstudianteListComponent,
    EstudianteFormComponent,
    EstudianteEditModalComponent,
    CursoHomeComponent,
    CursoListComponent,
    CursoFormComponent,
    CursoEditModalComponent,
    MatriculaHomeComponent,
    MatriculaListComponent,
    MatriculaFormComponent,
    MatriculaEditModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
