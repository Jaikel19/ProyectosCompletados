import {Component, Input, OnInit} from '@angular/core';
import {Matricula} from "../../../logic-components/classes/Matricula";
import {NgbActiveModal} from "@ng-bootstrap/ng-bootstrap";
import {MatriculaService} from "../../../logic-components/services/MatriculaService";

@Component({
  selector: 'app-matricula-edit-modal',
  templateUrl: './matricula-edit-modal.component.html',
  styleUrls: ['./matricula-edit-modal.component.css']
})
export class MatriculaEditModalComponent implements OnInit{
  @Input() matricula: Matricula | undefined;
  toUpdateMatricula: Matricula = new Matricula();// Recibe el objeto Matricula del componente padre
  constructor(public activeModal: NgbActiveModal, private matriculaService: MatriculaService) {
  }

  ngOnInit() {
    if (this.matricula) {
      // @ts-ignore
      this.toUpdateMatricula = new Matricula(this.matricula?.id_matricula, this.matricula?.id_estudiante, this.matricula?.id_curso, this.matricula?.ano_academico, this.matricula?.calificacion)
      console.log('matricula', this.toUpdateMatricula);
    }
  }

  onSubmit() {
    // Llama al servicio para actualizar el Matricula
    // @ts-ignore
    this.matriculaService.updateMatricula(this.toUpdateMatricula).subscribe(
      (updatedMatricula) => {
        console.log('Matricula actualizado:', updatedMatricula);
        // Realiza acciones adicionales después de actualizar el matricula si es necesario

        // Cierra el modal
        this.activeModal.close('Cambios guardados');
      },
      (error) => {
        console.error('Error al actualizar el matricula:', error);
        // Maneja el error apropiadamente

        // Cierra el modal (opcional, dependiendo de cómo quieras manejar los errores)
        this.activeModal.dismiss();
      }
    );
  }
}




