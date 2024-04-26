import {Component, OnInit} from '@angular/core';
import {EstudianteService} from "../../../logic-components/services/EstudianteService";
import {Estudiante} from "../../../logic-components/classes/Estudiante";
import {EstudianteEditModalComponent} from "../estudiante-edit-modal/estudiante-edit-modal.component";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {BehaviorSubject} from "rxjs";

@Component({
  selector: 'app-estudiante-list',
  templateUrl: './estudiante-list.component.html',
  styleUrls: ['./estudiante-list.component.css']
})
export class EstudianteListComponent implements OnInit {
  estudiantes: Estudiante[] = [];

  constructor(private estudianteService: EstudianteService, private modalService: NgbModal) {}

  ngOnInit() {
    this.estudianteService.estudiantes$.subscribe((estudiantes) => {
      this.estudiantes = estudiantes;
    });
  }

  openEditModal(estudianteCedula: number) {
    this.estudianteService.getEstudianteDetails(estudianteCedula).subscribe((estudiante: any) => {
      const modalRef = this.modalService.open(EstudianteEditModalComponent, { size: 'lg' });
      modalRef.componentInstance.estudiante = estudiante;
    });
  }
  onDeleteEstudiante(estudiante: Estudiante) {
    this.estudianteService.deleteEstudiante(estudiante.cedula).subscribe((response:any) => {
      console.log("borrado", response);
    });
  }


}

