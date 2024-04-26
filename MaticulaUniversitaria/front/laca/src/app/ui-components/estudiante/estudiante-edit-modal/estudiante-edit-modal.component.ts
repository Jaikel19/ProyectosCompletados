import {Component, Input, OnInit} from '@angular/core';
import {Estudiante} from "../../../logic-components/classes/Estudiante";
import {NgbActiveModal} from "@ng-bootstrap/ng-bootstrap";
import {EstudianteService} from "../../../logic-components/services/EstudianteService";

@Component({
  selector: 'app-estudiante-edit-modal',
  templateUrl: './estudiante-edit-modal.component.html',
  styleUrls: ['./estudiante-edit-modal.component.css']
})
export class EstudianteEditModalComponent implements OnInit{
  @Input() estudiante: Estudiante | undefined;
  toUpdateEstudiante: Estudiante = new Estudiante();// Recibe el objeto Transporter del componente padre
  constructor(public activeModal: NgbActiveModal, private estudianteService: EstudianteService) {
  }

  ngOnInit() {
    if (this.estudiante) {
      // @ts-ignore
      this.toUpdateEstudiante = new Estudiante(this.estudiante?.cedula, this.estudiante?.nombre, this.estudiante?.apellido, this.estudiante?.correo_electronico, this.estudiante?.contrasena)
      console.log('estudiante', this.toUpdateEstudiante);
    }
  }

  onSubmit() {
    // Llama al servicio para actualizar el Transporter
    // @ts-ignore
    this.estudianteService.updateEstudiante(this.toUpdateEstudiante).subscribe(
      (updatedEstudiante) => {
        console.log('Estudiante actualizado:', updatedEstudiante);
        // Realiza acciones adicionales después de actualizar el transporter si es necesario

        // Cierra el modal
        this.activeModal.close('Cambios guardados');
      },
      (error) => {
        console.error('Error al actualizar el Estudiante:', error);
        // Maneja el error apropiadamente

        // Cierra el modal (opcional, dependiendo de cómo quieras manejar los errores)
        this.activeModal.dismiss();
      }
    );
  }
}




