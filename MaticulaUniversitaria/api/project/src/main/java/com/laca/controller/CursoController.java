package com.laca.controller;

import com.laca.entity.Curso; // Importa la entidad Curso
import com.laca.service.CursoService; // Importa el servicio CursoService
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/cursos")
@CrossOrigin(origins = "http://localhost:4200/")
public class CursoController {

    private final CursoService cursoService;

    @Autowired
    public CursoController(CursoService cursoService) {
        this.cursoService = cursoService;
    }

    @GetMapping
    public List<Curso> getAllCursos() {
        return cursoService.getAllCursos();
    }

    @PostMapping
    public Curso saveCurso(@RequestBody Curso curso) {
        return cursoService.saveCurso(curso);
    }

    @PutMapping("/{cursoId}")
    public ResponseEntity<?> updateCurso(
            @PathVariable int cursoId,
            @RequestBody Curso updateCurso) {
        try {
            int updated = cursoService.updateCurso(cursoId, updateCurso);

            if(updated != 0){
                return ResponseEntity.ok(updateCurso);
            }
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error actualizando curso ");
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error actualizando curso: " + e.getMessage());
        }
    }

    @GetMapping("/{cursoId}")
    public ResponseEntity<?> getCursoById(@PathVariable int cursoId) {
        try {
            Curso curso = cursoService.getCursoById(cursoId);
            return ResponseEntity.ok(curso);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Curso no encontrado: " + e.getMessage());
        }
    }

    @DeleteMapping("/{cursoId}")
    public ResponseEntity<?> deleteCurso(@PathVariable int cursoId) {
        try {
            boolean isDeleted = cursoService.deleteCurso(cursoId);
            Curso curso= new Curso();
            curso.setId_curso(cursoId);
            if (isDeleted) {
                return ResponseEntity.ok(curso);
            }
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(cursoId);

        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Error al borrar el curso: " + e.getMessage());
        }

    }
}
