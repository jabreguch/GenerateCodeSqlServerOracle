using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace GeneraCodigo
{
	public class BE_TABLA_COLECCION : CollectionBase
	{

		public void Add(BE_TABLA TABLA_)
		{
			List.Add(TABLA_);
		}
		public void Remove(int index)
		{
			if (index > Count - 1 | index < 0) {
				Console.WriteLine("No se encontro la Tabla");
			} else {
				List.RemoveAt(index);
			}
		}
		public BE_TABLA this[int index] {
			get { return (BE_TABLA)List[index]; }
		}

		

		public void Ordenar()
		{
			IComparer Nombre = new OrdenarPorNombre();
			InnerList.Sort(Nombre);
		}


	}
}
namespace GeneraCodigo
{
	public class OrdenarPorNombre : IComparer
	{
		public int Compare(object x, object y)
		{
			BE_TABLA T1 = (BE_TABLA)x;
			IComparable IC1 = (IComparable)T1.NOMBRE;

			BE_TABLA T2 = (BE_TABLA)y;
			IComparable IC2 = (IComparable)T2.NOMBRE;

			return IC1.CompareTo(IC2);

		}
	}
}
