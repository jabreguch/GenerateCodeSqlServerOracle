using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace GeneraCodigo
{
	public enum E_TIPO_BD
	{
		ORACLE = 0,
		SQL_SERVER
	}
}
namespace GeneraCodigo
{
	public class BD_GENERICO
	{
		private E_TIPO_BD TIPO_BASE_DATOS_;

		private I_BD TIPO_BD_;
		public E_TIPO_BD TIPO_BASE_DATOS {
			get { return TIPO_BASE_DATOS_; }
			set {
				TIPO_BASE_DATOS_ = value;
				ACTUALIZA_TIPO_BD();
			}
		}

		public BD_GENERICO(E_TIPO_BD TIPO_BASE_DATOS__)
		{
			TIPO_BASE_DATOS_ = TIPO_BASE_DATOS__;
			ACTUALIZA_TIPO_BD();
		}

		private void ACTUALIZA_TIPO_BD()
		{
			if (TIPO_BASE_DATOS_ == E_TIPO_BD.ORACLE) {
				TIPO_BD_ = new ORACLE();
			} else if (TIPO_BASE_DATOS_ == E_TIPO_BD.SQL_SERVER) {
				TIPO_BD_ = new SQL_SERVER();
			}
		}

		public StringBuilder PKG(BE_TABLA TABLA)
		{
			return TIPO_BD_.PKG(TABLA);
		}

		public StringBuilder BL(BE_TABLA TABLA)
		{
			return TIPO_BD_.BL(TABLA);
		}

		public StringBuilder DA(BE_TABLA TABLA)
		{
			return TIPO_BD_.DA(TABLA);
		}

        public System.Text.StringBuilder HTMLINDEX(BE_TABLA TABLA)
        {
            return TIPO_BD_.HTMLINDEX(TABLA);
        }
        public System.Text.StringBuilder HTMLGET(BE_TABLA TABLA)
        {
            return TIPO_BD_.HTMLGET(TABLA);
        }
        public System.Text.StringBuilder HTMLGETS(BE_TABLA TABLA)
        {
            return TIPO_BD_.HTMLGETS(TABLA);
        }
        public System.Text.StringBuilder Controlador(BE_TABLA TABLA)
        {
            return TIPO_BD_.Controlador(TABLA);
        }

        public System.Text.StringBuilder ControladorApi(BE_TABLA TABLA)
        {
            return TIPO_BD_.ControladorApi(TABLA);
        }

        public List<BE_TABLA> TABLAS(BE_BASE_DATOS BD)
		{
			return TIPO_BD_.TABLAS(BD);
		}
		public List<BE_INDICE> INDICE(BE_TABLA TABLA)
		{
			return TIPO_BD_.INDICE(TABLA);
		}
		public List<BE_CAMPO> CAMPOS(BE_TABLA TABLA)
		{
			return TIPO_BD_.CAMPOS(TABLA);
		}
	}
}
namespace GeneraCodigo
{

	public interface I_BD
	{

		List<BE_TABLA> TABLAS(BE_BASE_DATOS BASE_DATOS);
        
		List<BE_CAMPO> CAMPOS(BE_TABLA TABLA);
		List<BE_INDICE> INDICE(BE_TABLA TABLA);
		StringBuilder BL(BE_TABLA TABLA);
		StringBuilder DA(BE_TABLA TABLA);
		StringBuilder PKG(BE_TABLA TABLA);
     
        StringBuilder Controlador(BE_TABLA TABLA);
        StringBuilder ControladorApi(BE_TABLA TABLA);
        

        StringBuilder HTMLINDEX(BE_TABLA TABLA);
        StringBuilder HTMLGET(BE_TABLA TABLA);
        StringBuilder HTMLGETS(BE_TABLA TABLA);


    }
}
