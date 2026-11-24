namespace CEntidades
{
    public class CEMedico
    {
        private string id_medico = string.Empty;
        private string tip_doc = "CC";
        private string nom_medico = string.Empty;
        private string dir_medico = string.Empty;
        private string tel_medico = string.Empty;
        private string cel_medico = string.Empty;
        private string especialidad = string.Empty;
        private bool activo = true;

        public string Id_medico
        {
            get => id_medico;
            set => id_medico = value;
        }

        public string Tip_doc
        {
            get => tip_doc;
            set => tip_doc = value;
        }

        public string Nom_medico
        {
            get => nom_medico;
            set => nom_medico = value;
        }

        public string Dir_medico
        {
            get => dir_medico;
            set => dir_medico = value;
        }

        public string Tel_medico
        {
            get => tel_medico;
            set => tel_medico = value;
        }

        public string Cel_medico
        {
            get => cel_medico;
            set => cel_medico = value;
        }

        public string Especialidad
        {
            get => especialidad;
            set => especialidad = value;
        }

        public bool Activo
        {
            get => activo;
            set => activo = value;
        }
    }
}
