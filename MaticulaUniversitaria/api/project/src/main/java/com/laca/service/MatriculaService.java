package com.laca.service;

import com.laca.entity.Matricula;
import jakarta.transaction.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.sql.DataSource;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;

@Service
public class MatriculaService {

    private final DataSource dataSource;

    public MatriculaService(DataSource dataSource) {
        this.dataSource = dataSource;
    }

    @Transactional
    public List<Matricula> getAllMatriculas() {
        List<Matricula> matriculas = new ArrayList<>();
        try (Connection connection = dataSource.getConnection()) {
            String query = "SELECT * FROM matricula";
            PreparedStatement statement = connection.prepareStatement(query);
            ResultSet resultSet = statement.executeQuery();
            while (resultSet.next()) {
                Matricula matricula = new Matricula();
                matricula.setId_matricula(resultSet.getInt("id_matricula"));
                matricula.setId_estudiante(resultSet.getInt("id_estudiante"));
                matricula.setId_curso(resultSet.getInt("id_curso"));
                matricula.setAno_academico(resultSet.getInt("ano_academico"));
                matricula.setCalificacion(resultSet.getInt("calificacion"));
                matriculas.add(matricula);
            }
        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al obtener las matrículas", e);
        }
        return matriculas;
    }

    @Transactional
    public Matricula saveMatricula(Matricula matricula) {
        if (!estudianteExistsByCedula(matricula.getId_estudiante())) {
            throw new RuntimeException("No se encontró ningún estudiante con la cédula proporcionada");
        }

        if (!cursoExists(matricula.getId_curso())) {
            throw new RuntimeException("El ID de curso proporcionado no existe");
        }

        try (Connection connection = dataSource.getConnection()) {
            String query = "INSERT INTO matricula (id_estudiante, id_curso, ano_academico, calificacion) VALUES (?, ?, ?, ?)";
            PreparedStatement statement = connection.prepareStatement(query, PreparedStatement.RETURN_GENERATED_KEYS);
            statement.setInt(1, matricula.getId_estudiante());
            statement.setInt(2, matricula.getId_curso());
            statement.setInt(3, matricula.getAno_academico());
            statement.setInt(4, matricula.getCalificacion());
            int affectedRows = statement.executeUpdate();
            if (affectedRows == 1) {
                ResultSet generatedKeys = statement.getGeneratedKeys();
                if (generatedKeys.next()) {
                    matricula.setId_matricula(generatedKeys.getInt(1));
                }
            }
        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al guardar la matrícula", e);
        }
        return matricula;
    }

    private boolean estudianteExistsByCedula(int cedula) {
        try (Connection connection = dataSource.getConnection()) {
            String query = "SELECT 1 FROM estudiante WHERE cedula = ?";
            PreparedStatement statement = connection.prepareStatement(query);
            statement.setInt(1, cedula);
            ResultSet resultSet = statement.executeQuery();
            return resultSet.next();
        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al verificar la existencia del estudiante por cédula", e);
        }
    }

    private boolean cursoExists(int idCurso) {
        try (Connection connection = dataSource.getConnection()) {
            String query = "SELECT 1 FROM curso WHERE id_curso = ?";
            PreparedStatement statement = connection.prepareStatement(query);
            statement.setInt(1, idCurso);
            ResultSet resultSet = statement.executeQuery();
            return resultSet.next();
        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al verificar la existencia del curso", e);
        }
    }

    @Transactional
    public int updateMatricula(int id, Matricula matricula) {

        if (!estudianteExistsByCedula(matricula.getId_estudiante())) {
            throw new RuntimeException("No se encontró ningún estudiante con la cédula proporcionada");
        }

        if (!cursoExists(matricula.getId_curso())) {
            throw new RuntimeException("El ID de curso proporcionado no existe");
        }

        try (Connection connection = dataSource.getConnection()) {
            String query = "UPDATE matricula SET id_estudiante = ?, id_curso = ?, ano_academico = ?, calificacion = ? WHERE id_matricula = ?";
            PreparedStatement statement = connection.prepareStatement(query);
            statement.setInt(1, matricula.getId_estudiante());
            statement.setInt(2, matricula.getId_curso());
            statement.setInt(3, matricula.getAno_academico());
            statement.setInt(4, matricula.getCalificacion());
            statement.setInt(5, id);
            return statement.executeUpdate();
        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al actualizar la matrícula", e);
        }
    }

    @Transactional
    public boolean deleteMatricula(int id) {
        try (Connection connection = dataSource.getConnection()) {
            String query = "DELETE FROM matricula WHERE id_matricula = ?";
            PreparedStatement statement = connection.prepareStatement(query);
            statement.setInt(1, id);
            int rowsAffected = statement.executeUpdate();
            return rowsAffected > 0;
        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al borrar la matrícula", e);
        }
    }

    @Transactional
    public Matricula getMatriculaById(int id) {
        try (Connection connection = dataSource.getConnection()) {
            String query = "SELECT * FROM matricula WHERE id_matricula = ?";
            PreparedStatement statement = connection.prepareStatement(query);
            statement.setInt(1, id);
            ResultSet resultSet = statement.executeQuery();
            if (resultSet.next()) {
                Matricula matricula = new Matricula();
                matricula.setId_matricula(resultSet.getInt("id_matricula"));
                matricula.setId_estudiante(resultSet.getInt("id_estudiante"));
                matricula.setId_curso(resultSet.getInt("id_curso"));
                matricula.setAno_academico(resultSet.getInt("ano_academico"));
                matricula.setCalificacion(resultSet.getInt("calificacion"));
                return matricula;
            } else {
                return null;
            }
        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al obtener la matrícula", e);
        }
    }
}
