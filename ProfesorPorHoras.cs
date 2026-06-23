using System;

namespace ExamenParcial
{
    // Herencia: ProfesorPorHoras hereda de Persona
    public class ProfesorPorHoras : Persona
    {
        private int _horasTrabajadas;
        private double _tarifaPorHora;

        public int HorasTrabajadas
        {
            get => _horasTrabajadas;
            set => _horasTrabajadas = value >= 0 ? value : throw new ArgumentException("Las horas no pueden ser negativas.");
        }

        public double TarifaPorHora
        {
            get => _tarifaPorHora;
            set => _tarifaPorHora = value >= 0 ? value : throw new ArgumentException("La tarifa no puede ser negativa.");
        }

        // Constructor que invoca la lógica de la clase padre usando 'base'
        public ProfesorPorHoras(int id, string nombre, string email, int horasTrabajadas, double tarifaPorHora)
            : base(id, nombre, email)
        {
            HorasTrabajadas = horasTrabajadas;
            TarifaPorHora = tarifaPorHora;
        }

        // Polimorfismo: Redefinición del método abstracto de la clase padre
        public override double CalcularPagoMensual()
        {
            return _horasTrabajadas * _tarifaPorHora;
        }
    }
}