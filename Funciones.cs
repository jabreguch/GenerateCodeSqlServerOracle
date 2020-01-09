using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace GeneraCodigo
{
	public class Funciones
	{

        public struct sItems
		{
			private string sItemNombre;
			private int sItemCodigo;
			public sItems(string Nombre, int Codigo)
			{
				sItemNombre = Nombre;
				sItemCodigo = Codigo;
			}
			public string LeerNombre {
				get { return sItemNombre; }
			}
			public int LeerCodigo {
				get { return sItemCodigo; }
			}
		}
	}
}
