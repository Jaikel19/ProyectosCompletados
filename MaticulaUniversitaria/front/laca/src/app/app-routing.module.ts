import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./ui-components/home/home.component";
import {EstudianteHomeComponent} from "./ui-components/estudiante/estudiante-home.component";
import {CursoHomeComponent} from "./ui-components/curso/curso-home.component";
import {MatriculaHomeComponent} from "./ui-components/matricula/matricula-home.component";

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'estudiante', component: EstudianteHomeComponent },
  { path: 'curso', component: CursoHomeComponent },
  { path: 'matricula', component: MatriculaHomeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
