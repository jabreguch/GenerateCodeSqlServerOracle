using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace GeneraCodigo
{
	public class BE_TIPO_DATO
	{	
		public string TIPO_DATO_NATIVO { get; set; }
        public string TIPO_DATO_VB { get; set; }
        public string TIPO_DATO_PARAMETRO { get; set; }
        public string VALOR_INICIAL { get; set; }
        public string VALOR_FINAL { get; set; }
        public E_TIPO_DATO_GENERICO TIPO_DATO_GENERICO { get; set; }


        public BE_TIPO_DATO(string TIPO_DATO_NATIVO__, string TIPO_DATO_VB__, string TIPO_DATO_PARAMETRO__, string VALOR_INICIAL__, string VALOR_FINAL__, E_TIPO_DATO_GENERICO TIPO_DATO_GENERICO__)
		{
			TIPO_DATO_NATIVO = TIPO_DATO_NATIVO__;
			TIPO_DATO_VB = TIPO_DATO_VB__;
			TIPO_DATO_PARAMETRO = TIPO_DATO_PARAMETRO__;
			VALOR_INICIAL = VALOR_INICIAL__;
			VALOR_FINAL = VALOR_FINAL__;
			TIPO_DATO_GENERICO = TIPO_DATO_GENERICO__;
		}

		public BE_TIPO_DATO()
		{			
			TIPO_DATO_GENERICO = E_TIPO_DATO_GENERICO.CARACTER;
		}
	}
}
namespace GeneraCodigo
{

	public class BE_TIPO_DATO_COLECCION : CollectionBase
	{

		public void Add(BE_TIPO_DATO TIPO_DATO_)
		{
			List.Add(TIPO_DATO_);
		}

		public void Remove(int index)
		{
			if (index > Count - 1 | index < 0) {
				Console.WriteLine("Can't add this item");
			} else {
				List.RemoveAt(index);
			}
		}

		public BE_TIPO_DATO this[int index] {
			get { return (BE_TIPO_DATO)List[index]; }
		}

		public BE_CAMPO ACTUALIZAR_TIPO_DATO_CAMPO(ref BE_CAMPO CAMPO_)
		{
			int I = 0;
			for (I = 0; I <= List.Count - 1; I++) {
				if (((BE_TIPO_DATO)List[I]).TIPO_DATO_NATIVO.Mayuscula() == CAMPO_.TIPO_DATO_NATIVO.Mayuscula()) {
					BE_TIPO_DATO TIPO_DATO = (BE_TIPO_DATO)List[I];
					CAMPO_.TIPO_DATO_NATIVO = TIPO_DATO.TIPO_DATO_NATIVO;
					CAMPO_.TIPO_DATO_VB = TIPO_DATO.TIPO_DATO_VB;
					CAMPO_.TIPO_DATO_PARAMETRO = TIPO_DATO.TIPO_DATO_PARAMETRO;
					CAMPO_.VALOR_INICIAL = TIPO_DATO.VALOR_INICIAL;
					CAMPO_.VALOR_FINAL = TIPO_DATO.VALOR_FINAL;
					CAMPO_.TIPO_DATO_GENERICO = TIPO_DATO.TIPO_DATO_GENERICO;
					return CAMPO_;
				}
			}
            return null;
        }
		public BE_CAMPO_AUDITORIA ACTUALIZAR_TIPO_DATO_CAMPO_AUDITORIA(BE_CAMPO_AUDITORIA CAMPO_AUDITORIA_)
		{
			int I = 0;
			for (I = 0; I <= List.Count - 1; I++) {
				if (((BE_TIPO_DATO)List[I]).TIPO_DATO_GENERICO == CAMPO_AUDITORIA_.TIPO_DATO_GENERICO) {
					BE_TIPO_DATO TIPO_DATO = (BE_TIPO_DATO)List[I];
					CAMPO_AUDITORIA_.TIPO_DATO_NATIVO = TIPO_DATO.TIPO_DATO_NATIVO;
					CAMPO_AUDITORIA_.TIPO_DATO_VB = TIPO_DATO.TIPO_DATO_VB;
					CAMPO_AUDITORIA_.TIPO_DATO_PARAMETRO = TIPO_DATO.TIPO_DATO_PARAMETRO;
					CAMPO_AUDITORIA_.VALOR_INICIAL = TIPO_DATO.VALOR_INICIAL;
					CAMPO_AUDITORIA_.VALOR_FINAL = TIPO_DATO.VALOR_FINAL;
					CAMPO_AUDITORIA_.TIPO_DATO_GENERICO = TIPO_DATO.TIPO_DATO_GENERICO;
					return CAMPO_AUDITORIA_;
				}
			}
            return null;
        }



	}
}
