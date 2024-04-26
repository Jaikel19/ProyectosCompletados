import { Component } from '@angular/core';
import {CursoService} from "../../../logic-components/services/CursoService";

@Component({
  selector: 'app-curso-form',
  templateUrl: './curso-form.component.html',
  styleUrls: ['./curso-form.component.css']
})
export class CursoFormComponent {
  curso: any = {};

  constructor(private cursoService: CursoService) {}

  onSubmit() {
    // Envía el nuevo curso al servidor a través del servicio si es necesario
    this.cursoService.addCurso(this.curso).subscribe(
      (response: any) => {
        console.log('Curso agregado exitosamente:', response);
        // Puedes redirigir o realizar otras acciones después de agregar el curso
      },
      (error: any) => {
        console.error('Error al agregar el curso:', error);
        // Maneja el error apropiadamente
      }
    );
  }
}
