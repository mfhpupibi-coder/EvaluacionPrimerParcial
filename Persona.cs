using System;

namespace ExamenParcial
{
    // Abstracción: Clase base que no se puede instanciar directamente
    public abstract class Persona
    {
        // Atributos privados (Encapsulamiento estricto)
        private int _id;
        private string _nombre;
        private string _email;

        // Propiedades públicas con validación defensiva (Buenas Prácticas)
        public int Id
        {
            get => _id;
            set => _id = value > 0 ? value : throw new ArgumentException("El ID debe ser un número positivo.");
        }

        public string Nombre
        {
            get => _nombre;
            set => _nombre = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("El nombre no puede estar vacío.");
        }

        public string Email
        {
            get => _email;
            set => _email = value.Contains("@") ? value : throw new ArgumentException("El correo electrónico no es válido.");
        }

        // Constructor para inicializar la clase base
        protected Persona(int id, string nombre, string email)
        {
            Id = id;
            Nombre = nombre;
            Email = email;
        }

        // Método abstracto: Polimorfismo puro obligado para los hijos
        public abstract double CalcularPagoMensual();
    }
}