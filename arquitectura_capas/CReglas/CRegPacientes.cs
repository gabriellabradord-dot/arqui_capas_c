using System.Data;
using CEntidades;
using CDatos;

namespace CReglas
{
    public class CRegPacientes
    {
        private readonly CDPacientes odatpacientes;

        public CRegPacientes(CDPacientes odatpacientes)
        {
            this.odatpacientes = odatpacientes;
        }

        public bool guardar_paciente(CEPaciente oentpaciente)
        {
            Validar_paciente(oentpaciente);
            return odatpacientes.Guardar_paciente(oentpaciente);
        }

        public bool anular_paciente(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Debe indicar el documento del paciente.");

            return odatpacientes.Anular_paciente(id);
        }

        public CEPaciente? obtener_paciente(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            DataSet ds = odatpacientes.Consultar_paciente(id);
            return ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0
                ? null
                : Mapear_paciente(ds.Tables[0].Rows[0]);
        }

        public List<CEPaciente> listar_pacientes()
        {
            DataSet ds = odatpacientes.Consultar_pacientes();
            List<CEPaciente> lista = new List<CEPaciente>();

            if (ds.Tables.Count == 0)
                return lista;

            foreach (DataRow fila in ds.Tables[0].Rows)
                lista.Add(Mapear_paciente(fila));

            return lista;
        }

        private static void Validar_paciente(CEPaciente paciente)
        {
            if (string.IsNullOrWhiteSpace(paciente.Id_paciente))
                throw new ArgumentException("El documento del paciente es obligatorio.");
            if (paciente.Id_paciente.Length > 10)
                throw new ArgumentException("El documento no puede superar 10 caracteres.");
            if (string.IsNullOrWhiteSpace(paciente.Tip_doc))
                throw new ArgumentException("El tipo de documento es obligatorio.");
            if (string.IsNullOrWhiteSpace(paciente.Nom_paciente))
                throw new ArgumentException("El nombre del paciente es obligatorio.");
            if (paciente.Nom_paciente.Length > 50)
                throw new ArgumentException("El nombre no puede superar 50 caracteres.");
        }

        private static CEPaciente Mapear_paciente(DataRow fila)
        {
            return new CEPaciente
            {
                Id_paciente = fila["id_paciente"]?.ToString() ?? string.Empty,
                Tip_doc = fila["Tip_doc"]?.ToString() ?? string.Empty,
                Nom_paciente = fila["nom_paciente"]?.ToString() ?? string.Empty,
                Dir_paciente = fila["dir_paciente"]?.ToString() ?? string.Empty,
                Tel_paciente = fila["tel_paciente"]?.ToString() ?? string.Empty,
                Cel_paciente = fila["cel_paciente"]?.ToString() ?? string.Empty,
                Activo = Convert.ToBoolean(fila["activo"])
            };
        }
    }
}
