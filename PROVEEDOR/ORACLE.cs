using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using Oracle.DataAccess.Client;
using System.IO;
using System.Collections.Generic;
namespace GeneraCodigo
{
    public class ORACLE : I_BD
    {
        public OracleConnection CONEXION(BE_BASE_DATOS BD)
        {
            OracleConnection cn = new OracleConnection(BD.CADENA_CONEXION_ORACLE);
            cn.Open();
            return cn;
        }

        public List<BE_TABLA> TABLAS(BE_BASE_DATOS BASE_DATOS)
        {
            OracleCommand cm = new OracleCommand();
            OracleConnection cn = new OracleConnection();
            OracleDataAdapter da = new OracleDataAdapter();
            DataTable dt = new DataTable();
            int I = 0;
            List<BE_TABLA> TABLA_COL = new List<BE_TABLA>();
            try
            {
                cn = CONEXION(BASE_DATOS);
                cm.Connection = cn;
                cm.CommandType = CommandType.Text;
                cm.CommandText = "SELECT DISTINCT all_tables.table_name, USER_TAB_COMMENTS.COMMENTS AS COMMENTS FROM all_tables LEFT JOIN user_tab_comments ON ALL_TABLES.TABLE_NAME= USER_TAB_COMMENTS.TABLE_NAME WHERE USER_TAB_COMMENTS.TABLE_TYPE='TABLE' AND UPPER(ALL_TABLES.OWNER) = '" + BASE_DATOS.NoEsquema.ToUpper() + "'";
                da.SelectCommand = cm;
                da.Fill(dt);
                for (I = 0; I <= dt.Rows.Count - 1; I++)
                {
                    BE_TABLA TABLA = new BE_TABLA();
                    TABLA.NoEsquema = BASE_DATOS.NoEsquema;
                    TABLA.NoBaseDatos = BASE_DATOS.NoBaseDatos;
                    TABLA.NOMBRE = dt.Rows[I]["table_name"].ToString();
                    TABLA.COMENTARIO = dt.Rows[I]["COMMENTS"].ToString();
                    TABLA.NombrePreliminar = string.Concat(TABLA.NoEsquema, ".", TABLA.NOMBRE);
                    TABLA_COL.Add(TABLA);
                }
                return TABLA_COL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cm.Dispose();
                cm = null;
                cn.Close();
                cn = null;
                da = null;
            }
        }
        private BE_CAMPO_AUDITORIA_COLECCION CAMPOS_AUDITORIA_ACTUALIZAR_TIPOS(BE_CAMPO_AUDITORIA_COLECCION CAMPO_AUDITORIA_COLECCION, BE_TIPO_DATO_COLECCION TIPO_DATO_COLECCION)
        {
            int I = 0;
            try
            {
                for (I = 0; I <= CAMPO_AUDITORIA_COLECCION.Count - 1; I++)
                {
                    TIPO_DATO_COLECCION.ACTUALIZAR_TIPO_DATO_CAMPO_AUDITORIA((BE_CAMPO_AUDITORIA)CAMPO_AUDITORIA_COLECCION[I]);
                }
                return CAMPO_AUDITORIA_COLECCION;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BE_CAMPO> CAMPOS(BE_TABLA TABLA)
        {
            OracleCommand cm = new OracleCommand();
            OracleConnection cn = new OracleConnection();
            OracleDataAdapter da = new OracleDataAdapter();
            //Dim dr As OracleDataReader
            int I = 0;
            List<BE_CAMPO> CAMPO_COL = new List<BE_CAMPO>();
            BE_TIPO_DATO_COLECCION TIPO_DATO_COLECCION = f_TIPO_DATO_COLECCION();
            try
            {
                cn = CONEXION(TABLA.BASE_DATOS);
                cm.Connection = cn;
                cm.CommandType = CommandType.Text;

                cm.CommandText = "SELECT DISTINCT user_tab_columns.table_name, user_tab_columns.column_id, " + "user_tab_columns.column_name, user_tab_columns.data_type, user_tab_columns.data_length, user_tab_columns.nullable, " + "user_col_comments.comments, CASE WHEN LENGTH (user_ind_columns.index_name) > 0 " + "THEN 'INDEX' ELSE '' END AS index_name " + "FROM user_tab_columns LEFT JOIN user_col_comments " + "ON user_tab_columns.table_name = user_col_comments.table_name " + "AND user_tab_columns.column_name = user_col_comments.column_name " + "LEFT JOIN user_ind_columns " + "ON user_tab_columns.table_name = user_ind_columns.table_name " + "AND user_tab_columns.column_name = user_ind_columns.column_name " + "WHERE user_tab_columns.table_name = upper('" + TABLA.NOMBRE + "')";

                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                CAMPOS_AUDITORIA_ACTUALIZAR_TIPOS(TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION, TIPO_DATO_COLECCION);
                for (I = 0; I <= dt.Rows.Count - 1; I++)
                {
                    if (TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.EXISTE_X_NOMBRE_CAMPO(dt.Rows[I]["column_name"].ToString()) == false)
                    {
                        BE_CAMPO CAMPO = new BE_CAMPO();
                        CAMPO.COMENTARIO = dt.Rows[I]["comments"].ToString();
                        CAMPO.NOMBRE = dt.Rows[I]["column_name"].ToString();
                        CAMPO.NOMBRE_TABLA = dt.Rows[I]["table_name"].ToString();
                        CAMPO.NOMBRE_TABLA_SIN_SIGLA_INICIAL = TABLA.NOMBRE_SIN_SIGLA_INICIAL;
                        CAMPO.QUITAR_N_CARACTERES_INICIALES = TABLA.QUITAR_N_CARACTERES_INICIALES_CAMPO;
                        if (dt.Rows[I]["nullable"].ToString().Trim().ToUpper() == "Y")
                        {
                            CAMPO.NULO = true;
                        }
                        else
                        {
                            CAMPO.NULO = false;
                        }

                        CAMPO.POSICION = Convert.ToInt32(dt.Rows[I]["column_id"].ToString());

                        if (dt.Rows[I]["index_name"].ToString().Trim().Length > 0)
                        {
                            CAMPO.PRIMARY_KEY = true;
                        }
                        else
                        {
                            CAMPO.PRIMARY_KEY = false;
                        }
                        CAMPO.TAMANO_CAMPO = Convert.ToInt32(dt.Rows[I]["data_length"].ToString());
                        CAMPO.TIPO_DATO_NATIVO = dt.Rows[I]["data_type"].ToString();

                        CAMPO = TIPO_DATO_COLECCION.ACTUALIZAR_TIPO_DATO_CAMPO(ref CAMPO);
                        //CAMPO = TABLA.BASE_DATOS.CSS_COLECCION.ACTUALIZAR_CONTROL_CSS(CAMPO)

                        CAMPO_COL.Add(CAMPO);
                    }
                }
                return CAMPO_COL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cm.Dispose();
                cm = null;
                cn.Close();
                cn = null;
                TIPO_DATO_COLECCION.Clear();
                TIPO_DATO_COLECCION = null;
            }
        }

        public List<BE_INDICE> INDICE(BE_TABLA TABLA)
        {
            OracleCommand cm = new OracleCommand();
            OracleConnection cn = new OracleConnection();
            OracleDataAdapter da = new OracleDataAdapter();
            //Dim dr As OracleDataReader
            int I = 0;
            List<BE_INDICE> INDICES_COL = new List<BE_INDICE>();

            try
            {
                cn = CONEXION(TABLA.BASE_DATOS);
                cm.Connection = cn;
                cm.CommandType = CommandType.Text;

                cm.CommandText = "SELECT CONSTRAINT_TYPE, USER_CONS_COLUMNS.COLUMN_NAME, USER_CONS_COLUMNS.CONSTRAINT_NAME " + "FROM user_constraints, USER_CONS_COLUMNS WHERE user_constraints.TABLE_NAME = USER_CONS_COLUMNS.TABLE_NAME " + "AND user_constraints.CONSTRAINT_NAME = USER_CONS_COLUMNS.CONSTRAINT_NAME " + "AND user_constraints.table_name = upper('" + TABLA.NOMBRE + "')";

                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (I = 0; I <= dt.Rows.Count - 1; I++)
                {
                    BE_INDICE _ITEM = new BE_INDICE();
                    _ITEM.CONSTRAINT_NAME = dt.Rows[I]["CONSTRAINT_NAME"].ToString();
                    _ITEM.NOMBRE_CAMPO = dt.Rows[I]["COLUMN_NAME"].ToString();
                    _ITEM.TIPO_INDICE = dt.Rows[I]["CONSTRAINT_TYPE"].ToString();
                    INDICES_COL.Add(_ITEM);
                }
                return INDICES_COL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cm.Dispose();
                cm = null;
                cn.Close();
                cn = null;
            }
        }
        private BE_TIPO_DATO_COLECCION f_TIPO_DATO_COLECCION()
        {
            BE_TIPO_DATO_COLECCION TIPO_DATO_COLECCION_ = new BE_TIPO_DATO_COLECCION();
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("TIMESTAMP", " DateTime", " OracleDbType.Date", " null", " null", E_TIPO_DATO_GENERICO.TIEMPO));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("DATE", " DateTime", " OracleDbType.Date", " null", " null", E_TIPO_DATO_GENERICO.TIEMPO));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("FLOAT", " double", " OracleDbType.Double", " 0.0", " null", E_TIPO_DATO_GENERICO.DECIMAL_));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("NUMBER", " int", " OracleDbType.Int32", " 0", " null", E_TIPO_DATO_GENERICO.NUMERO));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("VARCHAR2", " string", " OracleDbType.Varchar2", " \"\"", " null", E_TIPO_DATO_GENERICO.CARACTER));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("CHAR", " string", " OracleDbType.Char", " \"\"", " null", E_TIPO_DATO_GENERICO.CARACTER));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("NVARCHAR2", " string", " OracleDbType.NVarchar2", " \"\"", " null", E_TIPO_DATO_GENERICO.CARACTER));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("CLOB", " string", " OracleDbType.NVarchar2", " \"\"", " null", E_TIPO_DATO_GENERICO.CARACTER));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("NCLOB", " string", " OracleDbType.NVarchar2", " \"\"", " null", E_TIPO_DATO_GENERICO.CARACTER));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("LONG", " string", " OracleDbType.NVarchar2", " \"\"", " null", E_TIPO_DATO_GENERICO.CARACTER));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("BFILE", " byte[]", " OracleDbType.Blob", " null", " null", E_TIPO_DATO_GENERICO.BINARIO));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("LOB", " byte[]", " OracleDbType.Blob", " null", " null", E_TIPO_DATO_GENERICO.BINARIO));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("BLOB", " byte[]", " OracleDbType.Blob", " null", " null", E_TIPO_DATO_GENERICO.BINARIO));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("RAW", " byte[]", " OracleDbType.Blob", " null", " null", E_TIPO_DATO_GENERICO.BINARIO));
            TIPO_DATO_COLECCION_.Add(new BE_TIPO_DATO("LONG RAW", " byte[]", " OracleDbType.Blob", " null", " null", E_TIPO_DATO_GENERICO.BINARIO));
            return TIPO_DATO_COLECCION_;
        }


        public System.Text.StringBuilder BL(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)
                return null;
            int I = 0;

            StringBuilder sb = new StringBuilder();

            sb.Append("using System.Collections.Generic;" + "\r\n");
            sb.Append("using Oracle.DataAccess.Client;\r\n");
            sb.Append("using Oracle.DataAccess.Types;\r\n");
            sb.Append("using System;" + "\r\n");
            sb.Append("using System.Web.Mvc;" + "\r\n");

            sb.Append("///<summary>" + "\r\n");
            sb.Append("///" + "\r\n");
            sb.Append("///</summary>" + "\r\n");
            sb.Append("///<remarks>" + "\r\n");
            sb.Append("///</remarks>" + "\r\n");
            sb.Append("///<history>" + "\r\n");
            sb.Append("/// t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n");
            sb.Append("///</history>" + "\r\n");

            sb.Append("public class BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ": BL_BASE { \r\n");
            sb.Append("DA_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " data; \r\n");

            int nu_param = 0;

            string tx_lst_x_id = null;


            for (I = 0; I <= TABLA.CAMPO_COL.Count - 1; I++)
            {
                string tx_nombre_columna = TABLA.CAMPO_COL[I].NOMBRE;
                string tx_tipo_dato_columna = TABLA.CAMPO_COL[I].TIPO_DATO_NATIVO;
                string tx_tamano_columna = "," + TABLA.CAMPO_COL[I].TAMANO_CAMPO;

                //tx_tipo_dato_columna = Me.o_Tipo_Dato(tx_tipo_dato_columna).ToString; dr.Text("NO_TIPO_ARTICULO");

                if (TABLA.CAMPO_COL[I].TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.CARACTER)
                {
                    tx_lst_x_id = tx_lst_x_id + "i." + tx_nombre_columna + "=" + "dr.Text(\"" + tx_nombre_columna + "\");\r\n";
                }
                else if (TABLA.CAMPO_COL[I].TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.NUMERO)
                {
                    tx_lst_x_id = tx_lst_x_id + "i." + tx_nombre_columna + "=" + "dr.Num(\"" + tx_nombre_columna + "\");\r\n";
                }

                nu_param = nu_param + 1;

            }


            string tx_add_ = null;

            tx_add_ = tx_add_ + "public int Add ( BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) { " + "\r\n";
            tx_add_ = tx_add_ + "return data.Add(c);" + "\r\n";
            tx_add_ = tx_add_ + "}" + "\r\n";


            string tx_edit_ = null;
        

            tx_edit_ = tx_edit_ + "public void Edit (BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {" + "\r\n";
            tx_edit_ = tx_edit_ + "data.Edit(c);\r\n";
            tx_edit_ = tx_edit_ + "}" + "\r\n";

            string tx_edit_est_ = null;
          

            tx_edit_est_ = tx_edit_est_ + "public void EditEst (BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {" + "\r\n";
            tx_edit_est_ = tx_edit_est_ + " data.EditEst(c);\r\n";
            tx_edit_est_ = tx_edit_est_ + "}" + "\r\n";

            string tx_del_ = null;
       

            tx_del_ = tx_del_ + "public void Del (BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {" + "\r\n";
            tx_del_ = tx_del_ + "data.Del(c);\r\n";
            tx_del_ = tx_del_ + "}" + "\r\n";

            string tx_lst_ = null;
            tx_lst_ = tx_lst_ + "public List<BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ">  Gets (BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n";
            tx_lst_ = tx_lst_ + "List<BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "> r = new List<BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ">(); \r\n";
            tx_lst_ = tx_lst_ + "OracleConnection cn = new OracleConnection(TX_CADENA_CONEXION); \r\n";
            tx_lst_ = tx_lst_ + "OracleDataReader dr = null;\r\n";
            tx_lst_ = tx_lst_ + "try {\r\n";
            tx_lst_ = tx_lst_ + "dr = data.Gets(cn, c);\r\n";
            tx_lst_ = tx_lst_ + "while (dr.Read()) {\r\n";
            tx_lst_ = tx_lst_ + "BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " i = new BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "();\r\n";
            tx_lst_ = tx_lst_ + tx_lst_x_id;
            tx_lst_ = tx_lst_ + "r.Add(i);\r\n";
            tx_lst_ = tx_lst_ + "}\r\n";
            tx_lst_ = tx_lst_ + "return r;" + "\r\n";
            tx_lst_ = tx_lst_ + "} catch (Exception ex) {\r\n";
            tx_lst_ = tx_lst_ + "throw ex;\r\n";
            tx_lst_ = tx_lst_ + "} finally {\r\n";
            tx_lst_ = tx_lst_ + "pCerrarDr(cn, dr);\r\n";
            tx_lst_ = tx_lst_ + "}\r\n";
            tx_lst_ = tx_lst_ + "}\r\n";

            string tx_lst_x_id_ = null;
            tx_lst_x_id_ = tx_lst_x_id_ + "public BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " Get (BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " i = null;\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "OracleConnection cn = new OracleConnection(TX_CADENA_CONEXION); \r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "OracleDataReader dr = null;\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "try {\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "dr = data.Get(cn, c);\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "if (dr.Read()) {\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "i = new BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "();\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + tx_lst_x_id;
            tx_lst_x_id_ = tx_lst_x_id_ + "}" + "\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "return i;\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "} catch (Exception ex) {" + "\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "throw ex;\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "} finally {\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "pCerrarDr(cn, dr);\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "}\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "}\r\n";


            sb.Append(tx_add_ + "\r\n" + tx_edit_ + "\r\n" + tx_edit_est_ + "\r\n" + tx_del_ + "\r\n" + tx_lst_ + "\r\n" + tx_lst_x_id_ + "\r\n");


            sb.Append("public BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "()\r\n");
            sb.Append("{\r\n");
            sb.Append("data = new DA_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "();\r\n");
            sb.Append(" }\r\n\r\n");

            sb.Append("//Destruir Objetos\r\n");
            sb.Append("bool disposed = false;\r\n");
            sb.Append("protected override void Dispose(bool disposing) {\r\n");
            sb.Append("if (disposed) return;\r\n");
            sb.Append("if (disposing) {\r\n");
            sb.Append("data = new DA_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "();\r\n");
            sb.Append("}\r\n");
            sb.Append(" disposed = true;\r\n");
            sb.Append("base.Dispose(disposing);\r\n");
            sb.Append("}" + "\r\n\r\n");

            sb.Append("~BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "() { Dispose(false); }\r\n");




            sb.Append("}");
            return sb;

        }


        public System.Text.StringBuilder DA(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)
                return null;
            int I = 0;

            StringBuilder sb = new StringBuilder();           
            sb.Append("using Oracle.DataAccess.Types;\r\n");
            sb.Append("using Oracle.DataAccess.Types;\r\n");
            sb.Append("using System.Data;\r\n");

            sb.Append("///<summary>" + "\r\n");
            sb.Append("///" + "\r\n");
            sb.Append("///</summary>" + "\r\n");
            sb.Append("///<remarks>" + "\r\n");
            sb.Append("///</remarks>" + "\r\n");
            sb.Append("///<history>" + "\r\n");
            sb.Append("/// t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n");
            sb.Append("///</history>" + "\r\n");

            sb.Append("public class DA_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ": DA_BASE {\r\n");


            string tx_add = null;
            string tx_edit = null;

            int nu_param_Add = 0;
            int nu_param_edit = 0;

            string tx_var_key = "";

            int nu_parama_var_key = -1;
            for (I = 0; I <= TABLA.CAMPO_COL.Count - 1; I++)
            {
                string tx_nombre_columna = TABLA.CAMPO_COL[I].NOMBRE;
                string tx_tamano_columna = "," + TABLA.CAMPO_COL[I].TAMANO_CAMPO;
                // Dim tx_Input_Out As String = "Input"

                if (TABLA.CAMPO_COL[I].TIPO_DATO_GENERICO != E_TIPO_DATO_GENERICO.CARACTER)
                {
                    tx_tamano_columna = "";
                }



                if (TABLA.CAMPO_COL[I].PRIMARY_KEY == true)
                {
                    //tx_Input_Out = "InputOutput"
                    nu_parama_var_key = nu_parama_var_key + 1;
                    tx_var_key = tx_var_key + "pr[" + nu_parama_var_key + "] = new OracleParameter(\"" + tx_nombre_columna + "\"," + TABLA.CAMPO_COL[I].TIPO_DATO_PARAMETRO + tx_tamano_columna + " );\r\n";
                    tx_var_key = tx_var_key + "pr[" + nu_parama_var_key + "].Value = c." + tx_nombre_columna + ";\r\n";
                }

                tx_add = tx_add + "pr[" + nu_param_Add + "] = new OracleParameter(\"" + tx_nombre_columna + "\"," + TABLA.CAMPO_COL[I].TIPO_DATO_PARAMETRO + tx_tamano_columna + " );\r\n";
                if (TABLA.CAMPO_COL[I].CAMPO_SEQUENCIA == true | TABLA.CAMPO_COL[I].PRIMARY_KEY == true)
                {
                    tx_add = tx_add + "pr[" + nu_parama_var_key + "].Direction = ParameterDirection.InputOutput;\r\n";
                }

                tx_add = tx_add + "pr[" + nu_param_Add + "].Value = c." + tx_nombre_columna + ";\r\n";
                nu_param_Add = nu_param_Add + 1;

                tx_edit = tx_edit + "pr[" + nu_param_edit + "] = new OracleParameter(\"" + tx_nombre_columna + "\"," + TABLA.CAMPO_COL[I].TIPO_DATO_PARAMETRO + tx_tamano_columna + " );\r\n";

                tx_edit = tx_edit + "pr[" + nu_param_edit + "].Value = c." + tx_nombre_columna + ";\r\n";

                nu_param_edit = nu_param_edit + 1;
            }


            string tx_add_ = null;
            tx_add_ = tx_add_ + "public int Add (BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n";
            tx_add_ = tx_add_ + "OracleParameter[] pr = new OracleParameter[" + (nu_param_Add + 1) + "];\r\n";
            tx_add_ = tx_add_ + tx_add + "\r\n";
            tx_add_ = tx_add_ + "pr[" + nu_param_Add + "] = new OracleParameter(\"" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_CREACION).NOMBRE + "\"," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_CREACION).TIPO_DATO_PARAMETRO + "," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_CREACION).TAMANO_CAMPO + " );\r\n";
            tx_add_ = tx_add_ + "pr[" + nu_param_Add + "].Value = " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_CREACION).NOMBRE + ";\r\n";


            tx_add_ = tx_add_ + "CnHelper.EjecutarQR(\"" + TABLA.NombreControlador + "Add\", pr);\r\n";
            tx_add_ = tx_add_ + "return (int)pr[0].Value;\r\n";

            tx_add_ = tx_add_ + "}\r\n";


            string tx_edit_ = null;
            tx_edit_ = tx_edit_ + "public void Edit (BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n";
            tx_edit_ = tx_edit_ + "OracleParameter[] pr = new OracleParameter[" + (nu_param_edit + 1) + "];\r\n";
            tx_edit_ = tx_edit_ + tx_edit + "\r\n";

            tx_edit_ = tx_edit_ + "pr[" + nu_param_Add + "] = new OracleParameter(\"" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "\"," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TIPO_DATO_PARAMETRO + "," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TAMANO_CAMPO + " );\r\n";
            tx_edit_ = tx_edit_ + "pr[" + nu_param_Add + "].Value = c." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + ";\r\n";

            tx_edit_ = tx_edit_ + "CnHelper.EjecutarQR(\"" + TABLA.NombreControlador + "Edit\", pr);\r\n";

            tx_edit_ = tx_edit_ + "}\r\n";
            string tx_edit_est_ = null;
            tx_edit_est_ = tx_edit_est_ + "public void EditEst (BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n";
            tx_edit_est_ = tx_edit_est_ + "OracleParameter[] pr = new OracleParameter[" + (nu_parama_var_key + 3) + "];\r\n";
            tx_edit_est_ = tx_edit_est_ + tx_var_key + "\r\n";

            tx_edit_est_ = tx_edit_est_ + "pr[" + nu_parama_var_key + 1 + "] = new OracleParameter(\"" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.CAMPO_ESTADO).NOMBRE + "\"," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TIPO_DATO_PARAMETRO + " );\r\n";
            tx_edit_est_ = tx_edit_est_ + "pr[" + nu_parama_var_key + 1 + "].Value = c." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.CAMPO_ESTADO).NOMBRE + ";\r\n";
            tx_edit_est_ = tx_edit_est_ + "pr[" + nu_parama_var_key + 2 + "] = new OracleParameter(\"" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "\"," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TIPO_DATO_PARAMETRO + "," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TAMANO_CAMPO + " );\r\n";
            tx_edit_est_ = tx_edit_est_ + "pr[" + nu_parama_var_key + 2 + "].Value = c." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + ";\r\n";

            tx_edit_est_ = tx_edit_est_ + "CnHelper.EjecutarQR(\"" + TABLA.NombreControlador + "EditEst\", pr);\r\n";

            tx_edit_est_ = tx_edit_est_ + "}\r\n";


            string tx_del_ = null;
            tx_del_ = tx_del_ + "public void Del (BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n";
            tx_del_ = tx_del_ + "OracleParameter[] pr = new OracleParameter[" + (nu_parama_var_key + 2) + "];\r\n";
            tx_del_ = tx_del_ + tx_var_key + "\r\n";

            tx_del_ = tx_del_ + "pr[" + nu_parama_var_key + 1 + "] = new OracleParameter(\"" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_ELIMINACION).NOMBRE + "\"," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TIPO_DATO_PARAMETRO + "," + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).TAMANO_CAMPO + " );\r\n";
            tx_del_ = tx_del_ + "pr[" + nu_parama_var_key + 1 + "].Value = c." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_ELIMINACION).NOMBRE + ";\r\n";

            tx_del_ = tx_del_ + "CnHelper.EjecutarQR(\"" + TABLA.NombreControlador + "Del\", pr);" + "\r\n";

            tx_del_ = tx_del_ + "}\r\n";


            string tx_lst_ = null;            
            tx_lst_ = tx_lst_ + "public OracleDataReader Gets (OracleConnection cn, BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c ) {\r\n";
            tx_lst_ = tx_lst_ + "OracleParameter[] pr = new OracleParameter[1];\r\n";
            tx_lst_ = tx_lst_ + "return CnHelper.ObtenerDR(cn,\"" + TABLA.NombreControlador + "Gets\", pr);\r\n";
            tx_lst_ = tx_lst_ + "}\r\n";

            string tx_lst_x_id_ = null;
            tx_lst_x_id_ = tx_lst_x_id_ + "public OracleDataReader Get(OracleConnection cn, BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c){\r\n";
            tx_lst_x_id_ = tx_lst_x_id_ + "OracleParameter[] pr = new OracleParameter[" + (nu_parama_var_key + 2) + "];\r\n";
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
            string tx_sequence = "";

            string tx_sequence_create = " CREATE SEQUENCE SEQ_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "  START WITH 1   MAXVALUE 999999999999999999999999999  MINVALUE 1;";


            for (I = 0; I <= TABLA.CAMPO_COL.Count - 1; I++)
            {

                if (TABLA.CAMPO_COL[I].PRIMARY_KEY == true)
                {
                    tx_select_key = tx_select_key + TABLA.NOMBRE + "." + TABLA.CAMPO_COL[I].NOMBRE + ",";

                    tx_key_var = tx_key_var + TABLA.CAMPO_COL[I].NOMBRE + "_ IN OUT " + TABLA.NOMBRE + "." + TABLA.CAMPO_COL[I].NOMBRE + "%TYPE,";

                    if (string.IsNullOrEmpty(tx_where_key.Trim()))
                    {
                        tx_where_key = tx_where_key + TABLA.NOMBRE + "." + TABLA.CAMPO_COL[I].NOMBRE + "= " + TABLA.CAMPO_COL[I].NOMBRE + "_ ";
                    }
                    else
                    {
                        tx_where_key = tx_where_key + " AND " + TABLA.NOMBRE + "." + TABLA.CAMPO_COL[I].NOMBRE + "= " + TABLA.CAMPO_COL[I].NOMBRE + "_ ";
                    }

                    tx_add = tx_add + TABLA.NOMBRE + "." + TABLA.CAMPO_COL[I].NOMBRE + ",";
                    tx_add_values = tx_add_values + TABLA.CAMPO_COL[I].NOMBRE + "_,";
                    tx_add_var = tx_add_var + TABLA.CAMPO_COL[I].NOMBRE + "_ IN OUT " + TABLA.NOMBRE + "." + TABLA.CAMPO_COL[I].NOMBRE + "%TYPE,";

                    tx_edit_var = tx_edit_var + TABLA.CAMPO_COL[I].NOMBRE + "_ IN OUT " + TABLA.NOMBRE + "." + TABLA.CAMPO_COL[I].NOMBRE + "%TYPE,";

                    tx_sequence = tx_sequence + " SELECT SEQ_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".NEXTVAL INTO " + TABLA.CAMPO_COL[I].NOMBRE + "_ FROM DUAL; ";


                }
                else
                {
                    tx_lst = tx_lst + TABLA.NOMBRE + "." + TABLA.CAMPO_COL[I].NOMBRE + ",";

                    tx_add = tx_add + TABLA.NOMBRE + "." + TABLA.CAMPO_COL[I].NOMBRE + ",";
                    tx_add_values = tx_add_values + TABLA.CAMPO_COL[I].NOMBRE + "_,";
                    tx_add_var = tx_add_var + TABLA.CAMPO_COL[I].NOMBRE + "_ " + TABLA.NOMBRE + "." + TABLA.CAMPO_COL[I].NOMBRE + "%TYPE,";

                    tx_edit = tx_edit + TABLA.NOMBRE + "." + TABLA.CAMPO_COL[I].NOMBRE + "=" + TABLA.CAMPO_COL[I].NOMBRE + "_,";
                    tx_edit_var = tx_edit_var + TABLA.CAMPO_COL[I].NOMBRE + "_ " + TABLA.NOMBRE + "." + TABLA.CAMPO_COL[I].NOMBRE + "%TYPE,";

                }
            }

            //Parametros de mantenimiento  de Insert


            tx_add = tx_add + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_CREACION).NOMBRE + ",";
            tx_add_values = tx_add_values + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_CREACION).NOMBRE + "_,";
            tx_add_var = tx_add_var + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_CREACION).NOMBRE + "_ " + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_CREACION).NOMBRE + "%TYPE,";
            //Fin Parametros de mantenimiento  de Insert

            //Parametros de mantenimiento  de Update
            tx_edit = tx_edit + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "=" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "_,";
            tx_edit = tx_edit + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.FECHA_MODIFICACION).NOMBRE + "=SYSDATE,";
            tx_edit_var = tx_edit_var + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "_ " + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "%TYPE,";
            //Fin Parametros de mantenimiento  de Update

            //Parametros de mantenimiento  de Select
            tx_lst = tx_select_key + tx_lst + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.LST_QUERY(TABLA.NOMBRE);
            //Parametros de mantenimiento  de Select


            int nu_posicion = -1;
            nu_posicion = tx_add.IndexOf(",", tx_add.Length - 1);
            if (nu_posicion != -1)
            {
                tx_add = tx_add.Substring(0, nu_posicion);
            }
            nu_posicion = tx_add_values.IndexOf(",", tx_add_values.Length - 1);
            if (nu_posicion != -1)
            {
                tx_add_values = tx_add_values.Substring(0, nu_posicion);
            }
            nu_posicion = tx_edit.IndexOf(",", tx_edit.Length - 1);
            if (nu_posicion != -1)
            {
                tx_edit = tx_edit.Substring(0, nu_posicion);
            }
            //nu_posicion = tx_lst.IndexOf(",", tx_lst.Length - 1)
            //If nu_posicion <> -1 Then
            //    tx_lst = tx_lst.Substring(0, nu_posicion)
            //End If
            nu_posicion = tx_add_var.IndexOf(",", tx_add_var.Length - 1);
            if (nu_posicion != -1)
            {
                tx_add_var = tx_add_var.Substring(0, nu_posicion);
            }
            nu_posicion = tx_edit_var.IndexOf(",", tx_edit_var.Length - 1);
            if (nu_posicion != -1)
            {
                tx_edit_var = tx_edit_var.Substring(0, nu_posicion);
            }

            //Add
            tx_add = " CREATE OR REPLACE PROCEDURE " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "_ADD (" + tx_add_var + ") IS BEGIN " + tx_sequence + "INSERT INTO " + TABLA.NOMBRE + " (" + tx_add + ") VALUES (" + tx_add_values + "); " + " RETURN; EXCEPTION WHEN OTHERS THEN  raise_application_error (-20000, SQLERRM, TRUE); END " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "_ADD;";
            //Fin Add

            //Edit
            tx_edit = " CREATE OR REPLACE PROCEDURE " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "_EDIT (" + tx_edit_var + ") IS BEGIN " + ("UPDATE " + TABLA.NOMBRE + " SET " + tx_edit + " WHERE " + tx_where_key + " ; ") + " RETURN; EXCEPTION WHEN OTHERS THEN  raise_application_error (-20000, SQLERRM, TRUE); END " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "_EDIT;";
            //Fin Edit

            //Del
            tx_del = " CREATE OR REPLACE PROCEDURE " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "_DEL (" + tx_key_var + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_ELIMINACION).NOMBRE + "_ " + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_ELIMINACION).NOMBRE + "%TYPE) IS BEGIN " + ("UPDATE " + TABLA.NOMBRE + " SET " + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.CAMPO_ELIMINADO).NOMBRE + "='I', " + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_ELIMINACION).NOMBRE + "=" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_ELIMINACION).NOMBRE + "_," + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.FECHA_ELIMINACION).NOMBRE + " = SYSDATE" + " WHERE " + tx_where_key + " ; ") + " RETURN; EXCEPTION WHEN OTHERS THEN  raise_application_error (-20000, SQLERRM, TRUE); END " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "_DEL;";
            //Fin Del

            //Edit Est
            tx_edit_est = " CREATE OR REPLACE PROCEDURE " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "_EDIT_EST (" + tx_key_var + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.CAMPO_ESTADO).NOMBRE + "_ " + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.CAMPO_ESTADO).NOMBRE + "%TYPE, " + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "_ " + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "%type) IS BEGIN " + ("UPDATE " + TABLA.NOMBRE + " SET " + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.CAMPO_ESTADO).NOMBRE + "=" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.CAMPO_ESTADO).NOMBRE + "_,  " + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "=" + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "_ , " + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.FECHA_MODIFICACION).NOMBRE + "=SYSDATE WHERE " + tx_where_key + " ; ") + " RETURN; EXCEPTION WHEN OTHERS THEN  raise_application_error (-20000, SQLERRM, TRUE); END " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "_EDIT_EST;";
            //Fin Edit Est

            string tx_cabecera_pkg = " CREATE OR REPLACE PACKAGE PKG_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " AS TYPE REFCURSOR IS REF CURSOR; " + "PROCEDURE LST (RC OUT REFCURSOR); /*-------------------------------------------------------------------------------------*/" + "PROCEDURE ID(" + tx_key_var + "RC OUT REFCURSOR); END PKG_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "; ";

            string tx_detalle_pkg = "CREATE OR REPLACE PACKAGE BODY PKG_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " AS ! END PKG_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ";";
            string tx_lst_ = " PROCEDURE LST(RC OUT REFCURSOR) IS BEGIN OPEN RC FOR  " + ("SELECT " + tx_lst + " FROM  " + TABLA.NOMBRE + " WHERE " + TABLA.NOMBRE + "." + TABLA.BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.CAMPO_ELIMINADO).NOMBRE + " ='A';") + " END LST;";
            string tx_lst_x_id_ = " PROCEDURE ID(" + tx_key_var + "RC OUT REFCURSOR) IS BEGIN OPEN RC FOR  " + ("SELECT " + tx_lst + " FROM  " + TABLA.NOMBRE + " WHERE " + tx_where_key + " ;") + " END ID; ";
            tx_detalle_pkg = tx_detalle_pkg.Replace("!", tx_lst_ + "/*-------------------------------------------------------------------------------------*/" + tx_lst_x_id_);


            sb.Append(tx_add + tx_edit + tx_del + tx_edit_est + tx_cabecera_pkg + tx_detalle_pkg + tx_sequence_create);

            return sb;
        }

        public static void pCloseDR(OracleConnection cn, OracleDataReader dr)
        {
            if ((dr != null))
            {
                if (dr.IsClosed == false)
                {
                    dr.Close();
                }
                dr.Dispose();
                dr = null;
            }
            cn.Close();
            cn.Dispose();
            cn = null;
        }

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
