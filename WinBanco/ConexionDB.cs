using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace WinBanco
{
    internal class ConexionDB
    {
        private SqlConnection conexionDB;

        public ConexionDB()
        {
            conexionDB = new SqlConnection(@"Data Source=localhost;Initial Catalog=Banco_Eje_1_3;Integrated Security=True");
        }

        internal DataTable EjecutarSP(string sp)
        {
            DataTable tabla = new DataTable();
            conexionDB.Open();
            SqlCommand comando = new SqlCommand(sp, conexionDB);
            tabla.Load(comando.ExecuteReader());
            conexionDB.Close();
            return tabla;
        }

        internal int EjecutarInsertSPCliente(string sP, Cliente cl)
        {
            int filas;
            SqlCommand comando = new SqlCommand(sP, conexionDB);
            conexionDB.Open();
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@apellido", cl.Apellido);
            comando.Parameters.AddWithValue("@nombre", cl.Nombre);
            comando.Parameters.AddWithValue("@dni", cl.Dni);
            filas = comando.ExecuteNonQuery();
            conexionDB.Close();
            return filas;
        }

        internal int EjecutarInsertSPCuenta(string sP, Cuenta c)
        {
            int filas;
            SqlCommand comando = new SqlCommand(sP, conexionDB);
            conexionDB.Open();
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@cbunum", c.Cbu);
            comando.Parameters.AddWithValue("@saldo", c.Saldo);
            comando.Parameters.AddWithValue("@tipocue", c.Tipo.IdTipoCuenta);
            comando.Parameters.AddWithValue("@fecha", c.UltimoMovimiento);
            comando.Parameters.AddWithValue("@idcliente", c.Cliente.IdCliente);
            filas = comando.ExecuteNonQuery();
            conexionDB.Close();
            return filas;
        }
    }
}
