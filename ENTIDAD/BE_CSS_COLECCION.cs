using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace GeneraCodigo
{
	public class BE_CSS_COLECCION : CollectionBase
	{
		public void Add(BE_CSS CSS_)
		{
			List.Add(CSS_);
		}
		public void Remove(int index)
		{
			if (index > Count - 1 | index < 0) {
				Console.WriteLine("No se encontro el Campo CSS");
			} else {
				List.RemoveAt(index);
			}
		}
		public BE_CSS this[int index] {
			get { return (BE_CSS)List[index]; }
		}

		
		public string X_TIPO_CONTROL(E_TIPO_CONTROL TIPO_CONTROL__)
		{
			int I = 0;
			for (I = 0; I <= List.Count - 1; I++) {
				if (((BE_CSS)List[I]).TIPO_CONTROL == TIPO_CONTROL__) {
					return ((BE_CSS)List[I]).CSS;
				}
			}
			return "CSS_NO_ENCONTRADO";
		}

		public BE_CAMPO ACTUALIZAR_CONTROL_CSS(ref BE_CAMPO CAMPO_)
		{

			try {

				string tx_ctrl_textarea_declara = "Protected WithEvents TXT_" + CAMPO_.NOMBRE_SIN_SIGLA_INICIAL + " As System.Web.UI.HtmlControls.HtmlTextArea \r\n";
				string tx_ctrl_select_declara = "Protected WithEvents CBO_" + CAMPO_.NOMBRE_SIN_SIGLA_INICIAL + " As System.Web.UI.HtmlControls.HtmlSelect \r\n" ;
				string tx_ctrl_input_text_declara = "Protected WithEvents TXT_" + CAMPO_.NOMBRE_SIN_SIGLA_INICIAL + " As System.Web.UI.HtmlControls.HtmlInputText \r\n" ;
				string tx_ctrl_input_CheckBox_declara = "Protected WithEvents CHK_" + CAMPO_.NOMBRE_SIN_SIGLA_INICIAL + " As System.Web.UI.HtmlControls.HtmlInputCheckBox \r\n" ;
				int I = 0;
				for (I = 0; I <= List.Count - 1; I++) {
					if (CAMPO_.TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.CARACTER & CAMPO_.TAMANO_CAMPO >= 1 & CAMPO_.TAMANO_CAMPO <= 150 & CAMPO_.FOREIGN_KEY == false) {
						CAMPO_.TIPO_CONTROL = E_TIPO_CONTROL.E_TEXT_CONTROL;
						CAMPO_.CSS = X_TIPO_CONTROL(CAMPO_.TIPO_CONTROL);
						CAMPO_.DECLARA_CONTROL_VB = tx_ctrl_input_text_declara;
					} else if (CAMPO_.TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.CARACTER & CAMPO_.TAMANO_CAMPO > 150 & CAMPO_.FOREIGN_KEY == false) {
						CAMPO_.TIPO_CONTROL = E_TIPO_CONTROL.E_AREA_CONTROL;
						CAMPO_.CSS = X_TIPO_CONTROL(CAMPO_.TIPO_CONTROL);
						CAMPO_.DECLARA_CONTROL_VB = tx_ctrl_textarea_declara;
					} else if (CAMPO_.TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.NUMERO & CAMPO_.FOREIGN_KEY == false) {
						CAMPO_.TIPO_CONTROL = E_TIPO_CONTROL.E_TEXT_CONTROL;
						CAMPO_.CSS = X_TIPO_CONTROL(CAMPO_.TIPO_CONTROL);
						CAMPO_.DECLARA_CONTROL_VB = tx_ctrl_input_text_declara;
					} else if (CAMPO_.TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.DECIMAL_ & CAMPO_.FOREIGN_KEY == false) {
						CAMPO_.TIPO_CONTROL = E_TIPO_CONTROL.E_TEXT_CONTROL;
						CAMPO_.CSS = X_TIPO_CONTROL(CAMPO_.TIPO_CONTROL);
						CAMPO_.DECLARA_CONTROL_VB = tx_ctrl_input_text_declara;
					} else if (CAMPO_.TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.TIEMPO & CAMPO_.FOREIGN_KEY == false) {
						CAMPO_.TIPO_CONTROL = E_TIPO_CONTROL.E_TEXT_CONTROL;
						CAMPO_.CSS = X_TIPO_CONTROL(CAMPO_.TIPO_CONTROL);
						CAMPO_.DECLARA_CONTROL_VB = tx_ctrl_input_text_declara;
					} else if (CAMPO_.TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.CARACTER & CAMPO_.TAMANO_CAMPO == 1 & CAMPO_.FOREIGN_KEY == false) {
						CAMPO_.TIPO_CONTROL = E_TIPO_CONTROL.E_CHECKBOX_CONTROL;
						CAMPO_.CSS = X_TIPO_CONTROL(CAMPO_.TIPO_CONTROL);
						CAMPO_.DECLARA_CONTROL_VB = tx_ctrl_input_CheckBox_declara;
					} else if (CAMPO_.FOREIGN_KEY == true) {
						CAMPO_.TIPO_CONTROL = E_TIPO_CONTROL.E_SELECT_CONTROL;
						CAMPO_.CSS = X_TIPO_CONTROL(CAMPO_.TIPO_CONTROL);
						CAMPO_.DECLARA_CONTROL_VB = tx_ctrl_select_declara;
					}
				}
				return CAMPO_;
			} catch (Exception ex) {
                throw ex;
			}
		}
	}
}
