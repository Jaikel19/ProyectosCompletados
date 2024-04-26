import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {BehaviorSubject, Observable, Subject, tap} from 'rxjs';
import {Estudiante} from "../classes/Estudiante";

@Injectable({
  providedIn: 'root'
})
export class EstudianteService {
  private apiUrl = 'http://localhost:8080/estudiantes';
  private estudiantesSubject = new BehaviorSubject<Estudiante[]>([]);
  estudiantes$ = this.estudiantesSubject.asObservable();

  constructor(private http: HttpClient) {
    this.getEstudiantes();
  }

  getEstudiantes(): void{
    this.http.get<Estudiante[]>(this.apiUrl).subscribe(
      (initialEstudiantes) => {
        this.estudiantesSubject.next(initialEstudiantes);
      },
      (error) => {
        console.error('Error al obtener la lista de Estudiantes:', error);
      }
    );
  }

  updateEstudiante(updatedEstudiante: Estudiante): Observable<Estudiante> {
    const url = `${this.apiUrl}/${updatedEstudiante.cedula}`;
    return this.http.put<Estudiante>(url, updatedEstudiante).pipe(
      tap((response) => {
        const currentEstudiantes = this.estudiantesSubject.value;
        const index = currentEstudiantes.findIndex(t => t.cedula === updatedEstudiante.cedula);

        if (index !== -1) {
          currentEstudiantes[index] = response;
          this.estudiantesSubject.next([...currentEstudiantes]);
        }
      })
    );
  }

  addEstudiante(newEstudiante: Estudiante): Observable<Estudiante> {
    return this.http.post<Estudiante>(this.apiUrl, newEstudiante).pipe(
      tap((response): void => {
        let currentEstudiantes: Estudiante[] = this.estudiantesSubject.value;
        currentEstudiantes.push(response);
        this.estudiantesSubject.next([...currentEstudiantes]);
      })
    );
  }

  getEstudianteDetails(cedula: number): Observable<Estudiante> {
    const url = `${this.apiUrl}/${cedula}`;
    return this.http.get<Estudiante>(url);
  }

  deleteEstudiante(estudianteCedula: number): Observable<Estudiante> {
    const deleteUrl = `${this.apiUrl}/${estudianteCedula}`;
    return this.http.delete<Estudiante>(deleteUrl).pipe(
      tap((response): void => {
        let currentEstudiantes: Estudiante[] = this.estudiantesSubject.value;
        currentEstudiantes.splice(currentEstudiantes.findIndex(item => item.cedula == estudianteCedula), 1);
        this.estudiantesSubject.next([...currentEstudiantes]);
      })
    );
  }

}
