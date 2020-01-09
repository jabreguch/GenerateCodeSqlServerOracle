using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;
namespace GeneraCodigo
{

	/// <summary>
	/// Enumeracion de Ordenacion
	/// </summary>
	public enum SortOrder
	{
		Ascending,
		Descending
	}
}
namespace GeneraCodigo
{

	/// <summary>
	/// This instance class is used to sort a generic collection of object instances.
	/// It automatically fetches the type and performs the necessary comparison(s) to sort.
	/// 
	/// To use, instantiate this class, set the sort string property, and pass this
	/// instance to the internal Sort() function of your generic collection.
	/// 
	/// Example:
	///     Dim MyList As List(Of MyClassType) = 'Populate the list somehow
	///     Dim Sorter As New Sorter(Of MyClassType)
	///     Sorter.SortString = "Field1 DESC, Field2"
	///     MyList.Sort(Sorter) 'After this call, the list is sorted
	/// </summary>
	public class Sorter<T> : IComparer<T>
	{


		private string _Sort;


		public Sorter()
		{
		}

		/// <summary>
		/// Instantiate the class, setting the sort string.
		/// 
		/// Example: "LastName DESC, FirstName"
		/// </summary>
		public Sorter(string SortString)
		{
			_Sort = SortString;
		}

		/// <summary>
		/// The sort string used to perform the sort. Can sort on multiple fields.
		/// Use the property names of the class and basic SQL Syntax.
		/// 
		/// Example: "LastName DESC, FirstName"
		/// </summary>
		public string SortString {
			get {
				if (_Sort != null) {
					return _Sort.Trim();
				}

				return null;
			}
			set { _Sort = value; }
		}

		/// <summary>
		/// This is an implementation of IComparer(Of T).Compare
		/// Can sort on multiple fields, or just one.
		/// </summary>
		public int Compare(T x, T y)
		{
			if (!string.IsNullOrEmpty(this.SortString)) {
				const string ERR = "The property \"{0}\" does not exist in type \"{1}\"";
				Type Type = typeof(T);
				Comparer Comp = Comparer.DefaultInvariant;
				PropertyInfo Info = null;

				foreach (string Expr in this.SortString.Split(',')) {
					SortOrder Dir = SortOrder.Ascending;
					string Field = null;

					string expr = Expr.Trim();

					if (expr.EndsWith(" DESC")) {
						Field = expr.Replace(" DESC", string.Empty).Trim();
						Dir = SortOrder.Descending;
					} else {
						Field = expr.Replace(" ASC", string.Empty).Trim();
					}

					Info = Type.GetProperty(Field);

					if (Info == null) {
						throw new MissingFieldException(string.Format(ERR, Field, Type.ToString()));
					} else {
						int Result = Comp.Compare(Info.GetValue(x, null), Info.GetValue(y, null));

						if (Result != 0) {
							if (Dir == SortOrder.Descending) {
								return Result * -1;
							} else {
								return Result;
							}
						}
					}
				}
			}
			return 0;
		}
	}
}
