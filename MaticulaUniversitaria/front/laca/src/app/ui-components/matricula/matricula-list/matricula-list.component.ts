import {Component, OnInit} from '@angular/core';
import {MatriculaService} from "../../../logic-components/services/MatriculaService";
import {Matricula} from "../../../logic-components/classes/Matricula";
import {MatriculaEditModalComponent} from "../matricula-edit-modal/matricula-edit-modal.component";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {BehaviorSubject} from "rxjs";

@Component({
  selector: 'app-matricula-list',
  templateUrl: './matricula-list.component.html',
  styleUrls: ['./matricula-list.component.css']
})
export class MatriculaListComponent implements OnInit {
  matriculas: Matricula[] = [];

  constructor(private matriculaService: MatriculaService, private modalService: NgbModal) {}

  ngOnInit() {
    this.matriculaService.matriculas$.subscribe((matriculas) => {
      this.matriculas = matriculas;
    });
  }

  openEditModal(matriculaId: number) {
    this.matriculaService.getMatriculaDetails(matriculaId).subscribe((matricula: any) => {
      const modalRef = this.modalService.open(MatriculaEditModalComponent, { size: 'lg' });
      modalRef.componentInstance.matricula = matricula;
    });
  }
  onDeleteMatricula(matricula: Matricula) {
    this.matriculaService.deleteMatricula(matricula.id_matricula).subscribe((response:any) => {
      console.log("borrado", response);
    });
  }


}

