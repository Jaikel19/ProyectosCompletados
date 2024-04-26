package com.laca.entity;

import java.sql.Time;

public class Curso {
    private int id_curso;
    private String nombre_curso;
    private String descripcion;
    private int creditos;
    private String profesor;
    private String hora_inicio;
    private String hora_fin;
    private String dias_semana;
    private String modalidad;

    public Curso() {
    }

    public Curso(int id_curso, String nombre_curso, String descripcion, int creditos, String profesor, String hora_inicio, String hora_fin, String dias_semana, String modalidad) {
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

    public int getId_curso() {
        return id_curso;
    }

    public void setId_curso(int id_curso) {
        this.id_curso = id_curso;
    }

    public String getNombre_curso() {
        return nombre_curso;
    }

    public void setNombre_curso(String nombre_curso) {
        this.nombre_curso = nombre_curso;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }

    public int getCreditos() {
        return creditos;
    }

    public void setCreditos(int creditos) {
        this.creditos = creditos;
    }

    public String getProfesor() {
        return profesor;
    }

    public void setProfesor(String profesor) {
        this.profesor = profesor;
    }

    public String getHora_inicio() {
        return hora_inicio;
    }

    public void setHora_inicio(String hora_inicio) {
        this.hora_inicio = hora_inicio;
    }

    public String getHora_fin() {
        return hora_fin;
    }

    public void setHora_fin(String hora_fin) {
        this.hora_fin = hora_fin;
    }

    public String getDias_semana() {
        return dias_semana;
    }

    public void setDias_semana(String dias_semana) {
        this.dias_semana = dias_semana;
    }

    public String getModalidad() {
        return modalidad;
    }

    public void setModalidad(String modalidad) {
        this.modalidad = modalidad;
    }
}
