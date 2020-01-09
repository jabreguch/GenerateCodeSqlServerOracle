using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace GeneraCodigo
{
	public enum E_TIPO_PARAMETRO
	{
		ENTRADA = 1,
		SALIDA,
		ENTRADA_SALIDA
	}
}
namespace GeneraCodigo
{
	public class BE_PARAMETRO
	{		
		public int NU_POSICION { get; set; }
        public E_TIPO_PARAMETRO TIPO_PARAMETRO { get; set; }
        public string NOMBRE {
			get {
				if (NOMBRE.EsNulo()) {
					return "";
				} else {
					return NOMBRE.Replace("@", "");
				}
			}
			set { NOMBRE= value; }
		}
		
		public string TIPO_DATO { get; set; }
        public int NU_TAMANO_CARACTER { get; set; }
        public int NU_PRECISION_NUMERICA { get; set; }
        public int NU_DECIMALES { get; set; }
    }
}
