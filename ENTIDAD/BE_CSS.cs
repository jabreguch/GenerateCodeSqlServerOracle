using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace GeneraCodigo
{
	public enum E_TIPO_CONTROL
	{
		E_TEXT_CONTROL,
		E_AREA_CONTROL,
		E_SELECT_CONTROL,
		E_CHECKBOX_CONTROL,
		E_OPTION_CONTROL,
		E_LABEL_CONTROL,
		E_TABLE_CONTROL,
		E_CELDA_NOMBRE,
		E_CELDA_VALOR
	}
}
namespace GeneraCodigo
{
	public class BE_CSS
	{		
		public string CSS { get; set; }
        public E_TIPO_CONTROL TIPO_CONTROL { get; set; }

        public BE_CSS(string CSS__, E_TIPO_CONTROL TIPO_CONTROL__)
		{
			CSS= CSS__;
			TIPO_CONTROL = TIPO_CONTROL__;
		}


	}
}
