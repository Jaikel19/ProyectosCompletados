package com.laca.controller;

import com.laca.entity.Matricula;
import com.laca.service.MatriculaService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/matriculas")
@CrossOrigin(origins = "http://localhost:4200/")
public class MatriculaController {

    private final MatriculaService matriculaService;

    @Autowired
    public MatriculaController(MatriculaService matriculaService) {
        this.matriculaService = matriculaService;
    }

    @GetMapping
    public List<Matricula> getAllMatriculas() {
        List<Matricula> matriculas = matriculaService.getAllMatriculas();
        return matriculas;
    }

    @PostMapping
    public ResponseEntity<Matricula> saveMatricula(@RequestBody Matricula matricula) {
        Matricula savedMatricula = matriculaService.saveMatricula(matricula);
        return ResponseEntity.status(HttpStatus.CREATED).body(savedMatricula);
    }

    @PutMapping("/{matriculaId}")
    public ResponseEntity<Matricula> updateMatricula(
            @PathVariable int matriculaId,
            @RequestBody Matricula updateMatricula) {
        try {
            int updated = matriculaService.updateMatricula(matriculaId, updateMatricula);

            if (updated != 0) {
                return ResponseEntity.ok(updateMatricula);
            }
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(null);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(null);
        }
    }

    @GetMapping("/{matriculaId}")
    public ResponseEntity<Matricula> getMatriculaById(@PathVariable int matriculaId) {
        Matricula matricula = matriculaService.getMatriculaById(matriculaId);
        if (matricula != null) {
            return ResponseEntity.ok(matricula);
        } else {
            return ResponseEntity.notFound().build();
        }
    }

    @DeleteMapping("/{matriculaId}")
    public ResponseEntity<?> deleteMatricula(@PathVariable int matriculaId) {
        boolean isDeleted = matriculaService.deleteMatricula(matriculaId);
        if (isDeleted) {
            return ResponseEntity.noContent().build();
        } else {
            return ResponseEntity.notFound().build();
        }
    }
}
