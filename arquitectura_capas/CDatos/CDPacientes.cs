using System.Data;
using CEntidades;
using Microsoft.Data.SqlClient;

namespace CDatos
{
    public class CDPacientes
    {
        private readonly CConexion oconexion;

        public CDPacientes(CConexion oconexion)
        {
            this.oconexion = oconexion;
        }

        public bool Guardar_paciente(CEPaciente opaciente)
        {
            try
            {
                using SqlConnection cn = oconexion.Conectar();
                using SqlCommand ocmd = new SqlCommand("Actualizar_paciente", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                ocmd.Parameters.Add("@id_paciente", SqlDbType.NVarChar, 10).Value = opaciente.Id_paciente;
                ocmd.Parameters.Add("@Tip_doc", SqlDbType.NVarChar, 20).Value = opaciente.Tip_doc;
                ocmd.Parameters.Add("@nom_paciente", SqlDbType.NVarChar, 50).Value = opaciente.Nom_paciente;
                ocmd.Parameters.Add("@dir_paciente", SqlDbType.NVarChar, 50).Value =
                    (object?)opaciente.Dir_paciente ?? DBNull.Value;
                ocmd.Parameters.Add("@tel_paciente", SqlDbType.NVarChar, 10).Value =
                    (object?)opaciente.Tel_paciente ?? DBNull.Value;
                ocmd.Parameters.Add("@cel_paciente", SqlDbType.NVarChar, 10).Value =
                    (object?)opaciente.Cel_paciente ?? DBNull.Value;
                ocmd.Parameters.Add("@activo", SqlDbType.Bit).Value = opaciente.Activo;

                ocmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public bool Anular_paciente(string id)
        {
            try
            {
                using SqlConnection cn = oconexion.Conectar();
                using SqlCommand ocmd = new SqlCommand("Anular_paciente", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                ocmd.Parameters.Add("@id_paciente", SqlDbType.NVarChar, 10).Value = id;

                ocmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public DataSet Consultar_paciente(string id)
        {
            try
            {
                using SqlConnection cn = oconexion.Conectar();
                using SqlCommand ocmd = new SqlCommand("Consultar_paciente", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                ocmd.Parameters.Add("@id_paciente", SqlDbType.NVarChar, 10).Value = id;

                using SqlDataAdapter adaptador = new SqlDataAdapter(ocmd);
                DataSet ds = new DataSet();
                adaptador.Fill(ds, "paciente");
                return ds;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public DataSet Consultar_pacientes()
        {
            try
            {
                using SqlConnection cn = oconexion.Conectar();
                using SqlCommand ocmd = new SqlCommand("Consultar_pacientes", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using SqlDataAdapter adaptador = new SqlDataAdapter(ocmd);
                DataSet ds = new DataSet();
                adaptador.Fill(ds, "pacientes");
                return ds;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
