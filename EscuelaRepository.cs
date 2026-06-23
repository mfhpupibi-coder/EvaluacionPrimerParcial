/// <summary>
/// Capa de persistencia encargada de gestionar los recursos e interacciones con el servidor de la Base de Datos.
/// Implementa operaciones CRUD seguras contra ataques de inyección SQL.
/// </summary>
public class EscuelaRepository
{
    /// <summary>
    /// Inicializa la infraestructura de datos ejecutando scripts DDL de forma directa y controlada.
    /// </summary>
    public void InicializarBaseDatos() { ... }

    /// <summary>
    /// Operación CRUD (Create) que inserta un registro de manera persistente usando Prepared Statements.
    /// </summary>
    /// <param name="profesor">Objeto de tipo ProfesorPorHoras cargado con los datos a persistir.</param>
    public void RegistrarPersona(ProfesorPorHoras profesor) { ... }
}
using System;
using System.Collections.Generic;
using Npgsql;

namespace ExamenParcial
{
    public class EscuelaRepository
    {
        // REMPLAZA ESTA CADENA POR LA TUYA DE SUPABASE
        private readonly string _connectionString = "YOUR_SUPABASE_CONNECTION_STRING";

        // Inicializa la tabla en Supabase si no existe
        public void InicializarBaseDatos()
        {
            string scriptCreacion = @"
                CREATE TABLE IF NOT EXISTS profesores (
                    id INT PRIMARY KEY,
                    nombre VARCHAR(100) NOT NULL,
                    email VARCHAR(100) NOT NULL UNIQUE,
                    horas_trabajadas INT NOT NULL DEFAULT 0,
                    tarifa_por_hora NUMERIC(10, 2) NOT NULL DEFAULT 0.00
                );";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                using (var command = new NpgsqlCommand(scriptCreacion, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("[BD] Verificación de tablas completada con éxito.");
                }
            }
        }

        // Operación CRUD: CREATE
        public void RegistrarPersona(ProfesorPorHoras profesor)
        {
            if (profesor == null)
                throw new ArgumentNullException(nameof(profesor), "El profesor no puede ser nulo.");

            string query = "INSERT INTO profesores (id, nombre, email, horas_trabajadas, tarifa_por_hora) VALUES (@id, @nombre, @email, @horas, @tarifa);";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", profesor.Id);
                    command.Parameters.AddWithValue("@nombre", profesor.Nombre);
                    command.Parameters.AddWithValue("@email", profesor.Email);
                    command.Parameters.AddWithValue("@horas", profesor.HorasTrabajadas);
                    command.Parameters.AddWithValue("@tarifa", profesor.TarifaPorHora);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Operación CRUD: READ
        public List<ProfesorPorHoras> ObtenerTodoElPersonal()
        {
            var lista = new List<ProfesorPorHoras>();
            string query = "SELECT id, nombre, email, horas_trabajadas, tarifa_por_hora FROM profesores;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                using (var command = new NpgsqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var prof = new ProfesorPorHoras(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetInt32(3),
                                Convert.ToDouble(reader.GetDecimal(4))
                            );
                            lista.Add(prof);
                        }
                    }
                }
            }
            return lista;
        }
    }
}