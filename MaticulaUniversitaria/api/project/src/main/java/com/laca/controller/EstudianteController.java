package com.laca.controller;
import com.laca.entity.Estudiante;
import com.laca.service.EstudianteService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/estudiantes")
@CrossOrigin(origins = "http://localhost:4200/")
public class EstudianteController {

    private final EstudianteService estudianteService;

    @Autowired
    public EstudianteController(EstudianteService estudianteService) {
        this.estudianteService = estudianteService;
    }

    @GetMapping
    public List<Estudiante> getAllEstudiantes() {
        List<Estudiante> estudiantes = estudianteService.getAllEstudiantes();
        return estudiantes;
    }

    @PostMapping
    public Estudiante saveTransporter(@RequestBody Estudiante estudiante) {
        return estudianteService.saveEstudiante(estudiante);
    }

    @PutMapping("/{estudianteId}")
    public ResponseEntity<?> updateEstudiante(
            @PathVariable int estudianteId,
            @RequestBody Estudiante updateEstudiante) {
        try {

            Estudiante updated = estudianteService.updateEstudiante(estudianteId, updateEstudiante);
            return ResponseEntity.ok(updated);

        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error actualizando estudiante: " + e.getMessage());
        }
    }

    @GetMapping("/{estudianteId}")
    public ResponseEntity<?> getEstudianteById(@PathVariable int estudianteId) {
        try {
            Estudiante estudiante = estudianteService.getEstudianteById(estudianteId);
            return ResponseEntity.ok(estudiante);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Estudiante no encontrado: " + e.getMessage());
        }
    }

    @DeleteMapping("/{estudianteId}")
    public ResponseEntity<?> deleteEstudiante(@PathVariable int estudianteId) {
        try {
            boolean isDeleted = estudianteService.deleteEstudiante(estudianteId);
            Estudiante estudiante= new Estudiante();
            estudiante.setCedula(estudianteId);
            if (isDeleted) {
                return ResponseEntity.ok(estudiante);
            }
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(estudianteId);

        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error al borrar al estudiante: " + e.getMessage());
        }

    }
}