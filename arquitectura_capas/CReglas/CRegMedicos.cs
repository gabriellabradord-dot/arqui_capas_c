using System.Data;
using CEntidades;
using CDatos;

namespace CReglas
{
    public class CRegMedicos
    {
        private readonly CDMedicos odatmedicos;

        public CRegMedicos(CDMedicos odatmedicos)
        {
            this.odatmedicos = odatmedicos;
        }

        public bool guardar_medico(CEMedico oentmedico)
        {
            Validar_medico(oentmedico);
            return odatmedicos.Guardar_medico(oentmedico);
        }

        public bool anular_medico(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Debe indicar el documento del médico.");

            return odatmedicos.Anular_medico(id);
        }

        public CEMedico? obtener_medico(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            DataSet ds = odatmedicos.Consultar_medico(id);
            return ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0
                ? null
                : Mapear_medico(ds.Tables[0].Rows[0]);
        }

        public List<CEMedico> listar_medicos()
        {
            DataSet ds = odatmedicos.Consultar_medicos();
            List<CEMedico> lista = new List<CEMedico>();

            if (ds.Tables.Count == 0)
                return lista;

            foreach (DataRow fila in ds.Tables[0].Rows)
                lista.Add(Mapear_medico(fila));

            return lista;
        }

        private static void Validar_medico(CEMedico medico)
        {
            if (string.IsNullOrWhiteSpace(medico.Id_medico))
                throw new ArgumentException("El documento del médico es obligatorio.");
            if (medico.Id_medico.Length > 10)
                throw new ArgumentException("El documento no puede superar 10 caracteres.");
            if (string.IsNullOrWhiteSpace(medico.Tip_doc))
                throw new ArgumentException("El tipo de documento es obligatorio.");
            if (string.IsNullOrWhiteSpace(medico.Nom_medico))
                throw new ArgumentException("El nombre del médico es obligatorio.");
            if (medico.Nom_medico.Length > 50)
                throw new ArgumentException("El nombre no puede superar 50 caracteres.");
        }

        private static CEMedico Mapear_medico(DataRow fila)
        {
            return new CEMedico
            {
                Id_medico = fila["id_medico"]?.ToString() ?? string.Empty,
                Tip_doc = fila["Tip_doc"]?.ToString() ?? string.Empty,
                Nom_medico = fila["nom_medico"]?.ToString() ?? string.Empty,
                Dir_medico = fila["dir_medico"]?.ToString() ?? string.Empty,
                Tel_medico = fila["tel_medico"]?.ToString() ?? string.Empty,
                Cel_medico = fila["cel_medico"]?.ToString() ?? string.Empty,
                Especialidad = fila["especialidad"]?.ToString() ?? string.Empty,
                Activo = Convert.ToBoolean(fila["activo"])
            };
        }
    }
}
