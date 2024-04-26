package com.laca.entity;

public class Matricula {
    private int id_matricula;
    private int id_estudiante;
    private int id_curso;
    private int ano_academico ;
    private int calificacion;

    public Matricula() {
    }

    public Matricula(int id_matricula, int id_estudiante, int id_curso, int ano_academico, int calificacion) {
        this.id_matricula = id_matricula;
        this.id_estudiante = id_estudiante;
        this.id_curso = id_curso;
        this.ano_academico = ano_academico;
        this.calificacion = calificacion;
    }

    public int getId_matricula() {
        return id_matricula;
    }

    public void setId_matricula(int id_matricula) {
        this.id_matricula = id_matricula;
    }

    public int getId_estudiante() {
        return id_estudiante;
    }

    public void setId_estudiante(int id_estudiante) {
        this.id_estudiante = id_estudiante;
    }

    public int getId_curso() {
        return id_curso;
    }

    public void setId_curso(int id_curso) {
        this.id_curso = id_curso;
    }

    public int getAno_academico() {
        return ano_academico;
    }

    public void setAno_academico(int ano_academico) {
        this.ano_academico = ano_academico;
    }

    public int getCalificacion() {
        return calificacion;
    }

    public void setCalificacion(int calificacion) {
        this.calificacion = calificacion;
    }
}
