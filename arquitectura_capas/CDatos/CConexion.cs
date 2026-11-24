using Microsoft.Data.SqlClient;

namespace CDatos
{
    public class CConexion
    {
        private readonly string cadenaConexion;

        public CConexion(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public SqlConnection Conectar()
        {
            try
            {
                SqlConnection oconectar = new SqlConnection(cadenaConexion);
                oconectar.Open();
                return oconectar;
            }
            catch (Exception err)
            {
                throw new Exception("Error al conectar a la base de datos: " + err.Message);
            }
        }
    }
}
