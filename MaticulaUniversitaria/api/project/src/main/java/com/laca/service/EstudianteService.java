package com.laca.service;
import com.laca.entity.Estudiante;
import jakarta.transaction.Transactional;
import org.springframework.stereotype.Service;

import javax.sql.DataSource;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;

@Service
public class EstudianteService {

    private final DataSource dataSource;

    public EstudianteService(DataSource dataSource) {
        this.dataSource = dataSource;
    }

    @Transactional
    public List<Estudiante> getAllEstudiantes() {
        List<Estudiante> estudiantes = new ArrayList<>();
        try (Connection connection = dataSource.getConnection()) {
            String query = "SELECT * FROM estudiante";
            PreparedStatement statement = connection.prepareStatement(query);
            ResultSet resultSet = statement.executeQuery();
            while (resultSet.next()) {
                Estudiante estudiante = new Estudiante();
                estudiante.setCedula(resultSet.getInt("cedula"));
                estudiante.setNombre(resultSet.getString("nombre"));
                estudiante.setApellido(resultSet.getString("apellido"));
                estudiante.setCorreo_electronico(resultSet.getString("correo_electronico"));
                estudiante.setContrasena(resultSet.getString("contrasena"));
                estudiantes.add(estudiante);
            }
        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al obtener los estudiantes", e);
        }
        return estudiantes;
    }

    @Transactional
    public Estudiante saveEstudiante(Estudiante estudiante) {
        try (Connection connection = dataSource.getConnection()) {
            String query = "INSERT INTO estudiante (cedula, nombre, apellido,correo_electronico,contrasena) VALUES (?, ?, ?, ?, ?)";
            PreparedStatement statement = connection.prepareStatement(query, PreparedStatement.RETURN_GENERATED_KEYS);
            statement.setInt(1, estudiante.getCedula());
            statement.setString(2, estudiante.getNombre());
            statement.setString(3, estudiante.getApellido());
            statement.setString(4, estudiante.getCorreo_electronico());
            statement.setString(5, estudiante.getContrasena());
            int affectedRows = statement.executeUpdate();
            if (affectedRows == 1) {
                ResultSet generatedKeys = statement.getGeneratedKeys();
                if (generatedKeys.next()) {
                    estudiante.setCedula(generatedKeys.getInt(1));
                }
            }
        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al guardar al estudiante", e);
        }
        return estudiante;
    }

    @Transactional
    public Estudiante updateEstudiante(int id, Estudiante estudiante) {
        try (Connection connection = dataSource.getConnection()) {
            String query = "UPDATE estudiante SET nombre = ?, apellido = ?, correo_electronico = ?, contrasena = ? WHERE cedula  = ?";
            PreparedStatement statement = connection.prepareStatement(query, PreparedStatement.RETURN_GENERATED_KEYS);
            statement.setString(1, estudiante.getNombre());
            statement.setString(2, estudiante.getApellido());
            statement.setString(3, estudiante.getCorreo_electronico());
            statement.setString(4, estudiante.getContrasena());
            statement.setInt(5, id);

            statement.executeUpdate();

            estudiante.setCedula(id);

            return estudiante;


        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al actualizar al estudiante :" + e.getMessage());
        }
    }

    @Transactional
    public Boolean deleteEstudiante(int id) {
        try (Connection connection = dataSource.getConnection()) {
            String query = "DELETE FROM estudiante where estudiante.cedula  = ?";
            PreparedStatement statement = connection.prepareStatement(query);
            statement.setInt(1, id);
            int rowsAffected = statement.executeUpdate();

            if (rowsAffected == 0) {
                return false;
            }

            return true;

        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al borrar al estudiante: " + e.getMessage(), e);
        }
    }

    @Transactional
    public Estudiante getEstudianteById(int id) {
        try (Connection connection = dataSource.getConnection()) {
            String query = "SELECT cedula, nombre, apellido, correo_electronico, contrasena FROM estudiante WHERE cedula = ?";
            PreparedStatement statement = connection.prepareStatement(query);
            statement.setLong(1, id);

            ResultSet resultSet = statement.executeQuery();

            if (resultSet.next()) {
                Estudiante estudiante = new Estudiante();
                estudiante.setCedula(resultSet.getInt("cedula"));
                estudiante.setNombre(resultSet.getString("nombre"));
                estudiante.setApellido(resultSet.getString("apellido"));
                estudiante.setCorreo_electronico(resultSet.getString("correo_electronico"));
                estudiante.setContrasena(resultSet.getString("contrasena"));
                return estudiante;
            } else {
                throw new RuntimeException("Ningun estudiante fue encontrando con el ID: " + id);
            }
        } catch (SQLException e) {
            throw new RuntimeException("Ocurrió un error al devolver al estudiante: " + e.getMessage(), e);
        }
    }
}
