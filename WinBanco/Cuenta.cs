using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace WinBanco
{
    internal class Cuenta
    {
        private int cbu;
        private double saldo;
        private TipoCuenta tipo;
        private DateTime ultimoMovimiento;
        private Cliente cliente;

        public Cuenta()
        {
            cbu = 0;
            saldo = 0;
            ultimoMovimiento = DateTime.Now;
            tipo = new TipoCuenta();
            cliente = new Cliente();
        }

        public int Cbu
            { get { return cbu; } set { cbu = value; } }

        public double Saldo
            { get { return saldo; } set { saldo = value; } }

        public DateTime UltimoMovimiento
        { get { return ultimoMovimiento; } set { ultimoMovimiento = value; } }

        public TipoCuenta Tipo
            { get { return tipo; } set { tipo = value; } }

        public Cliente Cliente
            { get { return cliente; } set { cliente = value; } }

        public override string ToString()
        {
            return "Cuenta: " + Cbu + " Tipo Cuenta: " + Tipo +" Del cliente: "+Cliente+" Saldo: " + Saldo + " Ultimo Movimiento: " + UltimoMovimiento;
        }

    }
}
