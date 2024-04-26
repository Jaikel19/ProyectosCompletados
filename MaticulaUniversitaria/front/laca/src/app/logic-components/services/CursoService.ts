import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {BehaviorSubject, Observable, Subject, tap} from 'rxjs';
import {Curso} from "../classes/Curso";

@Injectable({
  providedIn: 'root'
})
export class CursoService {
  private apiUrl = 'http://localhost:8080/cursos';
  private cursosSubject = new BehaviorSubject<Curso[]>([]);
  cursos$ = this.cursosSubject.asObservable();

  constructor(private http: HttpClient) {
    this.getcursos();
  }

  getcursos(): void{
    this.http.get<Curso[]>(this.apiUrl).subscribe(
      (initialcursos) => {
        this.cursosSubject.next(initialcursos);
      },
      (error) => {
        console.error('Error al obtener la lista de cursos:', error);
      }
    );
  }

  updateCurso(updatedCurso: Curso): Observable<Curso> {
    const url = `${this.apiUrl}/${updatedCurso.id_curso}`;
    return this.http.put<Curso>(url, updatedCurso).pipe(
      tap((response) => {
        const currentcursos = this.cursosSubject.value;
        const index = currentcursos.findIndex(t => t.id_curso === updatedCurso.id_curso);

        if (index !== -1) {
          currentcursos[index] = response;
          this.cursosSubject.next([...currentcursos]);
        }
      })
    );
  }

  addCurso(newCurso: Curso): Observable<Curso> {
    return this.http.post<Curso>(this.apiUrl, newCurso).pipe(
      tap((response): void => {
        let currentcursos: Curso[] = this.cursosSubject.value;
        currentcursos.push(response);
        this.cursosSubject.next([...currentcursos]);
      })
    );
  }

  getCursoDetails(id_curso: number): Observable<Curso> {
    const url = `${this.apiUrl}/${id_curso}`;
    return this.http.get<Curso>(url);
  }

  deleteCurso(cursoId: number): Observable<Curso> {
    const deleteUrl = `${this.apiUrl}/${cursoId}`;
    return this.http.delete<Curso>(deleteUrl).pipe(
      tap((response): void => {
        let currentcursos: Curso[] = this.cursosSubject.value;
        currentcursos.splice(currentcursos.findIndex(item => item.id_curso == cursoId), 1);
        this.cursosSubject.next([...currentcursos]);
      })
    );
  }

}
