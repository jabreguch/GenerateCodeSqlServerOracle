using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace GeneraCodigo
{
	public class BE_CAMPO_AUDITORIA_COLECCION : CollectionBase
	{
		public void Add(BE_CAMPO_AUDITORIA CAMPO_AUDITORIA_)
		{
			List.Add(CAMPO_AUDITORIA_);
		}
		public void Remove(int index)
		{
			if (index > Count - 1 | index < 0) {
				Console.WriteLine("No se encontro el Campo Auditoria");
			} else {
				List.RemoveAt(index);
			}
		}
		public BE_CAMPO_AUDITORIA this[int index] {
			get { return (BE_CAMPO_AUDITORIA)List[index]; }
		}

	
		public BE_CAMPO_AUDITORIA DAME_X_NOMBRE_CAMPO(ref string NOMBRE_CAMPO_)
		{
			int I = 0;
			for (I = 0; I <= List.Count - 1; I++) {
				if (((BE_CAMPO_AUDITORIA)List[I]).NOMBRE.Trim().ToUpper() == NOMBRE_CAMPO_.Trim().ToUpper()) {
					return (BE_CAMPO_AUDITORIA)List[I];
				}
			}
            return null;
		}
		public bool EXISTE_X_NOMBRE_CAMPO(string NOMBRE_CAMPO_)
		{
			int I = 0;
			for (I = 0; I <= List.Count - 1; I++) {
				if (((BE_CAMPO_AUDITORIA)List[I]).NOMBRE.Trim().ToUpper() == NOMBRE_CAMPO_.Trim().ToUpper()) {
					return true;
				}
			}
			return false;
		}

		public BE_CAMPO_AUDITORIA X_FINALIDAD(E_FINALIDAD_CAMPO FINALIDAD_CAMPO_)
		{
			int I = 0;
			for (I = 0; I <= List.Count - 1; I++) {
				if (((BE_CAMPO_AUDITORIA)List[I]).FINALIDAD == FINALIDAD_CAMPO_) {
					return (BE_CAMPO_AUDITORIA)List[I];
				}
			}
            return null;
        }


		public string LST_QUERY(string NOMBRE_TABLA__)
		{
			int I = 0;
			System.Text.StringBuilder sb = new System.Text.StringBuilder("");
			for (I = 0; I <= List.Count - 1; I++) {
				if (I < List.Count - 1) {
					sb.Append(NOMBRE_TABLA__ + "." + ((BE_CAMPO_AUDITORIA)List[I]).NOMBRE + ", ");
				} else {
					sb.Append(NOMBRE_TABLA__ + "." + ((BE_CAMPO_AUDITORIA)List[I]).NOMBRE);
				}
			}
			return sb.ToString();
		}
	}
}
