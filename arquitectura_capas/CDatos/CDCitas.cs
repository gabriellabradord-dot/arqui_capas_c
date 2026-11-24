using System.Data;
using CEntidades;
using Microsoft.Data.SqlClient;

namespace CDatos
{
    public class CDCitas
    {
        private readonly CConexion oconexion;

        public CDCitas(CConexion oconexion)
        {
            this.oconexion = oconexion;
        }

        public bool Guardar_cita(CECitas ocitas)
        {
            try
            {
                using SqlConnection cn = oconexion.Conectar();
                using SqlCommand ocmd = new SqlCommand("Actualizar_cita", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                ocmd.Parameters.Add("@Cod_cita", SqlDbType.NVarChar, 10).Value = ocitas.Cod_cita;
                ocmd.Parameters.Add("@fecha", SqlDbType.Date).Value = ocitas.Fecha;
                ocmd.Parameters.Add("@hora", SqlDbType.Time).Value = ocitas.Hora;
                ocmd.Parameters.Add("@id_paciente", SqlDbType.NVarChar, 10).Value = ocitas.Id_paciente;
                ocmd.Parameters.Add("@id_medico", SqlDbType.NVarChar, 10).Value = ocitas.Id_medico;
                ocmd.Parameters.Add("@valor", SqlDbType.Int).Value = ocitas.Valor;
                ocmd.Parameters.Add("@diagnostico", SqlDbType.NVarChar, -1).Value =
                    (object?)ocitas.Diagnostico ?? DBNull.Value;
                ocmd.Parameters.Add("@Nom_acompanante", SqlDbType.NVarChar, 50).Value =
                    (object?)ocitas.Nom_acompanante ?? DBNull.Value;
                ocmd.Parameters.Add("@activo", SqlDbType.Bit).Value = ocitas.Activo;

                ocmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public bool Anular_cita(string codigo)
        {
            try
            {
                using SqlConnection cn = oconexion.Conectar();
                using SqlCommand ocmd = new SqlCommand("anula_cita", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                ocmd.Parameters.Add("@pcod_cita", SqlDbType.NVarChar, 10).Value = codigo;

                ocmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public DataSet Consultar_cita(string codigo)
        {
            try
            {
                using SqlConnection cn = oconexion.Conectar();
                using SqlCommand ocmd = new SqlCommand("Consultar_cita", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                ocmd.Parameters.Add("@Cod_cita", SqlDbType.NVarChar, 10).Value = codigo;

                using SqlDataAdapter adaptador = new SqlDataAdapter(ocmd);
                DataSet ds = new DataSet();
                adaptador.Fill(ds, "cita");
                return ds;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public DataSet Consultar_citas()
        {
            try
            {
                using SqlConnection cn = oconexion.Conectar();
                using SqlCommand ocmd = new SqlCommand("Consultar_citas", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using SqlDataAdapter adaptador = new SqlDataAdapter(ocmd);
                DataSet ds = new DataSet();
                adaptador.Fill(ds, "citas");
                return ds;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
