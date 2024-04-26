import { Component } from '@angular/core';
import {MatriculaService} from "../../../logic-components/services/MatriculaService";

@Component({
  selector: 'app-matricula-form',
  templateUrl: './matricula-form.component.html',
  styleUrls: ['./matricula-form.component.css']
})
export class MatriculaFormComponent {
  matricula: any = {};

  constructor(private matriculaService: MatriculaService) {}

  onSubmit() {
    // Envía el nuevo matricula al servidor a través del servicio si es necesario
    this.matriculaService.addMatricula(this.matricula).subscribe(
      (response: any) => {
        console.log('Matricula agregado exitosamente:', response);
        // Puedes redirigir o realizar otras acciones después de agregar el matricula
      },
      (error: any) => {
        console.error('Error al agregar el matricula:', error);
        // Maneja el error apropiadamente
      }
    );
  }
}
