using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace GeneraCodigo
{
	public class BE_CAMPO_COLECCION : CollectionBase
	{
		public void Add(BE_CAMPO CAMPO_)
		{
			List.Add(CAMPO_);
		}
		public void Remove(int index)
		{
			if (index > Count - 1 | index < 0) {
				Console.WriteLine("No se encontro la Tabla");
			} else {
				List.RemoveAt(index);
			}
		}
		public BE_CAMPO this[int index] {
			get { return (BE_CAMPO)List[index]; }
		}

		
	}
}
