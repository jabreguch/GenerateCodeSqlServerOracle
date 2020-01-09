using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace GeneraCodigo
{
	public enum E_TIPO_DATO_GENERICO
	{
		CARACTER = 1,
		NUMERO = 2,
		DECIMAL_,
		FLOAT,
		TIEMPO,
		BINARIO
	}
}
namespace GeneraCodigo
{
	public class BE_CAMPO
	{
		public bool CAMPO_SEQUENCIA { get; set; }
        public string DECLARA_CONTROL_VB { get; set; }
        public E_TIPO_CONTROL TIPO_CONTROL { get; set; }
        public string CSS { get; set; }

        public string NOMBRE_SIN_SIGLA_INICIAL {
			get {
				if (QUITAR_N_CARACTERES_INICIALES > 0 & NOMBRE.Length > QUITAR_N_CARACTERES_INICIALES) {
					return NOMBRE.Substring(QUITAR_N_CARACTERES_INICIALES);
				} else {
					return NOMBRE;
				}
			}
		}
        		
		public int QUITAR_N_CARACTERES_INICIALES { get; set; }
        public bool FOREIGN_KEY { get; set; }
        public bool PRIMARY_KEY { get; set; }
        public bool NULO { get; set; }
        public int TAMANO_CAMPO { get; set; }
        public int POSICION { get; set; }
        public string NOMBRE_TABLA_SIN_SIGLA_INICIAL { get; set; }
        public string NOMBRE_TABLA { get; set; }
        public string COMENTARIO { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO_DATO_NATIVO { get; set; }
        public string TIPO_DATO_VB { get; set; }
        public string TIPO_DATO_PARAMETRO { get; set; }
        public string VALOR_INICIAL { get; set; }
        public string VALOR_FINAL { get; set; }
        public E_TIPO_DATO_GENERICO TIPO_DATO_GENERICO { get; set; }

        public BE_CAMPO()
		{
			NULO = true;
			PRIMARY_KEY = false;
			FOREIGN_KEY = false;
		}

		private string TAMANO_CARACTERES {
			get {
                if (this.TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.CARACTER & this.TAMANO_CAMPO > 1 & this.TAMANO_CAMPO <= 150)
                {
                    return " maxlength=" + this.TAMANO_CAMPO + " ";
                }
                else if (this.TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.CARACTER & this.TAMANO_CAMPO > 150)
                {
                    return " maxlength=" + this.TAMANO_CAMPO + " rows=10 cols=40 ";
                }
                else if (this.TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.NUMERO)
                {
                    return " maxlength=9 ";
                }
                else if (this.TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.DECIMAL_)
                {
                    return " maxlength=9 ";
                }
                else if (this.TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.TIEMPO)
                {
                    return " maxlength=10 ";
                }
                else if (this.TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.CARACTER & this.TAMANO_CAMPO == 1)
                {
                    return " ";
                }
                else { return null; }
			}
		}

		private string VALIDA_DATO(E_TIPO_DATO_GENERICO TIPO_DATO_GENERICO__)
		{
			if (TIPO_DATO_GENERICO__ == E_TIPO_DATO_GENERICO.CARACTER) {
				return "Dame_Texto(";
			} else if (TIPO_DATO_GENERICO__ == E_TIPO_DATO_GENERICO.DECIMAL_) {
				return "Dame_Entero(";
			} else if (TIPO_DATO_GENERICO__ == E_TIPO_DATO_GENERICO.NUMERO) {
				return "Dame_Entero(";
			} else if (TIPO_DATO_GENERICO__ == E_TIPO_DATO_GENERICO.TIEMPO) {
				return "Dame_Texto(";
            }
            else { return null; }
        }

		public string ASIGNAR_VALOR_VARIABLE_GRABA {
			get {
				if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_TEXT_CONTROL) {
					return this.NOMBRE_TABLA_SIN_SIGLA_INICIAL + "." + NOMBRE + " = " + VALIDA_DATO(this.TIPO_DATO_GENERICO) + "TXT_" + NOMBRE + ".value)\n" ;
				} else if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_SELECT_CONTROL) {
					return this.NOMBRE_TABLA_SIN_SIGLA_INICIAL + "." + NOMBRE + " = " + VALIDA_DATO(this.TIPO_DATO_GENERICO) + "CBO_" + NOMBRE + ".value)\n";
				} else if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_AREA_CONTROL) {
					return this.NOMBRE_TABLA_SIN_SIGLA_INICIAL + "." + NOMBRE + " = " + VALIDA_DATO(this.TIPO_DATO_GENERICO) + "TXT_" + NOMBRE + ".value)\n";
				} else if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_CHECKBOX_CONTROL) {
					return this.NOMBRE_TABLA_SIN_SIGLA_INICIAL + "." + NOMBRE + " = Microsoft.VisualBasic.IIf(CHK_" + NOMBRE + ".Checked = True, \"1\", \"0\").ToString\n";
				} else if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_LABEL_CONTROL) {
                    return null;
                } else if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_OPTION_CONTROL) {
                    return null;
                }
                else { return null; }
            }
		}


		public string ASIGNA_VALOR_CONTROL_VB {
			get {
				if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_TEXT_CONTROL) {
					return "txt" + NOMBRE + ".value = " + this.NOMBRE_TABLA_SIN_SIGLA_INICIAL + "." + NOMBRE + ".ToString \n" ;
				} else if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_SELECT_CONTROL) {
					return "cbo" + NOMBRE + ".value = " + this.NOMBRE_TABLA_SIN_SIGLA_INICIAL + "." + NOMBRE + ".ToString \n" ;
				} else if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_AREA_CONTROL) {
					return "txt" + NOMBRE + ".value = " + this.NOMBRE_TABLA_SIN_SIGLA_INICIAL + "." + NOMBRE + ".ToString \n" ;
				} else if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_CHECKBOX_CONTROL) {
					return "chk" + NOMBRE + ".Checked = CBool(" + this.NOMBRE_TABLA_SIN_SIGLA_INICIAL + "." + NOMBRE + ".ToString) \n" ;
				} else if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_LABEL_CONTROL) {
                    return null;
                } else if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_OPTION_CONTROL) {
                    return null;
                }
                else { return null; }
            }
		}


		public string CONTROL_HTML {
			get {
				if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_TEXT_CONTROL) {
					return "<input id=\"txt" + NOMBRE + "\" type=\"text\" runat=\"server\" " + this.TAMANO_CARACTERES + " />";
				} else if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_SELECT_CONTROL) {
					return "<select id=\"cbo" + NOMBRE + "\" runat=\"server\" ></select>";
				} else if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_AREA_CONTROL) {
					return "<textarea id=\"txt" + NOMBRE + "\" runat=\"server\"" + this.TAMANO_CARACTERES + " ></textarea>";
				} else if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_CHECKBOX_CONTROL) {
					return "<input id=\"chk" + NOMBRE + "\" type=\"checkbox\" runat=\"server\" />" + COMENTARIO;
				} else if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_LABEL_CONTROL) {
                    return null;
                } else if (this.TIPO_CONTROL == E_TIPO_CONTROL.E_OPTION_CONTROL) {
                    return null;
                }
                else { return null; }
            }
		}

	}
}
