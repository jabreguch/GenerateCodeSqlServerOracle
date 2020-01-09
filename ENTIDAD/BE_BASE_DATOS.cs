using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace GeneraCodigo
{
	public class BE_BASE_DATOS
	{		
		public string AUTOR { get; set; }
		public BE_CAMPO_AUDITORIA_COLECCION CAMPO_AUDITORIA_COLECCION { get; set; }
        public string USUARIO { get; set; }
        public string CLAVE { get; set; }
        public string SERVIDOR { get; set; }
        public string NoBaseDatos { get; set; }
        public string NoEsquema { get; set; } // para Oracle

        public string CADENA_CONEXION_SQL_SERVER {
//'Return "integrated security=sspi;server=" & SERVIDOR_ & ";database=" & BASE_DATOS_
			get { return "Data Source="+ SERVIDOR + ";Initial Catalog=" + NoBaseDatos + ";User ID=sa;Password=" + CLAVE; }
		}

		public string CADENA_CONEXION_ORACLE {
            //"Data Source=" + SERVIDOR + ";User Id=" + USUARIO + ";Password=" + CLAVE;
            get { return "Data Source=" + SERVIDOR + ";User Id=" + USUARIO + ";Password=" + CLAVE; }
		}

	}
}
