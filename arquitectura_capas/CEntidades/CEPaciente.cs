namespace CEntidades
{
    public class CEPaciente
    {
        private string id_paciente = string.Empty;
        private string tip_doc = "CC";
        private string nom_paciente = string.Empty;
        private string dir_paciente = string.Empty;
        private string tel_paciente = string.Empty;
        private string cel_paciente = string.Empty;
        private bool activo = true;

        public string Id_paciente
        {
            get => id_paciente;
            set => id_paciente = value;
        }

        public string Tip_doc
        {
            get => tip_doc;
            set => tip_doc = value;
        }

        public string Nom_paciente
        {
            get => nom_paciente;
            set => nom_paciente = value;
        }

        public string Dir_paciente
        {
            get => dir_paciente;
            set => dir_paciente = value;
        }

        public string Tel_paciente
        {
            get => tel_paciente;
            set => tel_paciente = value;
        }

        public string Cel_paciente
        {
            get => cel_paciente;
            set => cel_paciente = value;
        }

        public bool Activo
        {
            get => activo;
            set => activo = value;
        }
    }
}
