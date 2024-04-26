import { Time } from "@angular/common";

export class Curso {
  id_curso: number;
  nombre_curso: string;
  descripcion: string;
  creditos: number;
  profesor: string;
  hora_inicio: string;
  hora_fin: string;
  dias_semana: string;
  modalidad: string;

  constructor(
    id_curso: number = 0,
    nombre_curso: string = '',
    descripcion: string = '',
    creditos: number = 0,
    profesor: string = '',
    hora_inicio: string = '',
    hora_fin: string = '',
    dias_semana: string = '',
    modalidad: string = ''
  ) {
    this.id_curso = id_curso;
    this.nombre_curso = nombre_curso;
    this.descripcion = descripcion;
    this.creditos = creditos;
    this.profesor = profesor;
    this.hora_inicio = hora_inicio;
    this.hora_fin = hora_fin;
    this.dias_semana = dias_semana;
    this.modalidad = modalidad;
  }
}