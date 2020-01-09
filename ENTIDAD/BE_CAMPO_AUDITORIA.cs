using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace GeneraCodigo
{
	public enum E_FINALIDAD_CAMPO
	{
		USUARIO_CREACION = 1,
		USUARIO_MODIFICACION,
		USUARIO_ELIMINACION,
		FECHA_CREACION,
		FECHA_MODIFICACION,
		FECHA_ELIMINACION,
		CAMPO_ELIMINADO,
		CAMPO_ESTADO
	}
}
namespace GeneraCodigo
{

	public class BE_CAMPO_AUDITORIA : BE_CAMPO
	{
	
		public E_FINALIDAD_CAMPO FINALIDAD { get; set; }
        public BE_CAMPO_AUDITORIA(string NOMBRE__, E_TIPO_DATO_GENERICO TIPO_DATO_GENERICO__, E_FINALIDAD_CAMPO FINALIDAD__, int TAMANO_CAMPO__)
		{
			NOMBRE = NOMBRE__;
			TIPO_DATO_GENERICO = TIPO_DATO_GENERICO__;
			FINALIDAD = FINALIDAD__;
			TAMANO_CAMPO = TAMANO_CAMPO__;
		}
		public BE_CAMPO_AUDITORIA()
		{
		}
	}
}
