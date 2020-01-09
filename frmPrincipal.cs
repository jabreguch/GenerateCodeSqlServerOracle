using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneraCodigo
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }
        private BD_GENERICO BD_GENERICO;
        private BE_BASE_DATOS BASE_DATOS;
        bool FL_NORMAL = true;
        bool FL_NORMAL_Esquema = true;
        private void btnEjecuta_Click(object sender, EventArgs e)
        {
            if (this.lst_tabla.Items.Count == 0 | this.lst_tabla.SelectedItems.Count == 0)
                return;
            try
            {
                int I = 0;
                CheckedListBox.CheckedItemCollection o_cheks = null;
                o_cheks = lst_tabla.CheckedItems;
                for (I = 0; I <= o_cheks.Count - 1; I++)
                {
                    BE_TABLA TABLA = (BE_TABLA)o_cheks[I];
                    TABLA.Area = TxtArea.Text;

                    if (this.chk_PKG.Checked == true)
                    {
                        p_PKG(TABLA);
                    }
                    if (this.chk_BE.Checked == true)
                    {
                        p_BE(TABLA);
                    }
                    if (this.chk_DA.Checked == true)
                    {
                        p_DA(TABLA);
                    }
                    if (this.chk_BL.Checked == true)
                    {
                        p_BL(TABLA);
                    }
                    if (this.chk_Html.Checked == true)
                    {
                        HTMLINDEX(TABLA);
                        HTMLGETS(TABLA);
                        HTMLGET(TABLA);
                        //p_WF_TABLA(TABLA);
                        //p_WF_GRILLA(TABLA);
                    }

                    if (this.chkControlador.Checked == true)
                    {
                        Controlador(TABLA);
                        //p_WF_TABLA(TABLA);
                        //p_WF_GRILLA(TABLA);
                    }

                    if (this.chkControladorApi.Checked == true)
                    {
                        ControladorApi(TABLA);
                        //p_WF_TABLA(TABLA);
                        //p_WF_GRILLA(TABLA);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        System.Windows.Forms.CheckedListBox.CheckedItemCollection o_cheks_tabla_sel;
        private BE_CAMPO_AUDITORIA_COLECCION f_Campos_Auditoria()
        {
            BE_CAMPO_AUDITORIA_COLECCION CAMPO_AUDITORIA_COLECCION = new BE_CAMPO_AUDITORIA_COLECCION();
            CAMPO_AUDITORIA_COLECCION.Add(new BE_CAMPO_AUDITORIA(txtEST_REG.Text, E_TIPO_DATO_GENERICO.NUMERO, E_FINALIDAD_CAMPO.CAMPO_ESTADO, 4));
            CAMPO_AUDITORIA_COLECCION.Add(new BE_CAMPO_AUDITORIA(txtUSU_ELI.Text, E_TIPO_DATO_GENERICO.NUMERO, E_FINALIDAD_CAMPO.USUARIO_ELIMINACION, 4));
            CAMPO_AUDITORIA_COLECCION.Add(new BE_CAMPO_AUDITORIA(txtUSU_MOD.Text, E_TIPO_DATO_GENERICO.NUMERO, E_FINALIDAD_CAMPO.USUARIO_MODIFICACION, 4));
            CAMPO_AUDITORIA_COLECCION.Add(new BE_CAMPO_AUDITORIA(txtUSU_CRE.Text, E_TIPO_DATO_GENERICO.NUMERO, E_FINALIDAD_CAMPO.USUARIO_CREACION, 4));
            CAMPO_AUDITORIA_COLECCION.Add(new BE_CAMPO_AUDITORIA(txtFE_CRE.Text, E_TIPO_DATO_GENERICO.TIEMPO, E_FINALIDAD_CAMPO.FECHA_CREACION, 0));
            CAMPO_AUDITORIA_COLECCION.Add(new BE_CAMPO_AUDITORIA(txtFE_ELI.Text, E_TIPO_DATO_GENERICO.TIEMPO, E_FINALIDAD_CAMPO.FECHA_ELIMINACION, 0));
            CAMPO_AUDITORIA_COLECCION.Add(new BE_CAMPO_AUDITORIA(txtFE_MOD.Text, E_TIPO_DATO_GENERICO.TIEMPO, E_FINALIDAD_CAMPO.FECHA_MODIFICACION, 0));
            CAMPO_AUDITORIA_COLECCION.Add(new BE_CAMPO_AUDITORIA(txtSIT_REG.Text, E_TIPO_DATO_GENERICO.CARACTER, E_FINALIDAD_CAMPO.CAMPO_ELIMINADO, 1));
            return CAMPO_AUDITORIA_COLECCION;
        }  

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            BASE_DATOS = new BE_BASE_DATOS();
            BD_GENERICO = new BD_GENERICO(E_TIPO_BD.ORACLE);

            this.lbl_base_Datos.Visible = false;
            this.txt_base_datos.Visible = false;
            ArrayList O_LISTA_BD = new ArrayList();
            O_LISTA_BD.Add(new Funciones.sItems("ORACLE", 1));
            O_LISTA_BD.Add(new Funciones.sItems("SQL SERVER", 2));
            cbo_base_datos.DataSource = O_LISTA_BD;
            cbo_base_datos.DisplayMember = "LeerNombre";
            cbo_base_datos.ValueMember = "LeerCodigo";
            cbo_base_datos.Refresh();
        }

        private void chkTodoTabla_CheckedChanged(object sender, EventArgs e)
        {
            int I = 0;
            for (I = 0; I <= this.lst_tabla.Items.Count - 1; I++)
            {
                if (this.chkTodoTabla.Checked == true)
                {
                    lst_tabla.SetItemCheckState(I, CheckState.Checked);
                }
                else if (this.chkTodoTabla.Checked == true)
                {
                    lst_tabla.SetItemCheckState(I, CheckState.Unchecked);
                }
            }
        }



        private void p_BE(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0) return;
            string tx_path = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\Be\\Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".cs";
            string tx_path_dir = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\Be";

            if (Directory.Exists(tx_path_dir) == false)
            {
                Directory.CreateDirectory(tx_path_dir);
            }

            if (File.Exists(tx_path) == true)
            {
                File.Delete(tx_path);
            }

            int I = 0;
            StringBuilder sb = new StringBuilder();


            sb.Append("using System.Runtime.Serialization;\r\n");
            sb.Append("using System;\r\n");
            sb.Append("using System.Collections.Generic;\r\n");

            sb.Append("///<summary>" + "\r\n");
            sb.Append("///" + "\r\n");
            sb.Append("///</summary>" + "\r\n");
            sb.Append("///<remarks>" + "\r\n");
            sb.Append("///</remarks>" + "\r\n");
            sb.Append("///<history>" + "\r\n");
            sb.Append("/// t[" + TABLA.BASE_DATOS.AUTOR + "]\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\tCreated" + "\r\n");
            sb.Append("///</history>" + "\r\n");

            sb.Append("public class Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ": BeBase {\r\n");
            sb.Append("!");
            //sb.Append("!");


            //string tx_atributo = "";
            //string tx_atributo_inicializar = "";
            //string tx_atributo_finalizar = "";
            string tx_propiedad = "";


            for (I = 0; I <= TABLA.CAMPO_COL.Count - 1; I++)
            {
                //tx_atributo = tx_atributo + "Private " + TABLA.CAMPO_COL[I].NOMBRE + "_ As " + TABLA.CAMPO_COL[I].TIPO_DATO_VB + "\r\n";

                //tx_atributo_inicializar = tx_atributo_inicializar + TABLA.CAMPO_COL[I].NOMBRE + "_ = " + TABLA.CAMPO_COL[I].VALOR_INICIAL + "\r\n";
                //tx_atributo_finalizar = tx_atributo_finalizar + TABLA.CAMPO_COL[I].NOMBRE + "_ = " + TABLA.CAMPO_COL[I].VALOR_FINAL + "\r\n";

                //tx_propiedad = tx_propiedad + "''' -----------------------------------------------------------------------------" + "\r\n";
                //tx_propiedad = tx_propiedad + "''' <summary>" + "\r\n";
                //tx_propiedad = tx_propiedad + "''' " + TABLA.CAMPO_COL[I].COMENTARIO + "\r\n";
                //tx_propiedad = tx_propiedad + "''' </summary>" + "\r\n";
                //tx_propiedad = tx_propiedad + "''' <value></value>" + "\r\n";
                //tx_propiedad = tx_propiedad + "''' <remarks>" + (TABLA.CAMPO_COL[I].PRIMARY_KEY == true ? "PRIMARY KEY   " : "").ToString() + (TABLA.CAMPO_COL[I].FOREIGN_KEY == true ? "FOREIGN KEY " : "").ToString() + "</remarks>" + "\r\n";
                //tx_propiedad = tx_propiedad + "''' <history>" + "\r\n";
                //tx_propiedad = tx_propiedad + "''' \t[" + this.txt_autor.Text + "]\tCreated\t" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\r\n";
                //tx_propiedad = tx_propiedad + "''' </history>" + "\r\n";
                //tx_propiedad = tx_propiedad + "''' -----------------------------------------------------------------------------" + "\r\n";
                //[DataMember(EmitDefaultValue = false, Name = "CO_FAMILIA")] public int CO_FAMILIA { get; set; }
                tx_propiedad = tx_propiedad + "[DataMember(EmitDefaultValue = false, Name = \"" + TABLA.CAMPO_COL[I].NOMBRE + "\")] public " + TABLA.CAMPO_COL[I].TIPO_DATO_VB + "  " + TABLA.CAMPO_COL[I].NOMBRE + " { get; set; }\r\n";
                //tx_propiedad = tx_propiedad + "Get" + "\r\n";
                //tx_propiedad = tx_propiedad + "Return " + TABLA.CAMPO_COL[I].NOMBRE + "_" + "\r\n";
                //tx_propiedad = tx_propiedad + "End Get " + "\r\n";
                //tx_propiedad = tx_propiedad + "Set(ByVal Value As " + TABLA.CAMPO_COL[I].TIPO_DATO_VB + ") " + "\r\n";
                //tx_propiedad = tx_propiedad + TABLA.CAMPO_COL[I].NOMBRE + "_ = Value " + "\r\n";
                //tx_propiedad = tx_propiedad + "End Set" + "\r\n";
                //tx_propiedad = tx_propiedad + "End Property" + "\r\n";

            }

            //sb.Replace("¡", tx_atributo);
            sb.Replace("!", tx_propiedad);


            //        bool disposed = false;
            //protected override void Dispose(bool disposing)
            //    {
            //        if (disposed) return;
            //        if (disposing){
            //            //Objetos Administrados
            //        }
            //        //Objetos No Administrados
            //        disposed = true;
            //        base.Dispose(disposing);
            //    }

            //    ~BE_FAMILIA() { Dispose(false); }

            sb.Append(" bool disposed = false;\r\n");
            sb.Append("protected override void Dispose(bool disposing){\r\n");
            sb.Append("if (disposed) return;\r\n");
            sb.Append("if (disposing){\r\n");
            //'sb.Append(tx_atributo_finalizar)
            sb.Append("}\r\n");
            sb.Append("disposed = true;\r\n");
            sb.Append("}\r\n");

            sb.Append("~Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "() { Dispose(false); }\r\n");
            sb.Append("}");

            FileStream fs = File.Create(tx_path);
            byte[] info = new UTF8Encoding(true).GetBytes(sb.ToString());

            fs.Write(info, 0, info.Length);
            fs.Close();

        }


        private void p_PKG(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)
                return;
            string tx_path = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\PKG\\PKG_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".txt";
            string tx_path_dir = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\PKG";
            if (Directory.Exists(tx_path_dir) == false)
            {
                Directory.CreateDirectory(tx_path_dir);
            }
            
            if (File.Exists(tx_path) == true)
            {
                File.Delete(tx_path);
            }
            FileStream fs = File.Create(tx_path);
            byte[] info = new UTF8Encoding(true).GetBytes(BD_GENERICO.PKG(TABLA).ToString());
            fs.Write(info, 0, info.Length);
            fs.Close();
        }

        private void HTMLINDEX(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)
                return;
            string tx_path = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\View\\" + TABLA.NombreControlador + "\\Index.cshtml";
            string tx_path_dir = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\View\\" + TABLA.NombreControlador;
            if (Directory.Exists(tx_path_dir) == false)
            {
                Directory.CreateDirectory(tx_path_dir);
            }
            if (File.Exists(tx_path) == true)
            {
                File.Delete(tx_path);
            }
            FileStream fs = File.Create(tx_path);
            byte[] info = new UTF8Encoding(true).GetBytes(this.BD_GENERICO.HTMLINDEX(TABLA).ToString());
            fs.Write(info, 0, info.Length);
            fs.Close();
        }
        private void HTMLGET(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)
                return;
            string tx_path = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\View\\" + TABLA.NombreControlador + "\\Get.cshtml";
            string tx_path_dir = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\View\\" + TABLA.NombreControlador;
            if (Directory.Exists(tx_path_dir) == false)
            {
                Directory.CreateDirectory(tx_path_dir);
            }
            if (File.Exists(tx_path) == true)
            {
                File.Delete(tx_path);
            }
            FileStream fs = File.Create(tx_path);
            byte[] info = new UTF8Encoding(true).GetBytes(this.BD_GENERICO.HTMLGET(TABLA).ToString());
            fs.Write(info, 0, info.Length);
            fs.Close();
        }

        private void HTMLGETS(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)
                return;
            string tx_path = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\View\\" + TABLA.NombreControlador + "\\Gets.cshtml";
            string tx_path_dir = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\View\\" + TABLA.NombreControlador;
            if (Directory.Exists(tx_path_dir) == false)
            {
                Directory.CreateDirectory(tx_path_dir);
            }
            if (File.Exists(tx_path) == true)
            {
                File.Delete(tx_path);
            }
            FileStream fs = File.Create(tx_path);
            byte[] info = new UTF8Encoding(true).GetBytes(this.BD_GENERICO.HTMLGETS(TABLA).ToString());
            fs.Write(info, 0, info.Length);
            fs.Close();
        }
  

        private void Controlador(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)
                return;
            string tx_path = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\Controllers\\" + TABLA.NombreControlador + "Controller.cs";
            string tx_path_dir = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\Controllers";
            if (Directory.Exists(tx_path_dir) == false)
            {
                Directory.CreateDirectory(tx_path_dir);
            }
            if (File.Exists(tx_path) == true)
            {
                File.Delete(tx_path);
            }
            FileStream fs = File.Create(tx_path);
            byte[] info = new UTF8Encoding(true).GetBytes(this.BD_GENERICO.Controlador(TABLA).ToString());
            fs.Write(info, 0, info.Length);
            fs.Close();
        }

        private void ControladorApi(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)
                return;
            string tx_path = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\Controllers\\Api\\" + TABLA.NombreControlador + "ApiController.cs";
            string tx_path_dir = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\Controllers\\Api";
            if (Directory.Exists(tx_path_dir) == false)
            {
                Directory.CreateDirectory(tx_path_dir);
            }
            if (File.Exists(tx_path) == true)
            {
                File.Delete(tx_path);
            }
            FileStream fs = File.Create(tx_path);
            byte[] info = new UTF8Encoding(true).GetBytes(this.BD_GENERICO.ControladorApi(TABLA).ToString());
            fs.Write(info, 0, info.Length);
            fs.Close();
        }


        private void p_DA(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)
                return;
            string tx_path = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\Da\\Da" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".cs";
            string tx_path_dir = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\Da";
            if (Directory.Exists(tx_path_dir) == false)
            {
                Directory.CreateDirectory(tx_path_dir);
            }
            if (File.Exists(tx_path) == true)
            {
                File.Delete(tx_path);
            }
            FileStream fs = File.Create(tx_path);
            byte[] info = new UTF8Encoding(true).GetBytes(this.BD_GENERICO.DA(TABLA).ToString());
            fs.Write(info, 0, info.Length);
            fs.Close();
        }

        private void p_BL(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)
                return;
            string tx_path = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\Bl\\Bl" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".cs";
            string tx_path_dir = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\Bl";
            if (Directory.Exists(tx_path_dir) == false)
            {
                Directory.CreateDirectory(tx_path_dir);
            }
            if (File.Exists(tx_path) == true)
            {
                File.Delete(tx_path);
            }
            FileStream fs = File.Create(tx_path);
            byte[] info = new UTF8Encoding(true).GetBytes(this.BD_GENERICO.BL(TABLA).ToString());
            fs.Write(info, 0, info.Length);
            fs.Close();
        }

        private void p_WF_TABLA(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)
                return;

            //Dim o_file_vb As IO.File
            string tx_path = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\WF\\wf_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL.ToLower() + ".aspx";
            string tx_path_vb = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\WF\\wf_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL.ToLower() + ".aspx.cs";

            string tx_path_dir = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\WF";
            string tx_path_wf = Path.GetFullPath(".") + "\\wf_test2.aspx";
            string tx_path_wf_vb = Path.GetFullPath(".") + "\\wf_test2.aspx.cs";


            StreamReader e_fs_aspx = File.OpenText(tx_path_wf);
            string tx_archiv_aspx = e_fs_aspx.ReadToEnd();


            StreamReader e_fs_vb = File.OpenText(tx_path_wf_vb);
            string tx_archiv_aspx_vb = e_fs_vb.ReadToEnd();

            if (Directory.Exists(tx_path_dir) == false)
            {
                Directory.CreateDirectory(tx_path_dir);
            }

            if (File.Exists(tx_path) == true)
            {
                File.Delete(tx_path);
            }

            if (File.Exists(tx_path_vb) == true)
            {
                File.Delete(tx_path_vb);
            }
            else
            {
            }

            string tx_carga_listas = null;
            StringBuilder sb = new StringBuilder();

            string tx_Procedimiento_Grabar = null;
            string tx_cargar_registro = null;
            string tx_declara_Controles = null;
            string tx_controles_html = null;
            int I = 0;

            if (TABLA.CAMPO_COL.Count > 0)
            {
                for (I = 0; I <= TABLA.CAMPO_COL.Count - 1; I++)
                {
                    tx_controles_html = tx_controles_html + "<tr><th>" + TABLA.CAMPO_COL[I].COMENTARIO + (TABLA.CAMPO_COL[I].NULO == false ? "<em>*</em>" : "").ToString() + "</th><td>" + TABLA.CAMPO_COL[I].CONTROL_HTML + "</td></tr>";
                    //Grabacion
                    tx_Procedimiento_Grabar = tx_Procedimiento_Grabar + TABLA.CAMPO_COL[I].ASIGNAR_VALOR_VARIABLE_GRABA;
                    //Fin Grabacion 
                    //declaracion y carga de de controles
                    tx_cargar_registro = TABLA.CAMPO_COL[I].ASIGNA_VALOR_CONTROL_VB;
                    // tx_cargar_registro & tx_ctrl_imput_text.Replace("{1}", tx_nombre_columna.Substring(3).ToLower).Replace("{campo}", tx_nombre_columna)
                    //fin declaracion y carga de de controles
                }
                tx_controles_html = tx_controles_html + "</tbody><tfoot><tr><td colspan=\"2\" > <input class=\"submit\" id=\"btn_graba\" onclick=\"p_Graba();\" type=\"submit\" value=\"Grabar\" /></td></tr></tfoot></table>";
            }
            //tx_nombre_tabla
            tx_archiv_aspx = tx_archiv_aspx.Replace("{1}", txt_nombre_proyecto.Text);
            tx_archiv_aspx = tx_archiv_aspx.Replace("{2}", TABLA.NOMBRE_SIN_SIGLA_INICIAL.ToLower() + "");
            tx_archiv_aspx = tx_archiv_aspx.Replace("{4}", tx_controles_html);

            //archivo vb
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{1}", TABLA.NOMBRE_SIN_SIGLA_INICIAL + "");
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{11}", this.txt_nombre_proyecto.Text);
            // tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{2}", tx_nombre_tabla_presentacion)
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{13}", TABLA.COMENTARIO);
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{15}", this.txt_autor.Text);
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{16}", System.DateTime.Now.ToString("dd/MM/yyyy"));

            //Declaracion dinamica
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{3}", tx_declara_Controles);


            string tx_cabecera = "Dim BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " As New BL.BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " " + "\r\n" + "Dim " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " As New BE.BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " " + "\r\n" + "Try " + "\r\n";

            string tx_pie = "Dim nu_resultado As Integer" + "\r\n" + "If Estado = Estado_Actualizacion.Grabar Then " + "\r\n" + "nu_resultado = BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".Agregar(" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ") " + "\r\n" + "ElseIf Estado = Estado_Actualizacion.Editar Then " + "\r\n" + "nu_resultado = BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".Editar(" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ") " + "\r\n" + "End If " + "\r\n" + "Dim sb As New System.Text.StringBuilder " + "\r\n" + "sb.Append(\"<script language=JavaScript>\") " + "\r\n" + "sb.Append(\"p_Cerrar(0);\") " + "\r\n" + "sb.Append(\"</script>\") " + "\r\n" + "Dim js As String = sb.ToString() " + "\r\n" + "If Not IsClientScriptBlockRegistered(\"p_Cerrar\") Then " + "\r\n" + "RegisterClientScriptBlock(\"p_Cerrar\", js) " + "\r\n" + "End If " + "\r\n" + "Catch ex As Exception" + "\r\n" + "ShowErrorMessage(\"Detalle faltante\", ex.Message, ex.Source)" + "\r\n" + "Finally" + "\r\n" + "BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".Dispose()" + "\r\n" + "BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " = Nothing" + "\r\n" + "" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".Dispose()" + "\r\n" + "" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " = Nothing" + "\r\n" + "End Try" + "\r\n";

            tx_Procedimiento_Grabar = tx_cabecera + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".campocodigo =integer.Parse(hdn_cmapocodigo.value.ToString) " + "\r\n" + tx_Procedimiento_Grabar + tx_pie;


            //Cargar Registros
            string tx_cargar_registros = "Dim BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " As New BL.BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " " + "\r\n" + "Dim " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " As New BE.BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "\r\n" + "Try " + "\r\n" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".campocodigo =  Integer.Parse(hdn_cmapocodigo.value.ToString) " + "\r\n" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "= CType(BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".id(" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "), BE.BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ")" + "\r\n" + tx_cargar_registro + "Catch ex As Exception " + "\r\n" + "ShowErrorMessage(\"Detalle faltante\", ex.Message, ex.Source) " + "\r\n" + "Finally" + "\r\n" + "BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".Dispose()" + "\r\n" + "BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " = Nothing" + "\r\n" + "" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".Dispose()" + "\r\n" + "" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " = Nothing" + "\r\n" + " End Try " + "\r\n";
            //Fin Cargar Registros


            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{6}", tx_cargar_registros);
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{8}", tx_Procedimiento_Grabar);

            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{7}", tx_carga_listas);



            FileStream fs = File.Create(tx_path);
            byte[] info = new UTF8Encoding(true).GetBytes(tx_archiv_aspx.ToString());

            fs.Write(info, 0, info.Length);
            fs.Close();

            FileStream fs_vb = File.Create(tx_path_vb);
            byte[] info_vb = new UTF8Encoding(true).GetBytes(tx_archiv_aspx_vb.ToString());

            fs_vb.Write(info_vb, 0, info_vb.Length);
            fs_vb.Close();

            //o_file_vb()

        }


        private void p_WF_GRILLA(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)
                return;
            string TX_VALINT = " onkeypress=\"valNum();\"";
            string TX_TAMANO = " maxlength=\"{0}\" ";
            // TAMAÑO DEL TEXTO

            string tx_css = "texto";


            string tx_imput_text = "<asp:TemplateColumn HeaderText=\"{4}\">" + "\r\n" + "<ItemStyle Width=\"100px\" ></ItemStyle>" + "\r\n" + "<FooterStyle Width=\"100px\"></FooterStyle>" + "\r\n" + "<ItemTemplate>" + "\r\n" + "<%# Container.DataItem(\"{2}\")%>" + "\r\n" + "</ItemTemplate>" + "\r\n" + "<EditItemTemplate>" + "\r\n" + "<input {1}  title=\"{4}\" style=\"WIDTH: 100%\" type=\"text\" runat=\"server\" value='<%# Container.DataItem(\"{2}\")%>' id=\"txt_{2}_e\" class=\"{3}\" />" + "\r\n" + "</EditItemTemplate>" + "\r\n" + "<FooterTemplate>" + "\r\n" + "<input {1} title=\"{4}\" id=\"txt_{2}_n\" style=\"WIDTH: 100%\" class=\"{3}\" type=\"text\" runat=\"server\" />" + "\r\n" + "</FooterTemplate>" + "\r\n" + "</asp:TemplateColumn>" + "\r\n";

            //3=si es nulo     css
            //4=el comentario

            string tx_select = "<asp:TemplateColumn HeaderText=\"{4}\">" + "\r\n" + "<ItemStyle Width=\"100px\" ></ItemStyle>" + "\r\n" + "<FooterStyle Width=\"100px\"></FooterStyle>" + "\r\n" + "<ItemTemplate>" + "\r\n" + "<%# Container.DataItem(\"{2}\")%>" + "\r\n" + "</ItemTemplate>" + "\r\n" + "<EditItemTemplate>" + "\r\n" + "<select title=\"{4}\" runat=\"server\" id=\"cbo_{2}_e\" class=\"{3}\"></select>" + "\r\n" + "</EditItemTemplate>" + "\r\n" + "<FooterTemplate>" + "\r\n" + "<select title=\"{4}\" runat=server id=\"cbo_{2}_n\" class=\"{3}\" ></select>" + "\r\n" + "</FooterTemplate>" + "\r\n" + "</asp:TemplateColumn>";

            string tx_textarea = "<asp:TemplateColumn HeaderText=\"{4}\">" + "\r\n" + "<ItemStyle Width=\"150px\" ></ItemStyle>" + "\r\n" + "<FooterStyle Width=\"150px\"></FooterStyle>" + "\r\n" + "<ItemTemplate>" + "\r\n" + "<%# Container.DataItem(\"{2}\")%>" + "\r\n" + "</ItemTemplate>" + "\r\n" + "<EditItemTemplate>" + "\r\n" + "<textarea title=\"{4}\" runat=\"server\" style=\"WIDTH: 100%; HEIGHT: 25px\" class=\"{3}\" id=\"txt_{2}_e\" ></textarea>" + "\r\n" + "</EditItemTemplate>" + "\r\n" + "<FooterTemplate>" + "\r\n" + "<textarea title=\"{4}\" runat=\"server\" style=\"WIDTH: 100%; HEIGHT: 25px\" class=\"{3}\" id=\"txt_{2}_n\" ></textarea>" + "\r\n" + "</FooterTemplate>" + "\r\n" + "</asp:TemplateColumn>" + "\r\n";

            //File o_file = null;
            //Directory o_dir = null;

            //System.IO.File o_file_vb = null;
            string tx_path = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\WF\\wf_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "_GRD.aspx";
            string tx_path_vb = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\WF\\wf_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "_GRD.aspx.cs";

            string tx_path_dir = Path.GetFullPath(".") + "\\" + TABLA.NoBaseDatos + "\\WF";
            string tx_path_wf = Path.GetFullPath(".") + "\\wf_test.aspx";
            string tx_path_wf_vb = Path.GetFullPath(".") + "\\wf_test.aspx.cs";

            //File e_file_Aspx = null;
            StreamReader e_fs_aspx = File.OpenText(tx_path_wf);
            string tx_archiv_aspx = e_fs_aspx.ReadToEnd();


            //File e_file_vb = null;
            StreamReader e_fs_vb = File.OpenText(tx_path_wf_vb);
            string tx_archiv_aspx_vb = e_fs_vb.ReadToEnd();

            if (Directory.Exists(tx_path_dir) == false)
            {
                Directory.CreateDirectory(tx_path_dir);
            }

            if (File.Exists(tx_path) == true)
            {
                File.Delete(tx_path);
            }
            else
            {
                //o_file.Create(tx_path)
            }

            if (File.Exists(tx_path_vb) == true)
            {
                File.Delete(tx_path_vb);
            }
            else
            {
                //o_file.Create(tx_path)
            }


            string tx_ctrl_textarea_e = TABLA.NOMBRE_SIN_SIGLA_INICIAL + "." + "{1} = {-1}CType(e.Item.FindControl(\"txt_{1}_e\"), System.Web.UI.HtmlControls.HtmlTextArea).Value{0}" + "\r\n";

            string tx_ctrl_textarea_n = TABLA.NOMBRE_SIN_SIGLA_INICIAL + "." + "{1} = {-1}CType(e.Item.FindControl(\"txt_{1}_n\"), System.Web.UI.HtmlControls.HtmlTextArea).Value{0}" + "\r\n";

            string tx_ctrl_select_e = TABLA.NOMBRE_SIN_SIGLA_INICIAL + "." + "{1} = {-1}CType(e.Item.FindControl(\"cbo_{1}_e\"), System.Web.UI.HtmlControls.HtmlSelect).Value{0}" + "\r\n";

            string tx_ctrl_select_n = TABLA.NOMBRE_SIN_SIGLA_INICIAL + "." + "{1} = {-1}CType(e.Item.FindControl(\"cbo_{1}_n\"), System.Web.UI.HtmlControls.HtmlSelect).Value{0}" + "\r\n";

            string tx_ctrl_imput_text_e = TABLA.NOMBRE_SIN_SIGLA_INICIAL + "." + "{1} ={-1}CType(e.Item.FindControl(\"txt_{1}_e\"), System.Web.UI.HtmlControls.HtmlInputText).Value{0}" + "\r\n";

            string tx_ctrl_imput_text_n = TABLA.NOMBRE_SIN_SIGLA_INICIAL + "." + "{1} ={-1} CType(e.Item.FindControl(\"txt_{1}_n\"), System.Web.UI.HtmlControls.HtmlInputText).Value{0}" + "\r\n";

            StringBuilder sb = new StringBuilder();

            string tx_columna_add = null;
            string tx_columna_edit = null;

            string tx_columna_grilla = null;

            int I = 0;

            for (I = 0; I <= TABLA.CAMPO_COL.Count - 1; I++)
            {
                string tx_nombre_columna = TABLA.CAMPO_COL[I].NOMBRE;
                string tx_tipo_dato_columna = TABLA.CAMPO_COL[I].TIPO_DATO_NATIVO;
                string tx_tamano_columna = TABLA.CAMPO_COL[I].TAMANO_CAMPO.ToString();
                string tx_comentario_columna = TABLA.CAMPO_COL[I].COMENTARIO;
                // Dim tx_nulo_columna As String = dt_Est.Rows(I)("nullable").ToString.ToUpper.Trim()


                //If Not f_Verificar_Tipo(tx_tipo_dato_columna) = e_Tipo_Generico_dato.Caracter Then
                //    tx_tamano_columna = ""
                //End If   

                string tx_formato_longitud_campo = "";
                string tx_formato_longitud_campo_add_edit = "";
                //Util.Util.Variables.Dame_Texto(

                if (TABLA.CAMPO_COL[I].TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.CARACTER)
                {
                    tx_formato_longitud_campo = TX_TAMANO.Replace("{0}", tx_tamano_columna);
                    tx_formato_longitud_campo_add_edit = "Util.Util.Variables.Dame_Texto(";


                }
                else if (TABLA.CAMPO_COL[I].TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.NUMERO)
                {
                    tx_formato_longitud_campo = TX_TAMANO.Replace("{0}", "9") + " " + TX_VALINT;
                    tx_formato_longitud_campo_add_edit = "Util.Util.Variables.Dame_Entero(";
                }

                if (string.IsNullOrEmpty(tx_comentario_columna.Trim()))
                {
                    tx_comentario_columna = tx_nombre_columna;
                }               

                if (TABLA.CAMPO_COL[I].NULO == false)
                {
                    tx_css = "requerido";
                }
                else
                {
                    tx_css = "texto";
                }

                if ((tx_nombre_columna.Substring(0, 2).Trim().ToUpper() == "TX") | (tx_nombre_columna.Substring(0, 2).Trim().ToUpper() == "NU"))
                {
                    tx_columna_grilla = tx_columna_grilla + tx_imput_text.Replace("{2}", tx_nombre_columna).Replace("{1}", tx_formato_longitud_campo);
                    tx_columna_grilla = tx_columna_grilla.Replace("{4}", tx_comentario_columna);
                    tx_columna_grilla = tx_columna_grilla.Replace("{3}", tx_css);
                }
                else if ((tx_nombre_columna.Substring(0, 2).Trim().ToUpper() == "ID"))
                {
                    tx_columna_grilla = tx_columna_grilla + tx_select.Replace("{2}", tx_nombre_columna);
                    tx_columna_grilla = tx_columna_grilla.Replace("{4}", tx_comentario_columna);
                    tx_columna_grilla = tx_columna_grilla.Replace("{3}", tx_css);
                }


                //-----------------------------------------------------------------------
                //Agregar despues usuario creacion

                //3=si es nulo    css
                //4=el comentario


                if ((tx_nombre_columna.Substring(0, 2).Trim().ToUpper() == "TX") | (tx_nombre_columna.Substring(0, 2).Trim().ToUpper() == "NU"))
                {
                    tx_columna_add = tx_columna_add + tx_ctrl_imput_text_n.Replace("{1}", tx_nombre_columna).Replace("{-1}", tx_formato_longitud_campo_add_edit).Replace("{0}", ")");

                }
                else if ((tx_nombre_columna.Substring(0, 2).Trim().ToUpper() == "ID"))
                {
                    tx_columna_add = tx_columna_add + tx_ctrl_select_n.Replace("{1}", tx_nombre_columna).Replace("{-1}", tx_formato_longitud_campo_add_edit).Replace("{0}", ")");
                }



                //Agregar despues usuario edicion

                if ((tx_nombre_columna.Substring(0, 2).Trim().ToUpper() == "TX") | (tx_nombre_columna.Substring(0, 2).Trim().ToUpper() == "NU"))
                {
                    tx_columna_edit = tx_columna_edit + tx_ctrl_imput_text_e.Replace("{1}", tx_nombre_columna).Replace("{-1}", tx_formato_longitud_campo_add_edit).Replace("{0}", ")");

                }
                else if ((tx_nombre_columna.Substring(0, 2).Trim().ToUpper() == "ID"))
                {
                    tx_columna_edit = tx_columna_edit + tx_ctrl_select_e.Replace("{1}", tx_nombre_columna).Replace("{-1}", tx_formato_longitud_campo_add_edit).Replace("{0}", ")");

                }



            }

            //tx_columna_add = tx_columna_add & tx_ctrl_select_n.Replace("{1}", Me.txt_usu_cre).Replace("{-1}", "Util.Util.Variables.Dame_Entero(").Replace("{0}", ")")
            tx_columna_add = tx_columna_add + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "." + BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_CREACION).NOMBRE + "=  CType(Session(\"IDUSUARIO\"), Integer)" + "\r\n";
            tx_columna_edit = tx_columna_edit + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "." + BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "=  CType(Session(\"IDUSUARIO\"), Integer)" + "\r\n";


            //tx_nombre_tabla
            tx_archiv_aspx = tx_archiv_aspx.Replace("{1}", txt_nombre_proyecto.Text);
            tx_archiv_aspx = tx_archiv_aspx.Replace("{2}", TABLA.NOMBRE_SIN_SIGLA_INICIAL);
            tx_archiv_aspx = tx_archiv_aspx.Replace("{4}", tx_columna_grilla);

            //archivo vb
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{1}", TABLA.NOMBRE_SIN_SIGLA_INICIAL + "_GRD");
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{11}", this.txt_nombre_proyecto.Text);
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{2}", TABLA.NOMBRE_SIN_SIGLA_INICIAL);
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{13}", TABLA.COMENTARIO);
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{15}", this.txt_autor.Text);
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{16}", System.DateTime.Now.ToString("dd/MM/yyyy"));

            string tx_cabecera = "Dim BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " As New BL.BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " " + "\r\n" + "Dim " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " As New BE.BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " " + "\r\n" + "Try " + "\r\n";

            string tx_pie = "Catch ex As Exception" + "\r\n" + "ShowErrorMessage(\"Detalle faltante\", ex.Message, ex.Source)" + "\r\n" + "Finally" + "\r\n" + "BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " = Nothing" + "\r\n" + "" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " = Nothing" + "\r\n" + "End Try" + "\r\n";


            tx_columna_add = tx_cabecera + tx_columna_add + "BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".Agregar(Nothing, " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ")" + "\r\n" + "p_Cargar_Grid()" + "\r\n" + tx_pie;


            string tx_datakey = TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".codigocampo = Convert.ToInt32(dgr_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".DataKeys(e.Item.ItemIndex))" + "\r\n";

            tx_columna_edit = tx_cabecera + tx_datakey + tx_columna_edit + "BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".Editar(Nothing, " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ")" + "\r\n" + "dgr_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".EditItemIndex = -1" + "\r\n" + "dgr_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".ShowFooter = True" + "\r\n" + "p_Cargar_Grid()" + "\r\n" + tx_pie;

            string tx_eliminar = "Dim BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " As New BL.BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " " + "\r\n" + "Dim " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " As New BE.BE_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " " + "\r\n" + "Try " + "\r\n" + "" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "." + BASE_DATOS.CAMPO_AUDITORIA_COLECCION.X_FINALIDAD(E_FINALIDAD_CAMPO.USUARIO_MODIFICACION).NOMBRE + "=Convert.ToInt32(Session(\"IDUSUARIO\").ToString)" + "\r\n" + "" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".idcampo = Convert.ToInt32(0 & hdn_codigo.Value)" + "\r\n" + " hdn_codigo.Value =\"\" " + "\r\n" + " Dim nu_resultado As Integer = CType(BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".Eliminar(" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "), Integer) " + "\r\n" + " If nu_resultado = 0 Then" + "\r\n" + "pError.InnerText = \"No se puede eliminar el registro porque esta relacionado a otro registro\" " + "\r\n" + " End If " + "\r\n" + " p_Cargar_Grid()" + "\r\n" + "Catch ex As Exception" + "\r\n" + "ShowErrorMessage(ex) " + "\r\n" + "Finally" + "\r\n" + "BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " = Nothing" + "\r\n" + "" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " = Nothing" + "\r\n" + "End Try" + "\r\n";


            string tx_cargar_grid = "Dim BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " As New BL.BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " " + "\r\n" + "Dim " + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " As New Hashtable" + "\r\n" + "Try " + "\r\n" + "Dim dv As DataView = CType(BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".lst(" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "), DataTable).DefaultView " + "\r\n" + " dgr_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".DataSource = dv " + "\r\n" + " dgr_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ".DataBind() " + "\r\n" + "Catch ex As Exception " + "\r\n" + "ShowErrorMessage(ex) " + "\r\n" + "Finally" + "\r\n" + "BL_" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " = Nothing" + "\r\n" + "" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " = Nothing" + "\r\n" + " End Try " + "\r\n";


            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{6}", tx_cargar_grid);
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{7}", tx_eliminar);
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{8}", tx_columna_add);
            tx_archiv_aspx_vb = tx_archiv_aspx_vb.Replace("{9}", tx_columna_edit);




            FileStream fs = File.Create(tx_path);
            byte[] info = new UTF8Encoding(true).GetBytes(tx_archiv_aspx.ToString());

            fs.Write(info, 0, info.Length);
            fs.Close();

            FileStream fs_vb = File.Create(tx_path_vb);
            byte[] info_vb = new UTF8Encoding(true).GetBytes(tx_archiv_aspx_vb.ToString());

            fs_vb.Write(info_vb, 0, info_vb.Length);
            fs_vb.Close();
            //tx_path_wf
            //o_file_vb()

        }

        private void btnIngresa_Click(object sender, EventArgs e)
        {
            BD_GENERICO.TIPO_BASE_DATOS = (E_TIPO_BD)cbo_base_datos.SelectedIndex;
            BASE_DATOS.NoBaseDatos = this.txt_base_datos.Text;
            BASE_DATOS.CLAVE = this.txt_clave.Text;
            BASE_DATOS.SERVIDOR = this.txt_servidor.Text;
            BASE_DATOS.USUARIO = this.txt_usuario.Text;

            BASE_DATOS.AUTOR = this.txt_autor.Text;

            if (this.cbo_base_datos.SelectedIndex == 0)
            {
                BASE_DATOS.NoEsquema = this.txt_usuario.Text;
                BASE_DATOS.NoBaseDatos = BASE_DATOS.NoEsquema;
            }
            else if (this.cbo_base_datos.SelectedIndex == 1)
            {
                BASE_DATOS.NoEsquema = this.txt_base_datos.Text;
            }


            BASE_DATOS.CAMPO_AUDITORIA_COLECCION = f_Campos_Auditoria();
            //BASE_DATOS.CSS_COLECCION = f_CSS()

            FL_NORMAL = false;

            List<BE_TABLA> TABLA_COL = BD_GENERICO.TABLAS(BASE_DATOS);
            foreach (BE_TABLA TABLA in TABLA_COL)
            {
                TABLA.BASE_DATOS = BASE_DATOS;
                TABLA.QUITAR_N_CARACTERES_INICIALES = Convert.ToInt32(this.txt_qt_caracter_invalido_tabla.Text);
                TABLA.QUITAR_N_CARACTERES_INICIALES_CAMPO = Convert.ToInt32(this.txt_n_caracter_inicial_campo.Text);
                TABLA.CAMPO_COL = BD_GENERICO.CAMPOS(TABLA);
                TABLA.INDICE_COL = BD_GENERICO.INDICE(TABLA);
            }
            TABLA_COL.Sort(new Sorter<BE_TABLA>("NombrePreliminar"));
            lst_tabla.DataSource = TABLA_COL;
            lst_tabla.DisplayMember = "NombrePreliminar";
            lst_tabla.Refresh();
            FL_NORMAL = true;


            //PROCEDIMIENTOS_COL = BD_GENERICO.PROCEDIMIENTOS(BASE_DATOS);
        }

        private void cbo_base_datos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_base_datos.SelectedIndex == 0)
            {
                this.lbl_base_Datos.Visible = false;
                this.txt_base_datos.Visible = false;
                lblEsquemaOracle.Text = "Esquema";
            }
            else if (cbo_base_datos.SelectedIndex == 1)
            {
                this.lbl_base_Datos.Visible = true;
                this.txt_base_datos.Visible = true;
                lblEsquemaOracle.Text = "Usuario";
            }
        }
    }
}
