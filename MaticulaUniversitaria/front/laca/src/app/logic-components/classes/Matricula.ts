export class Matricula {
  id_matricula: number;
  id_estudiante: number;
  id_curso: number;
  ano_academico: number;
  calificacion: number;


  constructor(id_matricula: number = 0, id_estudiante: number = 0, id_curso: number = 0, ano_academico: number = 0, calificacion: number = 0) {
    this.id_matricula = id_matricula;
    this.id_estudiante = id_estudiante;
    this.id_curso = id_curso;
    this.ano_academico = ano_academico;
    this.calificacion = calificacion;
  }

}
