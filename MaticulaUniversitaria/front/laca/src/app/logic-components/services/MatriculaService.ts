import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {BehaviorSubject, Observable, Subject, tap} from 'rxjs';
import {Matricula} from "../classes/Matricula";

@Injectable({
  providedIn: 'root'
})
export class MatriculaService {
  private apiUrl = 'http://localhost:8080/matriculas';
  private matriculasSubject = new BehaviorSubject<Matricula[]>([]);
  matriculas$ = this.matriculasSubject.asObservable();

  constructor(private http: HttpClient) {
    this.getmatriculas();
  }

  getmatriculas(): void{
    this.http.get<Matricula[]>(this.apiUrl).subscribe(
      (initialmatriculas) => {
        this.matriculasSubject.next(initialmatriculas);
      },
      (error) => {
        console.error('Error al obtener la lista de matriculas:', error);
      }
    );
  }

  updateMatricula(updatedMatricula: Matricula): Observable<Matricula> {
    const url = `${this.apiUrl}/${updatedMatricula.id_matricula}`;
    return this.http.put<Matricula>(url, updatedMatricula).pipe(
      tap((response) => {
        const currentmatriculas = this.matriculasSubject.value;
        const index = currentmatriculas.findIndex(t => t.id_matricula === updatedMatricula.id_matricula);

        if (index !== -1) {
          currentmatriculas[index] = response;
          this.matriculasSubject.next([...currentmatriculas]);
        }
      })
    );
  }

  addMatricula(newMatricula: Matricula): Observable<Matricula> {
    return this.http.post<Matricula>(this.apiUrl, newMatricula).pipe(
      tap((response): void => {
        let currentmatriculas: Matricula[] = this.matriculasSubject.value;
        currentmatriculas.push(response);
        this.matriculasSubject.next([...currentmatriculas]);
      })
    );
  }

  getMatriculaDetails(id_matricula: number): Observable<Matricula> {
    const url = `${this.apiUrl}/${id_matricula}`;
    return this.http.get<Matricula>(url);
  }

  deleteMatricula(matriculaId: number): Observable<Matricula> {
    const deleteUrl = `${this.apiUrl}/${matriculaId}`;
    return this.http.delete<Matricula>(deleteUrl).pipe(
      tap((response): void => {
        let currentmatriculas: Matricula[] = this.matriculasSubject.value;
        currentmatriculas.splice(currentmatriculas.findIndex(item => item.id_matricula == matriculaId), 1);
        this.matriculasSubject.next([...currentmatriculas]);
      })
    );
  }

}
