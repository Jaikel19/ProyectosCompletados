package com.laca.service;

import com.laca.entity.Curso; // Cambio de Estudiante a Curso
import jakarta.transaction.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.sql.DataSource;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;

@Service
public class CursoService {

    private final DataSource dataSource;

    public CursoService(DataSource dataSource) {
        this.dataSource = dataSource;
    }

    @Transactional
    public List<Curso> getAllCursos() {
        List<Curso> cursos = new ArrayList<>();
        try (Connection connection = dataSource.getConnection()) {
            String query = "SELECT * FROM curso";
            PreparedStatement statement = connection.prepareStatement(query);
            ResultSet resultSet = statement.executeQuery();
            while (resultSet.next()) {
                Curso curso = new Curso();
                curso.setId_curso(resultSet.getInt("id_curso"));
                curso.setNombre_curso(resultSet.getString("nombre_curso"));
                curso.setDescripcion(resultSet.getString("descripcion"));
                curso.setCreditos(resultSet.getInt("creditos"));
                curso.setProfesor(resultSet.getString("profesor"));
                curso.setHora_inicio(resultSet.getString("hora_inicio"));
                curso.setHora_fin(resultSet.getString("hora_fin"));
                curso.setDias_semana(resultSet.getString("dias_semana"));
                curso.setModalidad(resultSet.getString("modalidad"));
                cursos.add(curso);
            }
        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al obtener los cursos", e);
        }
        return cursos;
    }

    @Transactional
    public Curso saveCurso(Curso curso) {
        try (Connection connection = dataSource.getConnection()) {
            String query = "INSERT INTO curso (nombre_curso, descripcion, creditos, profesor, hora_inicio, hora_fin, dias_semana, modalidad) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
            PreparedStatement statement = connection.prepareStatement(query, PreparedStatement.RETURN_GENERATED_KEYS);
            statement.setString(1, curso.getNombre_curso());
            statement.setString(2, curso.getDescripcion());
            statement.setInt(3, curso.getCreditos());
            statement.setString(4, curso.getProfesor());
            statement.setString(5, curso.getHora_inicio());
            statement.setString(6, curso.getHora_fin());
            statement.setString(7, curso.getDias_semana());
            statement.setString(8, curso.getModalidad());
            int affectedRows = statement.executeUpdate();
            if (affectedRows == 1) {
                ResultSet generatedKeys = statement.getGeneratedKeys();
                if (generatedKeys.next()) {
                    curso.setId_curso(generatedKeys.getInt(1));
                }
            }
        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al guardar el curso", e);
        }
        return curso;
    }

    @Transactional
    public int updateCurso(int id, Curso curso) {
        try (Connection connection = dataSource.getConnection()) {
            String query = "UPDATE curso SET nombre_curso = ?, descripcion = ?, creditos = ?, profesor = ?, hora_inicio = ?, hora_fin = ?, dias_semana = ?, modalidad = ? WHERE id_curso  = ?";
            PreparedStatement statement = connection.prepareStatement(query, PreparedStatement.RETURN_GENERATED_KEYS);
            statement.setString(1, curso.getNombre_curso());
            statement.setString(2, curso.getDescripcion());
            statement.setInt(3, curso.getCreditos());
            statement.setString(4, curso.getProfesor());
            statement.setString(5, curso.getHora_inicio());
            statement.setString(6, curso.getHora_fin());
            statement.setString(7, curso.getDias_semana());
            statement.setString(8, curso.getModalidad());
            statement.setInt(9, id);

            return statement.executeUpdate();

        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al actualizar el curso: " + e.getMessage());
        }
    }

    @Transactional
    public Boolean deleteCurso(int id) {
        try (Connection connection = dataSource.getConnection()) {
            String query = "DELETE FROM curso where id_curso = ?";
            PreparedStatement statement = connection.prepareStatement(query);
            statement.setInt(1, id);
            int rowsAffected = statement.executeUpdate();

            if (rowsAffected == 0) {
                return false;
            }

            return true;

        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al borrar el curso: " + e.getMessage(), e);
        }
    }

    @Transactional
    public Curso getCursoById(int id) {
        try (Connection connection = dataSource.getConnection()) {
            String query = "SELECT id_curso, nombre_curso, descripcion, creditos, profesor, hora_inicio, hora_fin, dias_semana, modalidad FROM curso WHERE id_curso = ?";
            PreparedStatement statement = connection.prepareStatement(query);
            statement.setInt(1, id);

            ResultSet resultSet = statement.executeQuery();

            if (resultSet.next()) {
                Curso curso = new Curso();
                curso.setId_curso(resultSet.getInt("id_curso"));
                curso.setNombre_curso(resultSet.getString("nombre_curso"));
                curso.setDescripcion(resultSet.getString("descripcion"));
                curso.setCreditos(resultSet.getInt("creditos"));
                curso.setProfesor(resultSet.getString("profesor"));
                curso.setHora_inicio(resultSet.getString("hora_inicio"));
                curso.setHora_fin(resultSet.getString("hora_fin"));
                curso.setDias_semana(resultSet.getString("dias_semana"));
                curso.setModalidad(resultSet.getString("modalidad"));
                return curso;
            } else {
                throw new RuntimeException("Ningún curso fue encontrado con el ID: " + id);
            }
        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al devolver el curso: " + e.getMessage(), e);
        }
    }
}
