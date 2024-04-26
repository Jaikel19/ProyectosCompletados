import {Component, OnInit} from '@angular/core';
import {CursoService} from "../../../logic-components/services/CursoService";
import {Curso} from "../../../logic-components/classes/Curso";
import {CursoEditModalComponent} from "../curso-edit-modal/curso-edit-modal.component";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {BehaviorSubject} from "rxjs";

@Component({
  selector: 'app-curso-list',
  templateUrl: './curso-list.component.html',
  styleUrls: ['./curso-list.component.css']
})
export class CursoListComponent implements OnInit {
  cursos: Curso[] = [];

  constructor(private cursoService: CursoService, private modalService: NgbModal) {}

  ngOnInit() {
    this.cursoService.cursos$.subscribe((cursos) => {
      this.cursos = cursos;
    });
  }

  openEditModal(cursoId: number) {
    this.cursoService.getCursoDetails(cursoId).subscribe((curso: any) => {
      const modalRef = this.modalService.open(CursoEditModalComponent, { size: 'lg' });
      modalRef.componentInstance.curso = curso;
    });
  }
  onDeleteCurso(curso: Curso) {
    this.cursoService.deleteCurso(curso.id_curso).subscribe((response:any) => {
      console.log("borrado", response);
    });
  }


}

