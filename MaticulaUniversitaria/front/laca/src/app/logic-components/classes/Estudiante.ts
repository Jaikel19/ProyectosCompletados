export class Estudiante {
  cedula: number;
  nombre: string;
  apellido: string;
  correo_electronico: string;
  contrasena: string;


  constructor(cedula: number = 0, nombre: string = '', apellido: string = '', correo_electronico: string = '', contrasena: string = '') {
    this.cedula = cedula;
    this.nombre = nombre;
    this.apellido = apellido;
    this.correo_electronico = correo_electronico;
    this.contrasena = contrasena;
  }

}
