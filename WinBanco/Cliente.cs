using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace WinBanco
{
    internal class Cliente
    {
        private int idCliente;
        private string nombre;
        private string apellido;
        private int dni;

        public Cliente()
        {
               
            nombre =string.Empty;
            apellido = string.Empty;
            dni = 0;
            idCliente = 0;
        }

        public int IdCliente
        {
            get { return idCliente; }
            set { idCliente = value; }
        }
       
        public string Nombre 
            { get { return nombre; } set { nombre = value; } }

        public string Apellido 
            { get { return apellido; } set { apellido = value; } }

        public int Dni 
            { get { return dni; } set { dni = value; } }

        public override string ToString()
        {
            return "Nombre: " + Nombre + " Apellido: " + Apellido+" DNI: " + Dni;
        }
    }
}
