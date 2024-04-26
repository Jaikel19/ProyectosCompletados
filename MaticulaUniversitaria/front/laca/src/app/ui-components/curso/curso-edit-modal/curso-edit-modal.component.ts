import {Component, Input, OnInit} from '@angular/core';
import {Curso} from "../../../logic-components/classes/Curso";
import {NgbActiveModal} from "@ng-bootstrap/ng-bootstrap";
import {CursoService} from "../../../logic-components/services/CursoService";

@Component({
  selector: 'app-curso-edit-modal',
  templateUrl: './curso-edit-modal.component.html',
  styleUrls: ['./curso-edit-modal.component.css']
})
export class CursoEditModalComponent implements OnInit{
  @Input() curso: Curso | undefined;
  toUpdateCurso: Curso = new Curso();// Recibe el objeto Curso del componente padre
  constructor(public activeModal: NgbActiveModal, private cursoService: CursoService) {
  }

  ngOnInit() {
    if (this.curso) {
      // @ts-ignore
      this.toUpdateCurso = new Curso(this.curso?.id_curso, this.curso?.nombre_curso, this.curso?.descripcion, this.curso?.creditos, this.curso?.profesor, this.curso?.hora_inicio, this.curso?.hora_fin, this.curso?.dias_semana, this.curso?.modalidad)
      console.log('curso', this.toUpdateCurso);
    }
  }

  onSubmit() {
    // Llama al servicio para actualizar el Curso
    // @ts-ignore
    this.cursoService.updateCurso(this.toUpdateCurso).subscribe(
      (updatedCurso) => {
        console.log('Curso actualizado:', updatedCurso);
        // Realiza acciones adicionales después de actualizar el curso si es necesario

        // Cierra el modal
        this.activeModal.close('Cambios guardados');
      },
      (error) => {
        console.error('Error al actualizar el curso:', error);
        // Maneja el error apropiadamente

        // Cierra el modal (opcional, dependiendo de cómo quieras manejar los errores)
        this.activeModal.dismiss();
      }
    );
  }
}




