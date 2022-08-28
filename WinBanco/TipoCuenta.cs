using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace WinBanco
{
    internal class TipoCuenta
    {
        private string nombreTipo;
        private int idTipoCuenta;
        
        public TipoCuenta()
        {
            nombreTipo=string.Empty;
            idTipoCuenta=0;
        }

        public int IdTipoCuenta
        { get { return idTipoCuenta; } set { idTipoCuenta = value; } }
        public string NombreTipo
        { get { return nombreTipo; } set { nombreTipo = value; } }

        public override string ToString()
        {
            return "Tipo de cuenta: " + NombreTipo;
        }
    }
}
