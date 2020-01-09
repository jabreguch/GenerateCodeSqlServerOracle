using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
namespace GeneraCodigo
{
	public class BE_TABLA
	{		
		public int QUITAR_N_CARACTERES_INICIALES_CAMPO { get; set; }
        public int QUITAR_N_CARACTERES_INICIALES { get; set; }
        public string Area { get; set; }
        public string NOMBRE_SIN_SIGLA_INICIAL {
			get {
				if (QUITAR_N_CARACTERES_INICIALES > 0 & NOMBRE.Length > QUITAR_N_CARACTERES_INICIALES) {
					return NOMBRE.Substring(QUITAR_N_CARACTERES_INICIALES);
				} else {
					return NOMBRE;
				}
			}
		}

        public string NombreControlador
        {
            get
            {
                if (QUITAR_N_CARACTERES_INICIALES > 0 & NOMBRE.Length > QUITAR_N_CARACTERES_INICIALES)
                {
                    string Nombre = NOMBRE;
                    //Nombre = Nombre.PriLetraMayuscula();
                    Nombre = Nombre.Substring(QUITAR_N_CARACTERES_INICIALES).Replace("_", "");
                    return Nombre.PriLetraMayuscula();
                }
                else
                {
                    string Nombre = NOMBRE;
                    //Nombre = Nombre.PriLetraMayuscula();
                    Nombre = Nombre.Replace("_", "");
                    return NOMBRE.PriLetraMayuscula();
                }
            }
        }

        public string NoEsquema { get; set; }
        public string NoBaseDatos { get; set; }
        public List<BE_CAMPO> CAMPO_COL { get; set; }
        public List<BE_INDICE> INDICE_COL { get; set; }
        public BE_BASE_DATOS BASE_DATOS { get; set; }
        public string COMENTARIO { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO { get; set; }

        public string NombrePreliminar { get; set; }


        public BE_TABLA()
		{
			BASE_DATOS = new BE_BASE_DATOS();

		}
	}
}
