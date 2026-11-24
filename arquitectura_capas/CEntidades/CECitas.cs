using System;

namespace CEntidades
{
    public class CECitas
    {
        private string cod_cita = string.Empty;
        private DateTime fecha = DateTime.Today;
        private TimeSpan hora = new TimeSpan(9, 0, 0);
        private string id_paciente = string.Empty;
        private string id_medico = string.Empty;
        private int valor;
        private string diagnostico = string.Empty;
        private string nom_acompanante = string.Empty;
        private bool activo = true;

        public string Cod_cita
        {
            get => cod_cita;
            set => cod_cita = value;
        }

        public DateTime Fecha
        {
            get => fecha;
            set => fecha = value;
        }

        public TimeSpan Hora
        {
            get => hora;
            set => hora = value;
        }

        public string Id_paciente
        {
            get => id_paciente;
            set => id_paciente = value;
        }

        public string Id_medico
        {
            get => id_medico;
            set => id_medico = value;
        }

        public int Valor
        {
            get => valor;
            set => valor = value;
        }

        public string Diagnostico
        {
            get => diagnostico;
            set => diagnostico = value;
        }

        public string Nom_acompanante
        {
            get => nom_acompanante;
            set => nom_acompanante = value;
        }

        public bool Activo
        {
            get => activo;
            set => activo = value;
        }

        public string Nom_paciente { get; set; } = string.Empty;
        public string Nom_medico { get; set; } = string.Empty;
    }
}
