using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
namespace GeneraCodigo
{
	public class BE_PROCEDIMIENTO
	{
		public string ESQUEMA { get; set; }
        public List<BE_PARAMETRO> PARAMETRO_COL { get; set; }
        public string NOMBRE { get; set; }

        public BE_PROCEDIMIENTO()
		{
			PARAMETRO_COL = new List<BE_PARAMETRO>();
		}
	}
}
