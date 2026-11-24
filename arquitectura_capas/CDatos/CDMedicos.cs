using System.Data;
using CEntidades;
using Microsoft.Data.SqlClient;

namespace CDatos
{
    public class CDMedicos
    {
        private readonly CConexion oconexion;

        public CDMedicos(CConexion oconexion)
        {
            this.oconexion = oconexion;
        }

        public bool Guardar_medico(CEMedico omedico)
        {
            try
            {
                using SqlConnection cn = oconexion.Conectar();
                using SqlCommand ocmd = new SqlCommand("Actualizar_medico", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                ocmd.Parameters.Add("@id_medico", SqlDbType.NVarChar, 10).Value = omedico.Id_medico;
                ocmd.Parameters.Add("@Tip_doc", SqlDbType.NVarChar, 20).Value = omedico.Tip_doc;
                ocmd.Parameters.Add("@nom_medico", SqlDbType.NVarChar, 50).Value = omedico.Nom_medico;
                ocmd.Parameters.Add("@dir_medico", SqlDbType.NVarChar, 50).Value =
                    (object?)omedico.Dir_medico ?? DBNull.Value;
                ocmd.Parameters.Add("@tel_medico", SqlDbType.NVarChar, 10).Value =
                    (object?)omedico.Tel_medico ?? DBNull.Value;
                ocmd.Parameters.Add("@cel_medico", SqlDbType.NVarChar, 10).Value =
                    (object?)omedico.Cel_medico ?? DBNull.Value;
                ocmd.Parameters.Add("@especialidad", SqlDbType.NVarChar, 50).Value =
                    (object?)omedico.Especialidad ?? DBNull.Value;
                ocmd.Parameters.Add("@activo", SqlDbType.Bit).Value = omedico.Activo;

                ocmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public bool Anular_medico(string id)
        {
            try
            {
                using SqlConnection cn = oconexion.Conectar();
                using SqlCommand ocmd = new SqlCommand("Anular_medico", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                ocmd.Parameters.Add("@id_medico", SqlDbType.NVarChar, 10).Value = id;

                ocmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public DataSet Consultar_medico(string id)
        {
            try
            {
                using SqlConnection cn = oconexion.Conectar();
                using SqlCommand ocmd = new SqlCommand("Consultar_medico", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                ocmd.Parameters.Add("@id_medico", SqlDbType.NVarChar, 10).Value = id;

                using SqlDataAdapter adaptador = new SqlDataAdapter(ocmd);
                DataSet ds = new DataSet();
                adaptador.Fill(ds, "medico");
                return ds;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public DataSet Consultar_medicos()
        {
            try
            {
                using SqlConnection cn = oconexion.Conectar();
                using SqlCommand ocmd = new SqlCommand("Consultar_medicos", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using SqlDataAdapter adaptador = new SqlDataAdapter(ocmd);
                DataSet ds = new DataSet();
                adaptador.Fill(ds, "medicos");
                return ds;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
