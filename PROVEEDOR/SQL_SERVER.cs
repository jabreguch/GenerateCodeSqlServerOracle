using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
namespace GeneraCodigo
{
	public class SQL_SERVER : I_BD
	{
		public SqlConnection CONEXION(BE_BASE_DATOS BD)
		{
            SqlConnection cn = new SqlConnection(BD.CADENA_CONEXION_SQL_SERVER);
            cn.Open();
            return cn;
        }
		public List<BE_TABLA> TABLAS(BE_BASE_DATOS BASE_DATOS)
		{
			SqlCommand cm = new SqlCommand();
			SqlConnection cn = new SqlConnection();
			SqlDataAdapter da = new SqlDataAdapter();
			DataTable dt = new DataTable();
			int I = 0;
			List<BE_TABLA> TABLA_COL = new List<BE_TABLA>();
			try {
				cn = CONEXION(BASE_DATOS);
				cm.Connection = cn;
				cm.CommandType = CommandType.Text;

				cm.CommandText = "SELECT TABLE_NAME,TABLE_TYPE, TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES  " + "WHERE Table_Type='BASE TABLE'";


				da.SelectCommand = cm;
				da.Fill(dt);
				for (I = 0; I <= dt.Rows.Count - 1; I++) {
					BE_TABLA TABLA = new BE_TABLA();
					TABLA.NoBaseDatos = BASE_DATOS.NoBaseDatos;
                    TABLA.NoEsquema = dt.Rows[I].Text("TABLE_SCHEMA");
                    TABLA.NOMBRE = dt.Rows[I]["TABLE_NAME"].ToString();
					TABLA.TIPO = dt.Rows[I]["TABLE_TYPE"].ToString();
                    TABLA.NombrePreliminar = string.Concat(TABLA.NoEsquema, ".", TABLA.NOMBRE);

                    TABLA_COL.Add(TABLA);
				}
				return TABLA_COL;
			} catch (Exception ex) {
				MessageBox.Show(ex.ToString());
				return null;
			} finally {
				cm.Dispose();
				cm = null;
				cn.Close();
				cn = null;
				da = null;
			}
		}

		private BE_TIPO_DATO_COLECCION f_TIPO_DATO_COLECCION()
		{
			BE_TIPO_DATO_COLECCION TIPO_DATO_COLECCION_ = new BE_TIPO_DATO_COLECCION();
			TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("INT", " int", " SqlDbType.Int", " 0", " null", E_TIPO_DATO_GENERICO.NUMERO));
			TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("NUMERIC", " int", " SqlDbType.Int", " 0", " null", E_TIPO_DATO_GENERICO.NUMERO));
			TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("NVARCHAR", " string", " SqlDbType.VarChar", " \"\"", " null", E_TIPO_DATO_GENERICO.CARACTER));
			TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("VARCHAR", " string", " SqlDbType.VarChar", " \"\"", " null", E_TIPO_DATO_GENERICO.CARACTER));
			TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("FLOAT", " double", " SqlDbType.Float", " 0.0", " null", E_TIPO_DATO_GENERICO.FLOAT));
			TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("DECIMAL", " decimal", " SqlDbType.Decimal", " 0.0", " null", E_TIPO_DATO_GENERICO.DECIMAL_));
			TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("SMALLINT", " int", " SqlDbType.Int", " 0", " null", E_TIPO_DATO_GENERICO.NUMERO));
			TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("TINYINT", " int", " SqlDbType.TinyInt", " 0", " null", E_TIPO_DATO_GENERICO.NUMERO));
			TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("DATETIME", " DateTime", " SqlDbType.DateTime", " null", " null", E_TIPO_DATO_GENERICO.TIEMPO));
			TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("CHAR", " string", " SqlDbType.Char", " \"\"", " null", E_TIPO_DATO_GENERICO.CARACTER));
			TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("IMAGE", " byte()", " SqlDbType.Image", " null", " null", E_TIPO_DATO_GENERICO.BINARIO));
			TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("BINARY", " byte()", " SqlDbType.Binary", " null", " null", E_TIPO_DATO_GENERICO.BINARIO));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("VARBINARY", " byte()", " SqlDbType.Binary", " null", " null", E_TIPO_DATO_GENERICO.BINARIO));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("BIT", " int", " SqlDbType.Bit", " 0", " 0", E_TIPO_DATO_GENERICO.NUMERO));
			return TIPO_DATO_COLECCION_;
		}

		public List<BE_CAMPO> CAMPOS(BE_TABLA TABLA)
		{
			SqlCommand cm_campo = new SqlCommand();
			SqlCommand cm_constraint = new SqlCommand();
			SqlConnection cn = new SqlConnection();
			SqlDataAdapter da_campo = new SqlDataAdapter();
			SqlDataAdapter da_constraint = new SqlDataAdapter();
			int I = 0;
			List<BE_CAMPO> CAMPO_COL = new List<BE_CAMPO>();
			BE_TIPO_DATO_COLECCION TIPO_DATO_COLECCION = f_TIPO_DATO_COLECCION();
			try {
				//TABLA.NOMBRE = "TRF_EMPRESA"
				cn = CONEXION(TABLA.BASE_DATOS);
				cm_campo.Connection = cn;
				cm_campo.CommandType = CommandType.Text;
				//INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_TYPE,
				//'"LEFT OUTER JOIN  sysproperties ON sysproperties.smallid = syscolumns.colid AND sysproperties.id = syscolumns.id  " & _
				cm_campo.CommandText = "SELECT  DISTINCT   sysobjects.name AS TABLE_NAME, syscolumns.id AS ID, syscolumns.name AS COLUMN_NAME, systypes.name AS DATA_TYPE, " + "syscolumns.length AS data_length_system, '' AS COLUMN_DESCRIPTION, syscomments.text AS COLUMN_DEFAULT,  " + "syscolumns.isnullable AS NULLABLE, ordinal_position, COLUMNPROPERTY(OBJECT_ID(INFORMATION_SCHEMA.COLUMNS.TABLE_NAME),  " + "INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsIdentity') AS CAMPO_SECUENCIA,  " + "character_maximum_length AS data_length " + "FROM syscolumns INNER JOIN " + "systypes ON syscolumns.xtype = systypes.xtype LEFT OUTER JOIN " + "sysobjects ON syscolumns.id = sysobjects.id " + "LEFT OUTER JOIN syscomments ON syscolumns.cdefault = syscomments.id LEFT OUTER JOIN " + "INFORMATION_SCHEMA.COLUMNS ON syscolumns.name = INFORMATION_SCHEMA.COLUMNS.column_name LEFT OUTER JOIN " + "INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ON  " + "INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.COLUMN_NAME And " + "INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.TABLE_NAME LEFT OUTER JOIN " + "INFORMATION_SCHEMA.TABLE_CONSTRAINTS ON  " + "INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME And " + "INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.TABLE_NAME = INFORMATION_SCHEMA.TABLE_CONSTRAINTS.TABLE_NAME " + "WHERE (syscolumns.id IN " + "(SELECT     id " + "FROM          SYSOBJECTS " + "WHERE      xtype = 'U')) AND (systypes.name <> 'sysname') AND (INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '" + TABLA.NOMBRE + "') AND  " + "(sysobjects.name = '" + TABLA.NOMBRE + "')  " + "ORDER BY ordinal_position ";

				da_campo.SelectCommand = cm_campo;
				DataTable dt_campo = new DataTable();
				da_campo.Fill(dt_campo);


				cm_constraint.Connection = cn;
				cm_constraint.CommandType = CommandType.Text;
				//INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_TYPE,
				cm_constraint.CommandText = "SELECT INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME, INFORMATION_SCHEMA.TABLE_CONSTRAINTS.TABLE_SCHEMA, " + "INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_TYPE, INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.COLUMN_NAME, " + "INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.TABLE_NAME " + "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS INNER JOIN " + "INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ON " + "INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME AND " + "INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.TABLE_NAME = INFORMATION_SCHEMA.TABLE_CONSTRAINTS.TABLE_NAME " + "WHERE (INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.TABLE_NAME = '" + TABLA.NOMBRE + "')";

				da_constraint.SelectCommand = cm_constraint;
				DataTable dt_constraint = new DataTable();
				da_constraint.Fill(dt_constraint);

				CAMPOS_AUDITORIA_ACTUALIZAR_TIPOS(TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION, TIPO_DATO_COLECCION);

				for (I = 0; I <= dt_campo.Rows.Count - 1; I++) {
					if (TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.EXISTE_X_NOMBRE_CAMPO(dt_campo.Rows[I]["column_name"].ToString()) == false) {
						BE_CAMPO CAMPO = new BE_CAMPO();
						CAMPO.COMENTARIO = dt_campo.Rows[I]["COLUMN_DESCRIPTION"].ToString();
						CAMPO.NOMBRE = dt_campo.Rows[I]["column_name"].ToString();
						CAMPO.NOMBRE_TABLA = dt_campo.Rows[I]["table_name"].ToString();
						CAMPO.NOMBRE_TABLA_SIN_SIGLA_INICIAL = TABLA.NOMBRE_SIN_SIGLA_INICIAL;
						CAMPO.QUITAR_N_CARACTERES_INICIALES = TABLA.QUITAR_N_CARACTERES_INICIALES_CAMPO;
						if (dt_campo.Rows[I]["NULLABLE"].ToString().Trim().ToUpper() == "1") {
							CAMPO.NULO = true;
						} else {
							CAMPO.NULO = false;
						}

						CAMPO.POSICION = Convert.ToInt32(dt_campo.Rows[I]["ordinal_position"].ToString());
						//CONSTRAINT E INDICES
						DataRow[] dr_constraint = dt_constraint.Select("COLUMN_NAME='" + dt_campo.Rows[I]["column_name"].ToString() + "'");
						if (dr_constraint.Length > 0) {
							int J = 0;

							for (J = 0; J <= dr_constraint.Length - 1; J++) {
								if (dr_constraint[J]["CONSTRAINT_TYPE"].ToString().Trim() == "PRIMARY KEY") {
									CAMPO.PRIMARY_KEY = true;
								}

								if (dr_constraint[J]["CONSTRAINT_TYPE"].ToString().Trim() == "FOREIGN KEY") {
									CAMPO.FOREIGN_KEY = true;
								}
							}
						}
						dr_constraint = null;
						//FIN CONSTRAINT E INDICES

						if (dt_campo.Rows[I]["CAMPO_SECUENCIA"].ToString().Trim() == "1") {
							CAMPO.CAMPO_SEQUENCIA = true;
						} else {
							CAMPO.CAMPO_SEQUENCIA = false;
						}

						if (object.ReferenceEquals(dt_campo.Rows[I]["data_length"], System.DBNull.Value)) {
							CAMPO.TAMANO_CAMPO = 0;
						} else {
							CAMPO.TAMANO_CAMPO = Convert.ToInt32(dt_campo.Rows[I]["data_length"].ToString());
						}

						CAMPO.TIPO_DATO_NATIVO = dt_campo.Rows[I]["data_type"].ToString();

						CAMPO = TIPO_DATO_COLECCION.ACTUALIZAR_TIPO_DATO_CAMPO(ref CAMPO);
						//CAMPO = TABLA.BASE_DATOS.CSS_COLECCION.ACTUALIZAR_CONTROL_CSS(CAMPO)

						CAMPO_COL.Add(CAMPO);
					}
				}
				return CAMPO_COL;
			} catch (Exception ex) {
                throw ex;
			} finally {
				cn.Close();
				cn = null;
				cm_campo.Dispose();
				cm_campo = null;
				da_campo.Dispose();
				da_campo = null;
				cm_constraint.Dispose();
				cm_constraint = null;
				da_constraint.Dispose();
				da_constraint = null;
				TIPO_DATO_COLECCION.Clear();
				TIPO_DATO_COLECCION = null;
			}
		}

		private BE_CAMPO_AUDITORIA_COLECCION CAMPOS_AUDITORIA_ACTUALIZAR_TIPOS(BE_CAMPO_AUDITORIA_COLECCION CAMPO_AUDITORIA_COLECCION, BE_TIPO_DATO_COLECCION TIPO_DATO_COLECCION)
		{
			int I = 0;
			try {
				for (I = 0; I <= CAMPO_AUDITORIA_COLECCION.Count - 1; I++) {
					TIPO_DATO_COLECCION.ACTUALIZAR_TIPO_DATO_CAMPO_AUDITORIA((BE_CAMPO_AUDITORIA)CAMPO_AUDITORIA_COLECCION[I]);
				}
				return CAMPO_AUDITORIA_COLECCION;
			} catch (Exception ex) {
                throw ex;
			}
		}

		public System.Text.StringBuilder BL(BE_TABLA TABLA)
		{
			if (TABLA.CAMPO_COL.Count == 0)
				return null;
			int I = 0;

			StringBuilder sb = new StringBuilder();
            

            sb.Append("using System.Collections.Generic;" + "\r\n");
			sb.Append("using System.Data.SqlClient;" + "\r\n");
            sb.Append("using System.Data.SqlTypes;" + "\r\n");
            sb.Append("using System.Data;" + "\r\n");
            sb.Append("using System;" + "\r\n");
			sb.Append("using  Microsoft.AspNetCore.Mvc.Rendering;" + "\r\n");
            			
			sb.Append("///<summary>" + "\r\n");
			sb.Append("///" + "\r\n");
			sb.Append("///</summary>" + "\r\n");
			sb.Append("///<remarks>" + "\r\n");
			sb.Append("///</remarks>" + "\r\n");
			sb.Append("///<history>" + "\r\n");
			sb.Append("/// t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n");
			sb.Append("///</history>" + "\r\n");
			
			sb.Append("public class Bl" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ": BlBase { \r\n");
			sb.Append("Da" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " data; \r\n");
			
			int nu_param = 0;

            string tx_lst_x_id = null;


            for (I = 0; I <= TABLA.CAMPO_COL.Count - 1; I++) {
				string tx_nombre_columna = TABLA.CAMPO_COL[I].NOMBRE;
				string tx_tipo_dato_columna = TABLA.CAMPO_COL[I].TIPO_DATO_NATIVO;
				string tx_tamano_columna = "," + TABLA.CAMPO_COL[I].TAMANO_CAMPO;

                //tx_tipo_dato_columna = Me.o_Tipo_Dato(tx_tipo_dato_columna).ToString; dr.Text("NO_TIPO_ARTICULO");

                if (TABLA.CAMPO_COL[I].TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.CARACTER) {
					tx_lst_x_id = tx_lst_x_id + "i." + tx_nombre_columna + "=" + "dr.Text(\"" + tx_nombre_columna + "\");\r\n";
				} else if (TABLA.CAMPO_COL[I].TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.NUMERO) {
					tx_lst_x_id = tx_lst_x_id + "i." + tx_nombre_columna + "=" + "dr.Num(\"" + tx_nombre_columna + "\");\r\n";
                }

				nu_param = nu_param + 1;

			}


			string tx_add_ = null;
			//tx_add_ = tx_add_ + "''' ----------------------------------------------------------------------------- " + "\r\n";
			//tx_add_ = tx_add_ + "''' <summary>" + "\r\n";
			//tx_add_ = tx_add_ + "'''   Procedimiento de Negocio para Agregar un Nuevo Registro" + "\r\n";
			//tx_add_ = tx_add_ + "''' </summary>" + "\r\n";
			//tx_add_ = tx_add_ + "''' <param name=\"" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "\"></param>" + "\r\n";
			//tx_add_ = tx_add_ + "''' <returns></returns>" + "\r\n";
			//tx_add_ = tx_add_ + "''' <remarks>" + "\r\n";
			//tx_add_ = tx_add_ + "''' </remarks>" + "\r\n";
			//tx_add_ = tx_add_ + "''' <history>" + "\r\n";
			//tx_add_ = tx_add_ + "''' \t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n";
			//tx_add_ = tx_add_ + "''' </history>" + "\r\n";
			//tx_add_ = tx_add_ + "''' -----------------------------------------------------------------------------" + "\r\n";

			tx_add_ = tx_add_ + "public int Add( Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) { " + "\r\n";
			tx_add_ = tx_add_ + "return data.Add (c);" + "\r\n";
			tx_add_ = tx_add_ + "}" + "\r\n";


			string tx_edit_ = null;

			//tx_edit_ = tx_edit_ + "''' -----------------------------------------------------------------------------" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' <summary>" + "\r\n";
			//tx_edit_ = tx_edit_ + "'''   Procedimiento de Negocio para Editar el Registro" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' </summary>" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' <param name=\"" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "\"></param>" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' <returns></returns>" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' <remarks>" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' </remarks>" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' <history>" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' \t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' </history>" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' -----------------------------------------------------------------------------" + "\r\n";

			tx_edit_ = tx_edit_ + "public void Edit (Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {" + "\r\n";
			tx_edit_ = tx_edit_ + "data.Edit(c);\r\n";
			tx_edit_ = tx_edit_ + "}" + "\r\n";

			string tx_edit_est_ = null;
			//tx_edit_est_ = tx_edit_est_ + "''' -----------------------------------------------------------------------------" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' <summary>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' Procedimiento de Negocio para cambiar de estado al Registro" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' </summary>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' <param name=\"" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "\"></param>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' <returns></returns>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' <remarks>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' </remarks>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' <history>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' \t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' </history>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' -----------------------------------------------------------------------------" + "\r\n";

			tx_edit_est_ = tx_edit_est_ + "public void EditEst (Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL +  " c) {" + "\r\n";
			tx_edit_est_ = tx_edit_est_ + " data.EditEst(c);\r\n";
			tx_edit_est_ = tx_edit_est_ + "}" + "\r\n";

			string tx_del_ = null;
			//tx_del_ = tx_del_ + "''' -----------------------------------------------------------------------------" + "\r\n";
			//tx_del_ = tx_del_ + "''' <summary>" + "\r\n";
			//tx_del_ = tx_del_ + "''' Procedimiento de Negocio para eliminar un Registro" + "\r\n";
			//tx_del_ = tx_del_ + "''' </summary>" + "\r\n";
			//tx_del_ = tx_del_ + "''' <param name=\"" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "\"></param>" + "\r\n";
			//tx_del_ = tx_del_ + "''' <returns></returns>" + "\r\n";
			//tx_del_ = tx_del_ + "''' <remarks>" + "\r\n";
			//tx_del_ = tx_del_ + "''' </remarks>" + "\r\n";
			//tx_del_ = tx_del_ + "''' <history>" + "\r\n";
			//tx_del_ = tx_del_ + "''' \t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n";
			//tx_del_ = tx_del_ + "''' </history>" + "\r\n";
			//tx_del_ = tx_del_ + "''' -----------------------------------------------------------------------------" + "\r\n";

			tx_del_ = tx_del_ + "public void Del (Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {" + "\r\n";
			tx_del_ = tx_del_ + "data.Del(c);\r\n";
			tx_del_ = tx_del_ + "}" + "\r\n";

			string tx_lst_ = null;
			//tx_lst_ = tx_lst_ + "''' -----------------------------------------------------------------------------" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' <summary>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' Procedimiento de Negocio para listar todos lo registros" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' </summary>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' <param name=\"" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "\"></param>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' <returns></returns>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' <remarks>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' </remarks>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' <history>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' \t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' </history>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' -----------------------------------------------------------------------------" + "\r\n";


			tx_lst_ = tx_lst_ + "public List<Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ">  Gets (Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n";
			tx_lst_ = tx_lst_ + "List<Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "> r = new List<Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ">(); \r\n";
			tx_lst_ = tx_lst_ + "SqlConnection cn = new SqlConnection(TX_CADENA_CONEXION); \r\n";
			tx_lst_ = tx_lst_ + "SqlDataReader dr = null;\r\n";
			tx_lst_ = tx_lst_ + "try {\r\n";
			tx_lst_ = tx_lst_ + "dr = data.Gets(cn, c);\r\n";
			tx_lst_ = tx_lst_ + "while (dr.Read()) {\r\n";
			tx_lst_ = tx_lst_ + "Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " i = new Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "();\r\n";
			tx_lst_ = tx_lst_ + tx_lst_x_id;
			tx_lst_ = tx_lst_ + "r.Add(i);\r\n";
			tx_lst_ = tx_lst_ + "}\r\n";
			tx_lst_ = tx_lst_ + "return r;" + "\r\n";
			tx_lst_ = tx_lst_ + "} catch (Exception ex) {\r\n";
			tx_lst_ = tx_lst_ + "throw ex;\r\n";
			tx_lst_ = tx_lst_ + "} finally {\r\n";
			tx_lst_ = tx_lst_ + "pCloseDR(cn, dr);\r\n";
			tx_lst_ = tx_lst_ + "}\r\n";
			tx_lst_ = tx_lst_ + "}\r\n";

			string tx_lst_x_id_ = null;
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' -----------------------------------------------------------------------------" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' <summary>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' Procedimiento de Negocio para listar un registro por su Codigo Unico (Primary Key)" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' </summary>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' <param name=\"" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "\">Parametros con los criterios de busqueda</param>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' <returns>Retorna una Objeto</returns>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' <remarks>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' </remarks>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' <history>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' \t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' </history>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' -----------------------------------------------------------------------------" + "\r\n";

			tx_lst_x_id_ = tx_lst_x_id_ + "public Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " Get (Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n";			
			tx_lst_x_id_ = tx_lst_x_id_ + "Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " i = null;\r\n";
			tx_lst_x_id_ = tx_lst_x_id_ + "SqlConnection cn = new SqlConnection(TX_CADENA_CONEXION); \r\n";
			tx_lst_x_id_ = tx_lst_x_id_ + "SqlDataReader dr = null;\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "try {\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "dr = data.Get(cn, c);\r\n";
			tx_lst_x_id_ = tx_lst_x_id_ + "if (dr.Read()) {\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "i = new Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "();\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + tx_lst_x_id;
			tx_lst_x_id_ = tx_lst_x_id_ + "}" + "\r\n";
			tx_lst_x_id_ = tx_lst_x_id_ + "return i;\r\n";
			tx_lst_x_id_ = tx_lst_x_id_ + "} catch (Exception ex) {" + "\r\n";
			tx_lst_x_id_ = tx_lst_x_id_ + "throw ex;\r\n";
			tx_lst_x_id_ = tx_lst_x_id_ + "} finally {\r\n";
			tx_lst_x_id_ = tx_lst_x_id_ + "pCloseDR(cn, dr);\r\n";
			tx_lst_x_id_ = tx_lst_x_id_ + "}\r\n";
			tx_lst_x_id_ = tx_lst_x_id_ + "}\r\n";


			sb.Append(tx_add_ + "\r\n" + tx_edit_ + "\r\n" + tx_edit_est_ + "\r\n" + tx_del_ + "\r\n" + tx_lst_ + "\r\n" + tx_lst_x_id_ + "\r\n");

            //        //Destruir Objetos
            //        bool disposed = false;
            //protected override void Dispose(bool disposing) {
            //        if (disposed) return;
            //        if (disposing) {
            //            //Objetos Administrados
            //            data.Dispose(); data = null;
            //        }
            //        //Objetos No Administrados
            //        disposed = true;
            //        base.Dispose(disposing);
            //    }

            //    ~BL_FAMILIA() { Dispose(false); }
            //    //Fin Destruir Objetos

            sb.Append("public Bl" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "()\r\n");
            sb.Append("{\r\n");
            sb.Append("data = new Da" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "();\r\n");
            sb.Append(" }\r\n\r\n");
          
            sb.Append("//Destruir Objetos\r\n");
            sb.Append("bool disposed = false;\r\n");
            sb.Append("protected override void Dispose(bool disposing) {\r\n");
            sb.Append("if (disposed) return;\r\n");
            sb.Append("if (disposing) {\r\n");
            sb.Append("data = new Da" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "();\r\n");
			sb.Append("}\r\n");
            sb.Append(" disposed = true;\r\n");
            sb.Append("base.Dispose(disposing);\r\n");
            sb.Append("}" + "\r\n\r\n");

            sb.Append("~Bl" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "() { Dispose(false); }\r\n");
		



			sb.Append("}");
			return sb;

		}

		public System.Text.StringBuilder DA(BE_TABLA TABLA)
		{
			if (TABLA.CAMPO_COL.Count == 0)
				return null;
			int I = 0;

			StringBuilder sb = new StringBuilder();

            sb.Append("using System.Data.SqlClient;\r\n");
			sb.Append("using System.Data.SqlTypes;\r\n");
			sb.Append("using System.Data;\r\n");

            sb.Append("///<summary>" + "\r\n");
            sb.Append("///" + "\r\n");
            sb.Append("///</summary>" + "\r\n");
            sb.Append("///<remarks>" + "\r\n");
            sb.Append("///</remarks>" + "\r\n");
            sb.Append("///<history>" + "\r\n");
            sb.Append("/// t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n");
            sb.Append("///</history>" + "\r\n");

            sb.Append("public class Da" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ": DaBase {\r\n");
	

			string tx_add = null;
			string tx_edit = null;
		
			int nu_param_Add = 0;
			int nu_param_edit = 0;

			string tx_var_key = "";

			int nu_parama_var_key = -1;
			for (I = 0; I <= TABLA.CAMPO_COL.Count - 1; I++) {
				string tx_nombre_columna = TABLA.CAMPO_COL[I].NOMBRE;
				string tx_tamano_columna = "," + TABLA.CAMPO_COL[I].TAMANO_CAMPO;
				// Dim tx_Input_Out As String = "Input"

				if (TABLA.CAMPO_COL[I].TIPO_DATO_GENERICO != E_TIPO_DATO_GENERICO.CARACTER) {
					tx_tamano_columna = "";
				}



				if (TABLA.CAMPO_COL[I].PRIMARY_KEY == true) {
					//tx_Input_Out = "InputOutput"
					nu_parama_var_key = nu_parama_var_key + 1;
					tx_var_key = tx_var_key + "pr[" + nu_parama_var_key + "] = new SqlParameter(\"" + tx_nombre_columna + "\"," + TABLA.CAMPO_COL[I].TIPO_DATO_PARAMETRO + tx_tamano_columna + " );\r\n";
					tx_var_key = tx_var_key + "pr[" + nu_parama_var_key + "].Value = c." + tx_nombre_columna + ";\r\n";
				}

				tx_add = tx_add + "pr[" + nu_param_Add + "] = new SqlParameter(\"" + tx_nombre_columna + "\"," + TABLA.CAMPO_COL[I].TIPO_DATO_PARAMETRO + tx_tamano_columna + " );\r\n";
				if (TABLA.CAMPO_COL[I].CAMPO_SEQUENCIA == true | TABLA.CAMPO_COL[I].PRIMARY_KEY == true) {
                    tx_add = tx_add + "pr[" + nu_parama_var_key + "].Direction = ParameterDirection.InputOutput;\r\n";
				}

				tx_add = tx_add + "pr[" + nu_param_Add + "].Value = c." + tx_nombre_columna + ";\r\n";
				nu_param_Add = nu_param_Add + 1;

				tx_edit = tx_edit + "pr[" + nu_param_edit + "] = new SqlParameter(\"" + tx_nombre_columna + "\"," + TABLA.CAMPO_COL[I].TIPO_DATO_PARAMETRO + tx_tamano_columna + " );\r\n";

				tx_edit = tx_edit + "pr[" + nu_param_edit + "].Value = c." + tx_nombre_columna + ";\r\n";

				nu_param_edit = nu_param_edit + 1;
			}


			string tx_add_ = null;
			//tx_add_ = tx_add_ + "''' -----------------------------------------------------------------------------" + "\r\n";
			//tx_add_ = tx_add_ + "''' <summary>" + "\r\n";
			//tx_add_ = tx_add_ + "'''   Procedimiento de Acceso a datos para Agregar un Nuevo Registro" + "\r\n";
			//tx_add_ = tx_add_ + "''' </summary>" + "\r\n";
			//tx_add_ = tx_add_ + "''' <param name=\"cn\"></param>" + "\r\n";
			//tx_add_ = tx_add_ + "''' <param name=\"" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "\"></param>" + "\r\n";
			//tx_add_ = tx_add_ + "''' <returns></returns>" + "\r\n";
			//tx_add_ = tx_add_ + "''' <remarks>" + "\r\n";
			//tx_add_ = tx_add_ + "''' </remarks>" + "\r\n";
			//tx_add_ = tx_add_ + "''' <history>" + "\r\n";
			//tx_add_ = tx_add_ + "''' \t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n";
			//tx_add_ = tx_add_ + "''' </history>" + "\r\n";
			//tx_add_ = tx_add_ + "''' -----------------------------------------------------------------------------" + "\r\n";


			tx_add_ = tx_add_ + "public int Add (Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n";
			tx_add_ = tx_add_ + "SqlParameter[] pr = new SqlParameter[" + (nu_param_Add + 1 )+ "];\r\n";
			tx_add_ = tx_add_ + tx_add + "\r\n";
			tx_add_ = tx_add_ + "pr[" + nu_param_Add + "] = new SqlParameter(\"" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_CREACION).NOMBRE + "\"," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_CREACION).TIPO_DATO_PARAMETRO + "," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_CREACION).TAMANO_CAMPO + " );\r\n";
			tx_add_ = tx_add_ + "pr[" + nu_param_Add + "].Value = " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_CREACION).NOMBRE + ";\r\n";


			tx_add_ = tx_add_ + "CnHelper.EjecutarQR(\"" + TABLA.NombreControlador + "Add\", pr);\r\n";
			tx_add_ = tx_add_ + "return (int)pr[0].Value;\r\n";
		
			tx_add_ = tx_add_ + "}\r\n";


			string tx_edit_ = null;

			//tx_edit_ = tx_edit_ + "''' -----------------------------------------------------------------------------" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' <summary>" + "\r\n";
			//tx_edit_ = tx_edit_ + "'''   Procedimiento de Acceso a Datos para Editar el Registro" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' </summary>" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' <param name=\"cn\"></param>" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' <param name=\"" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "\"></param>" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' <returns></returns>" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' <remarks>" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' </remarks>" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' <history>" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' \t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' </history>" + "\r\n";
			//tx_edit_ = tx_edit_ + "''' -----------------------------------------------------------------------------" + "\r\n";


			tx_edit_ = tx_edit_ + "public void Edit (Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n";
            tx_edit_ = tx_edit_ + "SqlParameter[] pr = new SqlParameter[" + (nu_param_edit + 1 )+ "];\r\n";       
			tx_edit_ = tx_edit_ + tx_edit + "\r\n";

			tx_edit_ = tx_edit_ + "pr[" + nu_param_Add + "] = new SqlParameter(\"" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "\"," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TIPO_DATO_PARAMETRO + "," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TAMANO_CAMPO + " );\r\n";
			tx_edit_ = tx_edit_ + "pr[" + nu_param_Add + "].Value = c." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + ";\r\n";

			tx_edit_ = tx_edit_ + "CnHelper.EjecutarQR(\"" + TABLA.NombreControlador + "Edit\", pr);\r\n";
		
			tx_edit_ = tx_edit_ + "}\r\n";

			//Dim tx_nombre_tabla As String = drv("table_name").ToString
			//Dim tx_key As String = "CampoCodigo" ''drv("key").ToString
			//Dim txt_usu_cre As String = drv("usu_cre").ToString
			//Dim txt_usu_mod As String = drv("usu_mod").ToString
			//Dim txt_fe_cre As String = drv("fe_cre").ToString
			//Dim txt_fe_mod As String = drv("fe_mod").ToString
			//Dim txt_est_reg As String = drv("est_reg").ToString
			//Dim txt_fl_reg As String = drv("fl_reg").ToString
			//Dim tx_nombre_tabla_original As String = tx_nombre_tabla


			string tx_edit_est_ = null;

			//tx_edit_est_ = tx_edit_est_ + "''' -----------------------------------------------------------------------------" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' <summary>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' Procedimiento de Acceso a Datos para cambiar de estado al Registro" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' </summary>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' <param name=\"" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "\"></param>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' <returns></returns>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' <remarks>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' </remarks>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' <history>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' \t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' </history>" + "\r\n";
			//tx_edit_est_ = tx_edit_est_ + "''' -----------------------------------------------------------------------------" + "\r\n";

			tx_edit_est_ = tx_edit_est_ + "public void EditEst (Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n";
            tx_edit_est_ = tx_edit_est_ + "SqlParameter[] pr = new SqlParameter[" + (nu_parama_var_key + 3) + "];\r\n";
           tx_edit_est_ = tx_edit_est_ + tx_var_key + "\r\n";

			tx_edit_est_ = tx_edit_est_ + "pr[" + nu_parama_var_key + 1 + "] = new SqlParameter(\"" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.CAMPO_ESTADO).NOMBRE + "\"," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TIPO_DATO_PARAMETRO + " );\r\n";
			tx_edit_est_ = tx_edit_est_ + "pr[" + nu_parama_var_key + 1 + "].Value = c." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.CAMPO_ESTADO).NOMBRE + ";\r\n";
            tx_edit_est_ = tx_edit_est_ + "pr[" + nu_parama_var_key + 2 + "] = new SqlParameter(\"" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "\"," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TIPO_DATO_PARAMETRO + "," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TAMANO_CAMPO + " );\r\n";
			tx_edit_est_ = tx_edit_est_ + "pr[" + nu_parama_var_key + 2 + "].Value = c." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + ";\r\n";

			tx_edit_est_ = tx_edit_est_ + "CnHelper.EjecutarQR(\"" + TABLA.NombreControlador + "EditEst\", pr);\r\n";
		
			tx_edit_est_ = tx_edit_est_ + "}\r\n";


			string tx_del_ = null;

			//tx_del_ = tx_del_ + "''' -----------------------------------------------------------------------------" + "\r\n";
			//tx_del_ = tx_del_ + "''' <summary>" + "\r\n";
			//tx_del_ = tx_del_ + "''' Procedimiento de Acceso a Datos para eliminar un Registro" + "\r\n";
			//tx_del_ = tx_del_ + "''' </summary>" + "\r\n";
			//tx_del_ = tx_del_ + "''' <param name=\"" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "\"></param>" + "\r\n";
			//tx_del_ = tx_del_ + "''' <returns></returns>" + "\r\n";
			//tx_del_ = tx_del_ + "''' <remarks>" + "\r\n";
			//tx_del_ = tx_del_ + "''' </remarks>" + "\r\n";
			//tx_del_ = tx_del_ + "''' <history>" + "\r\n";
			//tx_del_ = tx_del_ + "''' \t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n";
			//tx_del_ = tx_del_ + "''' </history>" + "\r\n";
			//tx_del_ = tx_del_ + "''' -----------------------------------------------------------------------------" + "\r\n";


			tx_del_ = tx_del_ + "public void Del (Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n";
            tx_del_ = tx_del_ + "SqlParameter[] pr = new SqlParameter[" + (nu_parama_var_key + 2) + "];\r\n";
			tx_del_ = tx_del_ + tx_var_key + "\r\n";

			tx_del_ = tx_del_ + "pr[" + nu_parama_var_key + 1 + "] = new SqlParameter(\"" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_ELIMINACION).NOMBRE + "\"," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TIPO_DATO_PARAMETRO + "," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TAMANO_CAMPO + " );\r\n";
			tx_del_ = tx_del_ + "pr[" + nu_parama_var_key + 1 + "].Value = c." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_ELIMINACION).NOMBRE + ";\r\n";

			tx_del_ = tx_del_ + "CnHelper.EjecutarQR(\"" + TABLA.NombreControlador + "Del\", pr);" + "\r\n";
		
			tx_del_ = tx_del_ + "}\r\n";



			string tx_lst_ = null;
			//tx_lst_ = tx_lst_ + "''' -----------------------------------------------------------------------------" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' <summary>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' Procedimiento de Acceso a Datos para listar todos lo registros" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' </summary>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' <param name=\"" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "\"></param>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' <returns></returns>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' <remarks>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' </remarks>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' <history>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' \t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' </history>" + "\r\n";
			//tx_lst_ = tx_lst_ + "''' -----------------------------------------------------------------------------" + "\r\n";


			tx_lst_ = tx_lst_ + "public SqlDataReader Gets (SqlConnection cn, Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c ) {\r\n";
            tx_lst_ = tx_lst_ + "SqlParameter[] pr = new SqlParameter[1];\r\n";
			tx_lst_ = tx_lst_ + "return CnHelper.ObtenerDR(cn,\"" + TABLA.NombreControlador + "Gets\", pr);\r\n";
			tx_lst_ = tx_lst_ + "}\r\n";

			string tx_lst_x_id_ = null;
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' -----------------------------------------------------------------------------" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' <summary>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' Procedimiento de Acceso a Datos para listar un registro por su Codigo Unico (Primary Key)" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' </summary>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' <param name=\"" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "\">Parametros con los criterios de busqueda</param>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' <returns>Retorna un registro (DataReader)</returns>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' <remarks>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' </remarks>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' <history>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' \t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' </history>" + "\r\n";
			//tx_lst_x_id_ = tx_lst_x_id_ + "''' -----------------------------------------------------------------------------" + "\r\n";

			tx_lst_x_id_ = tx_lst_x_id_ + "public SqlDataReader Get(SqlConnection cn, Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c){\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "SqlParameter[] pr = new SqlParameter[" + (nu_parama_var_key + 2 )+ "];\r\n";
			tx_lst_x_id_ = tx_lst_x_id_ + tx_var_key + "\r\n";
			tx_lst_x_id_ = tx_lst_x_id_ + "return CnHelper.ObtenerDR(cn, \"" + TABLA.NombreControlador + "Get\", pr);\r\n";			
			tx_lst_x_id_ = tx_lst_x_id_ + "}\r\n";

			sb.Append(tx_add_ + "\r\n" + tx_edit_ + "\r\n" + tx_edit_est_ + "\r\n" + tx_del_ + "\r\n" + tx_lst_ + "\r\n" + tx_lst_x_id_ + "\r\n");

			sb.Append("}");

			return sb;
		}

		public System.Text.StringBuilder PKG(BE_TABLA TABLA)
		{
			if (TABLA.CAMPO_COL.Count == 0)
				return null;
			string tx_add = null;
			string tx_edit = null;
			string tx_del = null;
			string tx_edit_est = null;
			string tx_lst = null;
			string tx_lst_x_id = null;
			string tx_add_values = null;
			string tx_add_var = null;
			string tx_edit_var = null;
			string tx_del_var = null;
			string tx_edit_est_var = null;
			int I = 0;
			StringBuilder sb = new StringBuilder();
			string tx_select_key = "";
			string tx_where_key = "";
			string tx_key_var = "";

			string tx_key_var_primero = "";


			for (I = 0; I <= TABLA.CAMPO_COL.Count - 1; I++) {

				string no_longitud_campo = "";

				if (TABLA.CAMPO_COL[I].TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.CARACTER) {
					if (TABLA.CAMPO_COL[I].CAMPO_SEQUENCIA == true) {
						no_longitud_campo = "(" + TABLA.CAMPO_COL[I].TAMANO_CAMPO + ") OUTPUT ";
					} else if (TABLA.CAMPO_COL[I].CAMPO_SEQUENCIA == false) {
						no_longitud_campo = "(" + TABLA.CAMPO_COL[I].TAMANO_CAMPO + ")";
					}
				} else {
					no_longitud_campo = " OUTPUT ";
				}


				if (TABLA.CAMPO_COL[I].CAMPO_SEQUENCIA == true) {
					tx_key_var_primero = tx_key_var_primero + " SELECT @" + TABLA.NoEsquema + "." + TABLA.CAMPO_COL[I].NOMBRE + "=IDENT_CURRENT('" + TABLA.NOMBRE + "') ";
				}


				if (TABLA.CAMPO_COL[I].PRIMARY_KEY == true) {


					tx_select_key = tx_select_key + TABLA.CAMPO_COL[I].NOMBRE + ",\r\n";

					tx_key_var = tx_key_var + "@" + TABLA.CAMPO_COL[I].NOMBRE + "  " + TABLA.CAMPO_COL[I].TIPO_DATO_NATIVO + no_longitud_campo + ",";


					if (string.IsNullOrEmpty(tx_where_key.Trim())) {
						tx_where_key = tx_where_key + TABLA.CAMPO_COL[I].NOMBRE + "= " + "@" + TABLA.CAMPO_COL[I].NOMBRE + " ";
					} else {
						tx_where_key = tx_where_key + " And " + TABLA.CAMPO_COL[I].NOMBRE + "= " + "@" + TABLA.CAMPO_COL[I].NOMBRE + " ";
					}


					tx_add = tx_add + TABLA.CAMPO_COL[I].NOMBRE + ",\r\n";
					tx_add_values = tx_add_values + "@" + TABLA.CAMPO_COL[I].NOMBRE + ",\r\n";

					tx_add_var = tx_add_var + "@" + TABLA.CAMPO_COL[I].NOMBRE + "  " + TABLA.CAMPO_COL[I].TIPO_DATO_NATIVO + no_longitud_campo + ",\r\n";
					tx_edit_var = tx_edit_var + "@" + TABLA.CAMPO_COL[I].NOMBRE + " " + TABLA.CAMPO_COL[I].TIPO_DATO_NATIVO + no_longitud_campo + ",\r\n";


					// tx_sequence = tx_sequence & " Select " & tx_nombre_tabla_original & "_seq.nextval INTO " & dt_Est.Rows(I)("column_name").ToString.ToUpper.Trim & "_ FROM DUAL; "



				} else {
					tx_lst = tx_lst + TABLA.NOMBRE + "." + TABLA.CAMPO_COL[I].NOMBRE + ",\r\n";

					tx_add = tx_add + TABLA.CAMPO_COL[I].NOMBRE + ",\r\n";
					tx_add_values = tx_add_values + "@" + TABLA.CAMPO_COL[I].NOMBRE + ",\r\n";
					tx_add_var = tx_add_var + "@" + TABLA.CAMPO_COL[I].NOMBRE + " " + TABLA.CAMPO_COL[I].TIPO_DATO_NATIVO + no_longitud_campo + ",\r\n";

					tx_edit = tx_edit + TABLA.CAMPO_COL[I].NOMBRE + "=@" + TABLA.CAMPO_COL[I].NOMBRE + ",\r\n";
					tx_edit_var = tx_edit_var + "@" + TABLA.CAMPO_COL[I].NOMBRE + " " + TABLA.CAMPO_COL[I].TIPO_DATO_NATIVO + no_longitud_campo + ",\r\n";

				}
			}

			//Parametros de mantenimiento  de Insert
			tx_add = tx_add + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_CREACION).NOMBRE + ",";
			tx_add_values = tx_add_values + "@" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_CREACION).NOMBRE + ",";
			tx_add_var = tx_add_var + "@" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_CREACION).NOMBRE + " " + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_CREACION).TIPO_DATO_NATIVO + ",";
			//Fin Parametros de mantenimiento  de Insert

			//Parametros de mantenimiento  de Update
			tx_edit = tx_edit + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "= @" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + ",";
			tx_edit = tx_edit + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.FECHA_MODIFICACION).NOMBRE + "= getdate(),";
			tx_edit_var = tx_edit_var + "@" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + " " + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TIPO_DATO_NATIVO + ",";
			//Fin Parametros de mantenimiento  de Update

			//Parametros de mantenimiento  de Select
			tx_lst = tx_select_key + tx_lst + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.LST_QUERY(TABLA.NOMBRE);
			//Parametros de mantenimiento  de Select

			//Dim tx_nombre_columna As String = ""
			//Dim tx_tipo_dato_columna As String = ""

			//tx_add, tx_edit, tx_del, tx_edit_est, tx_lst, tx_lst_x_id As String
			int nu_posicion = -1;
			nu_posicion = tx_add.IndexOf(",", tx_add.Length - 1);
			if (nu_posicion != -1) {
				tx_add = tx_add.Substring(0, nu_posicion);
			}
			nu_posicion = tx_add_values.IndexOf(",", tx_add_values.Length - 1);
			if (nu_posicion != -1) {
				tx_add_values = tx_add_values.Substring(0, nu_posicion);
			}
			nu_posicion = tx_edit.IndexOf(",", tx_edit.Length - 1);
			if (nu_posicion != -1) {
				tx_edit = tx_edit.Substring(0, nu_posicion);
			}
			nu_posicion = tx_lst.IndexOf(",", tx_lst.Length - 1);
			if (nu_posicion != -1) {
				tx_lst = tx_lst.Substring(0, nu_posicion);
			}
			nu_posicion = tx_add_var.IndexOf(",", tx_add_var.Length - 1);
			if (nu_posicion != -1) {
				tx_add_var = tx_add_var.Substring(0, nu_posicion);
			}
			nu_posicion = tx_edit_var.IndexOf(",", tx_edit_var.Length - 1);
			if (nu_posicion != -1) {
				tx_edit_var = tx_edit_var.Substring(0, nu_posicion);
			}

			nu_posicion = tx_key_var.IndexOf(",", tx_key_var.Length - 1);
			if (nu_posicion != -1) {
				tx_key_var = tx_key_var.Substring(0, nu_posicion);
			}

			//      create procedure "Employee Sales by Country" 
			//@Beginning_Date DateTime, @Ending_Date DateTime AS
			//SELECT Employees.Country, Employees.LastName, Employees.FirstName, Orders.ShippedDate, Orders.OrderID, "Order Subtotals".Subtotal AS SaleAmount
			//FROM Employees INNER JOIN 
			//	(Orders INNER JOIN "Order Subtotals" ON Orders.OrderID = "Order Subtotals".OrderID) 
			//	ON Employees.EmployeeID = Orders.EmployeeID
			//WHERE Orders.ShippedDate Between @Beginning_Date And @Ending_Date

			//GO

			tx_add = "CREATE PROCEDURE " + TABLA.NoEsquema + "." + TABLA.NombreControlador + "Add(\r\n" + tx_add_var + ") AS\r\n\r\n" + "INSERT INTO " + TABLA.NoEsquema + "." + TABLA.NOMBRE + " (\r\n" + tx_add + ")\r\nVALUES (" + tx_add_values + ") " + tx_key_var_primero +  "\r\n";
			//Exception WHEN OTHERS THEN  raise_application_error (-20000, SQLERRM, TRUE); END " & tx_nombre_tabla & "_add;"
			//Fin Add

			//Edit
			tx_edit = "CREATE PROCEDURE " + TABLA.NoEsquema + "." + TABLA.NombreControlador + "Edit(\r\n" + tx_edit_var + ") AS\r\n\r\n" + "UPDATE " + TABLA.NoEsquema + "." + TABLA.NOMBRE + "\r\nSET " + tx_edit + "\r\nWHERE " + tx_where_key + "\r\n";
			//Exception WHEN OTHERS THEN  raise_application_error (-20000, SQLERRM, TRUE); END " & tx_nombre_tabla & "_edit;"
			//Fin Edit

			//Del
			tx_del = "CREATE PROCEDURE " + TABLA.NoEsquema + "." + TABLA.NombreControlador + "Del(\r\n" + tx_key_var + ",\r\n@" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_ELIMINACION).NOMBRE + " " + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_ELIMINACION).TIPO_DATO_NATIVO + ") AS\r\n\r\nUPDATE " + TABLA.NoEsquema + "." + TABLA.NOMBRE + "\r\nSET " + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.CAMPO_ELIMINADO).NOMBRE + "='I',\r\n" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_ELIMINACION).NOMBRE + " = @" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_ELIMINACION).NOMBRE + ",\r\n" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.FECHA_ELIMINACION).NOMBRE + "= getdate()\r\nWHERE " + tx_where_key + "\r\n";
			//"Exception WHEN OTHERS THEN  raise_application_error (-20000, SQLERRM, TRUE); END " & tx_nombre_tabla & "_del;"
			//Fin Del

			//Edit Est
			tx_edit_est = "CREATE PROCEDURE " + TABLA.NoEsquema + "." + TABLA.NombreControlador + "EditEst(\r\n" + tx_key_var + ",\r\n@" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.CAMPO_ESTADO).NOMBRE + " " + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.CAMPO_ESTADO).TIPO_DATO_NATIVO + ",\r\n@" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + " " + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TIPO_DATO_NATIVO + ") AS\r\n\r\nUPDATE " + TABLA.NoEsquema + "." + TABLA.NOMBRE + "\r\nSET " + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.CAMPO_ESTADO).NOMBRE + " = @" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.CAMPO_ESTADO).NOMBRE + ",\r\n" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "= @" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + ",\r\n" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.FECHA_MODIFICACION).NOMBRE + "= getdate()\r\nWHERE " + tx_where_key + "\r\n";
			//Exception WHEN OTHERS THEN  raise_application_error (-20000, SQLERRM, TRUE); END " & tx_nombre_tabla & "_edit_est;"
			//Fin Edit Est
            			
		
			string tx_lst_ = "CREATE PROCEDURE " + TABLA.NoEsquema + "." + TABLA.NombreControlador + "Gets AS\r\n\r\nSELECT " + tx_lst + "\r\nFROM  " + TABLA.NoEsquema + "." + TABLA.NOMBRE + "\r\nWHERE " + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD( E_FINALIDAD_CAMPO.CAMPO_ELIMINADO).NOMBRE + " ='A'\r\n";
			string tx_lst_x_id_ = "CREATE PROCEDURE " + TABLA.NoEsquema + "." + TABLA.NombreControlador + "Get " + tx_key_var + " AS\r\n\r\nSELECT " + tx_lst + "\r\nFROM  " + TABLA.NoEsquema + "." + TABLA.NOMBRE + "\r\nWHERE " + tx_where_key + "\r\n";
			
			sb.Append(tx_add + "\r\nGO\r\n" + tx_edit + "\r\nGO\r\n" + tx_del + "\r\nGO\r\n" + tx_edit_est + "\r\nGO\r\n" + tx_lst_ + "\r\nGO\r\n" + tx_lst_x_id_);

			return sb;

		}

		public System.Collections.Generic.List<BE_INDICE> INDICE(BE_TABLA TABLA)
		{
            return null;
        }

		public System.Collections.Generic.List<BE_PROCEDIMIENTO> PROCEDIMIENTOS(BE_BASE_DATOS BASE_DATOS)
		{
			SqlCommand cm = new SqlCommand();
			SqlConnection cn = new SqlConnection();
			SqlDataAdapter da = new SqlDataAdapter();
			DataTable dt = new DataTable();
			int I = 0;
			List<BE_PROCEDIMIENTO> PROCEDIMIENTO_COL = new List<BE_PROCEDIMIENTO>();
			try {
				cn = CONEXION(BASE_DATOS);
				cm.Connection = cn;
				cm.CommandType = CommandType.Text;

				PROCEDIMIENTO_COL = pCargarCabecera(BASE_DATOS);
				foreach (BE_PROCEDIMIENTO _ITEM in PROCEDIMIENTO_COL) {
					cm.CommandText = "SELECT ORDINAL_POSITION, " + "PARAMETER_MODE, " + "CHARACTER_MAXIMUM_LENGTH, " + "NUMERIC_PRECISION, " + "NUMERIC_SCALE, " + "PARAMETER_NAME, " + "DATA_TYPE, " + "PARAMETER_MODE " + "FROM INFORMATION_SCHEMA.PARAMETERS " + "WHERE UPPER(SPECIFIC_NAME) = '" + _ITEM.NOMBRE + "'  " + "AND SPECIFIC_CATALOG = '" + BASE_DATOS.SERVIDOR + "'  " + "ORDER BY ORDINAL_POSITION ";
					da.SelectCommand = cm;
					da.Fill(dt);
					List<BE_PARAMETRO> PARAMETRO_COL = new List<BE_PARAMETRO>();
					for (I = 0; I <= dt.Rows.Count - 1; I++) {
						BE_PARAMETRO PARAMETRO = new BE_PARAMETRO();
						PARAMETRO.NU_POSICION = dt.Rows[I].Num("ORDINAL_POSITION");
						if (dt.Rows[I].Text("PARAMETER_MODE") == "INOUT") {
							PARAMETRO.TIPO_PARAMETRO = E_TIPO_PARAMETRO.ENTRADA_SALIDA;
						} else if (dt.Rows[I].Text("PARAMETER_MODE") == "IN") {
							PARAMETRO.TIPO_PARAMETRO = E_TIPO_PARAMETRO.SALIDA;
						} else if (dt.Rows[I].Text("PARAMETER_MODE") == "IN") {
							PARAMETRO.TIPO_PARAMETRO = E_TIPO_PARAMETRO.ENTRADA;
						}
						PARAMETRO.NU_TAMANO_CARACTER = dt.Rows[I].Num("CHARACTER_MAXIMUM_LENGTH") ;
						PARAMETRO.NU_PRECISION_NUMERICA = dt.Rows[I].Num("NUMERIC_PRECISION");
						PARAMETRO.NU_DECIMALES = dt.Rows[I].Num("NUMERIC_SCALE");  
						PARAMETRO.NOMBRE = dt.Rows[I].Text("PARAMETER_NAME");  
						PARAMETRO.TIPO_DATO = dt.Rows[I].Text("DATA_TYPE"); 
						PARAMETRO_COL.Add(PARAMETRO);
					}
					_ITEM.PARAMETRO_COL = PARAMETRO_COL;
				}
				return PROCEDIMIENTO_COL;
				
			} catch (Exception ex) {
                throw ex;
            } finally {
				cm.Dispose();
				cm = null;
				cn.Close();
				cn = null;
				da = null;
			}



		}
		private System.Collections.Generic.List<BE_PROCEDIMIENTO> pCargarCabecera(BE_BASE_DATOS BASE_DATOS)
		{
			SqlCommand cm = new SqlCommand();
			SqlConnection cn = new SqlConnection();
			SqlDataAdapter da = new SqlDataAdapter();
			DataTable dt = new DataTable();
			int I = 0;
			List<BE_PROCEDIMIENTO> PROCEDIMIENTO_COL = new List<BE_PROCEDIMIENTO>();
			try {
				cn = CONEXION(BASE_DATOS);
				cm.Connection = cn;
				cm.CommandType = CommandType.Text;
				cm.CommandText = "SELECT routine_SCHEMA, routine_name FROM INFORMATION_SCHEMA.ROUTINES WHERE routine_type = 'PROCEDURE' AND SPECIFIC_CATALOG = '" + BASE_DATOS.SERVIDOR + "' ORDER BY routine_name";
				da.SelectCommand = cm;
				da.Fill(dt);
				for (I = 0; I <= dt.Rows.Count - 1; I++) {
					BE_PROCEDIMIENTO PROCEDIMIENTO = new BE_PROCEDIMIENTO();
					PROCEDIMIENTO.ESQUEMA = dt.Rows[I].Text("routine_SCHEMA");
					PROCEDIMIENTO.NOMBRE = dt.Rows[I].Text("routine_name"); 
					PROCEDIMIENTO_COL.Add(PROCEDIMIENTO);
				}
				return PROCEDIMIENTO_COL;
			} catch (Exception ex) {
                throw ex;
			} finally {
				cm.Dispose();
				cm = null;
				cn.Close();
				cn = null;
				da = null;
			}
		}
        
  //      public System.Text.StringBuilder PROCEDIMIENTOS_VB(System.Collections.Generic.List<BE_PROCEDIMIENTO> PROCEDIMIENTO_COL)
		//{
  //          return null;
		

		//}

      
        public StringBuilder HTMLINDEX(BE_TABLA TABLA)
        {
            return COMUN.HtmlIndex(TABLA);
        }

        public StringBuilder HTMLGET(BE_TABLA TABLA)
        {
            return COMUN.HtmlGet(TABLA);
        }

        public StringBuilder HTMLGETS(BE_TABLA TABLA)
        {
            return COMUN.HtmlGets(TABLA);
        }
   

        public StringBuilder Controlador(BE_TABLA TABLA)
        {
            return COMUN.Controlador(TABLA);
        }


        public StringBuilder ControladorApi(BE_TABLA TABLA)
        {
            return COMUN.ControladorApi(TABLA);
        }
    }
}
