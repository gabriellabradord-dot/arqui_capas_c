using System.Data;
using CEntidades;
using CDatos;

namespace CReglas
{
    public class CRegCitas
    {
        private readonly CDCitas odatcitas;

        public CRegCitas(CDCitas odatcitas)
        {
            this.odatcitas = odatcitas;
        }

        public bool guardar_cita(CECitas oentcita)
        {
            Validar_cita(oentcita);
            return odatcitas.Guardar_cita(oentcita);
        }

        public bool anular_cita(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("Debe indicar el código de la cita.");

            CECitas? cita = obtener_cita(codigo);
            if (cita is null)
                throw new ArgumentException("La cita no existe.");
            if (!cita.Activo)
                throw new ArgumentException("La cita ya se encuentra anulada.");

            return odatcitas.Anular_cita(codigo);
        }

        public CECitas? obtener_cita(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return null;

            DataSet ds = odatcitas.Consultar_cita(codigo);
            return ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0
                ? null
                : Mapear_cita(ds.Tables[0].Rows[0]);
        }

        public List<CECitas> listar_citas()
        {
            DataSet ds = odatcitas.Consultar_citas();
            List<CECitas> lista = new List<CECitas>();

            if (ds.Tables.Count == 0)
                return lista;

            foreach (DataRow fila in ds.Tables[0].Rows)
                lista.Add(Mapear_cita(fila));

            return lista;
        }

        private static void Validar_cita(CECitas cita)
        {
            if (string.IsNullOrWhiteSpace(cita.Cod_cita))
                throw new ArgumentException("El código de la cita es obligatorio.");
            if (cita.Cod_cita.Length > 10)
                throw new ArgumentException("El código de la cita no puede superar 10 caracteres.");
            if (cita.Fecha == default)
                throw new ArgumentException("La fecha de la cita es obligatoria.");
            if (string.IsNullOrWhiteSpace(cita.Id_paciente))
                throw new ArgumentException("Debe seleccionar un paciente.");
            if (string.IsNullOrWhiteSpace(cita.Id_medico))
                throw new ArgumentException("Debe seleccionar un médico.");
            if (cita.Valor < 0)
                throw new ArgumentException("El valor de la cita no puede ser negativo.");
            if (!string.IsNullOrEmpty(cita.Nom_acompanante) && cita.Nom_acompanante.Length > 50)
                throw new ArgumentException("El nombre del acompañante no puede superar 50 caracteres.");
        }

        private static CECitas Mapear_cita(DataRow fila)
        {
            return new CECitas
            {
                Cod_cita = fila["Cod_cita"]?.ToString() ?? string.Empty,
                Fecha = Convert.ToDateTime(fila["fecha"]),
                Hora = fila["hora"] is TimeSpan hora ? hora : TimeSpan.Zero,
                Id_paciente = fila["id_paciente"]?.ToString() ?? string.Empty,
                Nom_paciente = fila["nom_paciente"]?.ToString() ?? string.Empty,
                Id_medico = fila["id_medico"]?.ToString() ?? string.Empty,
                Nom_medico = fila["nom_medico"]?.ToString() ?? string.Empty,
                Valor = Convert.ToInt32(fila["valor"]),
                Diagnostico = fila["diagnostico"]?.ToString() ?? string.Empty,
                Nom_acompanante = fila["Nom_acompanante"]?.ToString() ?? string.Empty,
                Activo = Convert.ToBoolean(fila["activo"])
            };
        }
    }
}
