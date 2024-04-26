import { Component } from '@angular/core';
import {EstudianteService} from "../../../logic-components/services/EstudianteService";

@Component({
  selector: 'app-estudiante-form',
  templateUrl: './estudiante-form.component.html',
  styleUrls: ['./estudiante-form.component.css']
})
export class EstudianteFormComponent {
  estudiante: any = {};

  constructor(private estudianteService: EstudianteService) {}

  onSubmit() {
    // Envía el nuevo transporter al servidor a través del servicio si es necesario
    this.estudianteService.addEstudiante(this.estudiante).subscribe(
      (response: any) => {
        console.log('Estduiante agregado exitosamente:', response);
        // Puedes redirigir o realizar otras acciones después de agregar el transporter
      },
      (error: any) => {
        console.error('Error al agregar el estudiante:', error);
        // Maneja el error apropiadamente
      }
    );
  }
}
