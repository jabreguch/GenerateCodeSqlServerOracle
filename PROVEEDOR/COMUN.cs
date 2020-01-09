using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneraCodigo
{
    static class COMUN
    {

        public static StringBuilder HtmlIndex(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)
                return null;
           
            StringBuilder sb = new StringBuilder();

            sb.Append("@model Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "\r\n");
            sb.Append("@{\r\n");
            sb.Append("ViewBag.Title = \"Titulo de la página\";\r\n");
            sb.Append("Layout = \"~/Areas/" + TABLA.Area + "/Views/Shared/_Layout.cshtml\";\r\n");
            sb.Append("}\r\n\r\n");


            sb.Append("<div class='content-header row'>\r\n");
            sb.Append("<div class='content-header-left col-md-6 col-12 mb-1' >\r\n");
            sb.Append("<h3 class='content-header-title' > Titulo</h3>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("<div class='content-header-right breadcrumbs-right breadcrumbs-top col-md-6 col-12' >\r\n");
            sb.Append("<div class='breadcrumb-wrapper col-12' >\r\n");
            sb.Append("<ol class='breadcrumb'>\r\n");
            sb.Append("<li class='breadcrumb-item' >\r\n");
            sb.Append("<a data-target='#WinComun' id='btnNuevo" + TABLA.NombreControlador + "' data-codigo='{\"CoEstadoPagina\": @Enu.CoEstadoPagina.Nuevo }'>Nuevo</a>\r\n");
            sb.Append("</li>\r\n");
            sb.Append("</ol>\r\n");
            sb.Append("</div>\r\n");
            sb.Append(" </div>\r\n");
            sb.Append("</div>\r\n");

            sb.Append("<section class='card' >\r\n");
            sb.Append("<div class='card-header' >\r\n");
            sb.Append("<h4 class='card-title' > Subtitulo</h4>\r\n");
            sb.Append("<a class='heading-elements-toggle' ><i class='fa fa-ellipsis-v font-medium-3' ></i></a>\r\n");
            sb.Append("<div class='heading-elements'>\r\n");
            sb.Append("<ul class='list-inline mb-0'>\r\n");
            sb.Append("<li><a data-action='expand'><i class=vft -maximize'></i></a></li>\r\n");
            sb.Append("</ul>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("<div class='card-content' >\r\n");
            sb.Append("<div class='card-body' >\r\n");
            sb.Append("<form class='form form-horizontal' >\r\n");
            sb.Append("<div class='form-body' >\r\n");

            sb.Append("</div>\r\n");
            sb.Append("<div id='div" + TABLA.NombreControlador + "' class='dataTables_wrapper dt-bootstrap4 no-footer'>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("</form>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("</section>\r\n\r\n");



            sb.Append("<script type=\"text/javascript\">\r\n");

            sb.Append("function Gets" + TABLA.NombreControlador + "(e, criterio) {\r\n");
            sb.Append("var Filter = {};\r\n");
            sb.Append("criterio = $.extend({}, criterio, Filter);\r\n");
            sb.Append("$('#div" + TABLA.NombreControlador + "').empty();\r\n");
            sb.Append("$.ajax({\r\n");
            sb.Append("url: '@Url.Action(\"Gets\", \"" + TABLA.NombreControlador + "\", new { area= \"" + TABLA.Area + "\" })',\r\n");
            sb.Append("type: 'GET',\r\n");
            sb.Append("cache: false,\r\n");
            sb.Append("async: true,\r\n");
            sb.Append("data: criterio,\r\n");
            sb.Append("success: function (data) {\r\n");
            sb.Append("$('#div" + TABLA.NombreControlador + "').html(data);\r\n");
            sb.Append("},\r\n");
            sb.Append("complete: function () {\r\n");
            sb.Append("$('#tbl" + TABLA.NombreControlador + "').tablaFuncional({ funcion: 'Gets" + TABLA.NombreControlador + "', data: criterio });\r\n");
            sb.Append("$('[data-toggle=\"elimina-" + TABLA.NombreControlador + "\"]').confirmar({ funcion: 'ConfirmaElimina" + TABLA.NombreControlador + "' });\r\n");
            sb.Append("$('[data-toggle=\"activa-" + TABLA.NombreControlador + "\"]').confirmar({ funcion: 'ConfirmaActiva" + TABLA.NombreControlador + "' });\r\n");
            sb.Append("$('.edita-" + TABLA.NombreControlador + "').click(Get" + TABLA.NombreControlador + ");\r\n");
            sb.Append("}\r\n");
            sb.Append("});\r\n");
            sb.Append("}\r\n");
            sb.Append("$(function () {\r\n");
            sb.Append("$('#btnNuevo" + TABLA.NombreControlador + "').click(Get" + TABLA.NombreControlador + ");\r\n");
            sb.Append("Gets" + TABLA.NombreControlador + "();\r\n");
            sb.Append("});\r\n\r\n");

            sb.Append("Get" + TABLA.NombreControlador + " = function () {\r\n");
            sb.Append("var codigo = this.codigo;\r\n");
            sb.Append("if (codigo == null || codigo == undefined) { codigo = $(this).data('codigo'); }\r\n");
            sb.Append("$.ajax({\r\n");
            sb.Append("url: '@Url.Action(\"Get\", \"" + TABLA.NombreControlador + "\", new { area = \"" + TABLA.Area + "\" } )',\r\n");
            sb.Append("type: 'Get',\r\n");
            sb.Append("cache: false,\r\n");
            sb.Append("data: codigo,\r\n");
            sb.Append("success: function (data) {\r\n");
            sb.Append("DefaultModal(data, { Titulo: 'Titulo'});\r\n");
            sb.Append("} \r\n");
            sb.Append("});\r\n");
            sb.Append("}\r\n\r\n");

            sb.Append(" function ConfirmaElimina" + TABLA.NombreControlador + "(codigo) {\r\n");
            sb.Append("$.ajax({\r\n");
            sb.Append("url: '@Url.Action(\"Del\", \"api/" + TABLA.NombreControlador + "Api\", new { area= \"" + TABLA.Area + "\" })',\r\n");
            sb.Append("dataType: 'json',\r\n");
            sb.Append("type: 'POST',\r\n");
            sb.Append("cache: false,\r\n");
            sb.Append("data: codigo,\r\n");
            sb.Append("success: function (d) {\r\n");
            sb.Append("Gets" + TABLA.NombreControlador + "();\r\n");
            sb.Append("},\r\n");
            sb.Append("});\r\n");
            sb.Append(" }\r\n");


            sb.Append("function ConfirmaActiva" + TABLA.NombreControlador + "(codigo) {\r\n");
            sb.Append("$.ajax({\r\n");
            sb.Append("url: '@Url.Action(\"EditEstReg\", \"api/ " + TABLA.NombreControlador + "Api\", new { area= \"" + TABLA.Area + "\"})',\r\n");
            sb.Append("dataType: 'json',\r\n");
            sb.Append("type: 'POST',\r\n");
            sb.Append("cache: false,\r\n");
            sb.Append("data: codigo,\r\n");
            sb.Append("success: function (d) {\r\n");
            sb.Append("Gets" + TABLA.NombreControlador + "();\r\n");
            sb.Append("},\r\n");
            sb.Append("});\r\n");
            sb.Append("}\r\n");

            sb.Append("</script>\r\n");




            return sb;
        }


        public static StringBuilder HtmlGet(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0) return null;

            StringBuilder sb = new StringBuilder();

            sb.Append("@model Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "\r\n");
            sb.Append("@{ Layout = null;}\r\n");
            sb.Append("<form class='form form-horizontal form-bordered' id ='frm" + TABLA.NombreControlador + "' >\r\n");
            sb.Append("<div class='form-body'>\r\n");

            for (int i = 0; i <= TABLA.CAMPO_COL.Count - 1; i++)
            {

                if (TABLA.CAMPO_COL[i].PRIMARY_KEY == true)
                {
                    sb.Append("@Html.hidden(x => x." + TABLA.CAMPO_COL[i].NOMBRE + ")\r\n");

                }
                else
                {
                    sb.Append("<div class='form-group row'>\r\n");
                    sb.Append("<label class='col-md-3 label-control'  for='" + TABLA.CAMPO_COL[i].NOMBRE + "' >" + TABLA.CAMPO_COL[i].NOMBRE_SIN_SIGLA_INICIAL.NombresAltasYBajas() + "</label>");
                    sb.Append("<div class=\"col-md-9\">\r\n");
                    if (TABLA.CAMPO_COL[i].TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.NUMERO | TABLA.CAMPO_COL[i].TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.FLOAT | TABLA.CAMPO_COL[i].TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.DECIMAL_)
                    {
                        sb.Append("@Html.number(x => x." + TABLA.CAMPO_COL[i].NOMBRE + ", new { @class = \"form-control\" }, null, false, true)\r\n");
                    }
                    else if (TABLA.CAMPO_COL[i].TIPO_DATO_GENERICO == E_TIPO_DATO_GENERICO.CARACTER)
                    {
                        if (TABLA.CAMPO_COL[i].TAMANO_CAMPO <= 200)
                        {
                            sb.Append("@Html.text(x => x." + TABLA.CAMPO_COL[i].NOMBRE + ", new { @class = \"form-control\", @maxlength = \"" + TABLA.CAMPO_COL[i].TAMANO_CAMPO + "\" }, null, false, true)\r\n");
                        }
                        else
                        {
                            sb.Append("@Html.textarea(x => x." + TABLA.CAMPO_COL[i].NOMBRE + ", new { @class = \"form-control\", @maxlength = \"" + TABLA.CAMPO_COL[i].TAMANO_CAMPO + "\", @rows = \"6\" })\r\n");
                            //@Html.textarea(x => x.Observaciones, new { @class = "form-control", @rows = "6" })
                        }


                    }
                    sb.Append("</div>\r\n");
                    sb.Append("</div>\r\n");

                }

            }
            sb.Append("</div>\r\n");
            sb.Append("@Html.hidden(x => x.CoEstadoPagina)\r\n");
            sb.Append("</form>\r\n");




            sb.Append("<script type=\"text/javascript\">\r\n");
            sb.Append("$(function () {\r\n");
            sb.Append("$('#frm" + TABLA.NombreControlador + "').validate(DefaultOptionValidate);\r\n");
            sb.Append("$('#WinComun button[id=btnGraba]').unbind('click').on('click', Graba" + TABLA.NombreControlador + ");\r\n");
            sb.Append(" });\r\n\r\n");


            sb.Append("Graba" + TABLA.NombreControlador + " = function () {\r\n");
            sb.Append("if ($('#frm" + TABLA.NombreControlador + "').valid()) {\r\n");
            sb.Append("var data = FormToJson(document.getElementById('frm" + TABLA.NombreControlador + "'));\r\n");
            sb.Append("$.ajax({\r\n");
            sb.Append("url: '@Url.Action(\"Graba\",\"api/" + TABLA.NombreControlador + "Api\", new { area= \"" + TABLA.Area + "\" })',\r\n");
            sb.Append("dataType: 'json',\r\n");
            sb.Append("type: 'POST',\r\n");
            sb.Append("cache: false,\r\n");
            sb.Append("data: data,\r\n");
            sb.Append("success: function (d) {\r\n");
            sb.Append("if (d.success == true) {\r\n");
            sb.Append("var item = d.data;\r\n");
            sb.Append("Get" + TABLA.NombreControlador + ".call({ 'codigo': item });\r\n");
            sb.Append("Gets" + TABLA.NombreControlador + "();\r\n");
            sb.Append("}\r\n");
            sb.Append("}\r\n");
            sb.Append(" });\r\n");

            sb.Append("}\r\n");
            sb.Append("}\r\n");

            sb.Append("</script>\r\n");


            return sb;
        }
        public static System.Text.StringBuilder HtmlGets(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)
                return null;
            int I = 0;

            StringBuilder sb = new StringBuilder();

            sb.Append("@model IEnumerable<Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ">\r\n");
            sb.Append("@{Layout = null;}\r\n");

            sb.Append("@if (Model.Count() > 0)\r\n");
            sb.Append(" {\r\n");
            sb.Append("<div class='row'>\r\n");
            sb.Append("<div class='col-md-12 col-sm-12'>\r\n");
            sb.Append("<table id='tbl" + TABLA.NombreControlador + "' class='table table-bordered mb-0'>\r\n");
            sb.Append("<thead class='thead-default'>\r\n");
            sb.Append("<tr>\r\n");
            //sb.Append("<th>ID</th>\r\n");

            //            <th>Modulo</th>
            //            <th>Area</th>
            //            <th>Opciones</th>
            //            <th></th>
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>\r\n");
            sb.Append("@foreach (Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " item in Model) {\r\n");
            sb.Append("<tr>\r\n");


            for (int i = 0; i <= TABLA.CAMPO_COL.Count - 1; i++)
            {

                if (TABLA.CAMPO_COL[i].PRIMARY_KEY == true)
                {
                    sb.Append("<td>\r\n");
                    sb.Append(" <button type='button' title='Editar' class='btn btn-icon btn-pure success edita-" + TABLA.NombreControlador.ToLower() + "' data-target='#WinComun' data-codigo='{ \"" + TABLA.CAMPO_COL[i].NOMBRE + "\": @item." + TABLA.CAMPO_COL[i].NOMBRE + ",  \"CoEstadoPagina\": \"@Enu.CoEstadoPagina.Edicion\"}'><i class='fa fa-pencil'></i></button>");
                    sb.Append("</td>\r\n");

                    sb.Append("<td>\r\n");
                    sb.Append("<button type='button' data-codigo='{\"" + TABLA.CAMPO_COL[i].NOMBRE + "\": @item." + TABLA.CAMPO_COL[i].NOMBRE + ", \"CO_EST_REG\": @(item.CO_EST_REG == 1 ? 0 : 1)  }' title=@(item.CO_EST_REG == 1 ? \"¿Desactivar?\": \"¿Activar ?\") class='btn btn-icon btn-pure success' data-toggle='activa-" + TABLA.NombreControlador.ToLower() + "' data-placement='top'>\r\n");
                    sb.Append("<i class='@(item.CO_EST_REG == 1 ? \"fa fa-check\" : \"fa fa-thumbs-o-down\")'></i>\r\n");
                    sb.Append("</button>\r\n");
                    sb.Append("</td>\r\n");


                    sb.Append("<td>\r\n");
                    sb.Append("<button type='button' data-codigo='{\"" + TABLA.CAMPO_COL[i].NOMBRE + "\": @item." + TABLA.CAMPO_COL[i].NOMBRE + "}' title='¿Eliminar?' class='btn btn-icon btn-pure danger' data-toggle='elimina-" + TABLA.NombreControlador.ToLower() + "' data-placement='top'>\r\n");
                    sb.Append("<i class='fa fa-trash-o'></i>\r\n");
                    sb.Append("</button>\r\n");
                    sb.Append("</td>\r\n");

                }
                else
                {

                    sb.Append("<td>\r\n");
                    sb.Append("@item." + TABLA.CAMPO_COL[i].NOMBRE);
                    sb.Append("</td>\r\n");

                }

            }



            sb.Append("</tr>\r\n");
            sb.Append("}\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("}\r\n");
            sb.Append("@Glo.Paginador()\r\n");





            return sb;
        }


        public static StringBuilder Controlador(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)
                return null;
            int I = 0;

            StringBuilder sb = new StringBuilder();

            sb.Append("using System;\r\n");
            sb.Append("using System.Web;\r\n");
            sb.Append("using System.Collections.Generic;\r\n");
            sb.Append("using System.Linq;\r\n");
            sb.Append("using System.Web.Mvc;\r\n");
            sb.Append("namespace " + TABLA.NoBaseDatos + ".Areas." + TABLA.Area + ".Controllers {\r\n");
            sb.Append("public class " + TABLA.NombreControlador + "Controller : BaseController {\r\n");


            sb.Append("public ActionResult Index(Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n");
            sb.Append("return View(c);\r\n");
            sb.Append("}\r\n\r\n");


            sb.Append("public PartialViewResult Get(Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n");
            sb.Append("Bl" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " b = new Bl" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "();\r\n");
            sb.Append("try {\r\n");
            sb.Append("if (c.CoEstadoPagina == Enu.CoEstadoPagina.Nuevo) {\r\n");
            sb.Append("}\r\n");
            sb.Append("else if (c.CoEstadoPagina == Enu.CoEstadoPagina.Edicion) {\r\n");
            sb.Append("c = b.Get(c);\r\n");
            sb.Append("c.CoEstadoPagina = @Enu.CoEstadoPagina.Edicion;\r\n");
            sb.Append("}\r\n");
            sb.Append(" return PartialView(c);\r\n");
            sb.Append("}\r\n");
            sb.Append("catch (Exception ex) { throw ex; }\r\n");
            sb.Append("finally {\r\n");
            sb.Append("b.Dispose(); b = null;\r\n");
            sb.Append("}\r\n");
            sb.Append("}\r\n\r\n");

            
            sb.Append("public PartialViewResult Gets(Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n");
            sb.Append("Bl" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " b = new Bl" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "();\r\n");
            sb.Append("try {\r\n");
            sb.Append("List<Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "> r = new List<Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ">(); \r\n");
            sb.Append("r = b.Gets(c); \r\n");
            sb.Append("return PartialView(r.Paginar<Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + ">(c.NUPAGINAACTUAL));\r\n");                     
            sb.Append("}\r\n");
            sb.Append("catch (Exception ex) { throw ex; }\r\n");
            sb.Append("finally {\r\n");
            sb.Append("b.Dispose(); b = null;\r\n");
            sb.Append("}\r\n");
            sb.Append("}\r\n\r\n");

            sb.Append("}\r\n");
            sb.Append("}\r\n");
            return sb;
        }


        public static StringBuilder ControladorApi(BE_TABLA TABLA)
        {
            if (TABLA.CAMPO_COL.Count == 0)    return null;
          

            StringBuilder sb = new StringBuilder();

            sb.Append("using System;\r\n");
            sb.Append("using System.Linq;\r\n");
            sb.Append("using System.Collections.Generic;\r\n");
            sb.Append("using System.Net.Http;\r\n");
            sb.Append("using System.Net;\r\n");
            sb.Append("using System.Web.Http;\r\n");


            sb.Append("namespace " + TABLA.NoBaseDatos + ".Areas." + TABLA.Area + ".Controllers.api {\r\n");
            sb.Append("public class " + TABLA.NombreControlador + "ApiController : BaseApiController {\r\n");


            sb.Append("[HttpPost]\r\n");
            sb.Append(" public IHttpActionResult Graba([FromBody] Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n");
            sb.Append("Bl" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " b = new Bl" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "();\r\n");
            sb.Append("try {\r\n");
            sb.Append("if (ModeloValido) {\r\n");
            sb.Append("if (c.CoEstadoPagina == Enu.CoEstadoPagina.Nuevo) {\r\n");
            BE_CAMPO tmp = TABLA.CAMPO_COL.Find(x => x.PRIMARY_KEY == true);
            if (tmp != null)
            {
                sb.Append("c." + tmp.NOMBRE + " = b.Add(c);\r\n");
            }
            else
            {
                sb.Append("b.Add(c);\r\n");
            }

            sb.Append("c.CoEstadoPagina = Enu.CoEstadoPagina.Edicion;\r\n");
            sb.Append("}\r\n");
            sb.Append("else if (c.CoEstadoPagina == Enu.CoEstadoPagina.Edicion) {\r\n");
            sb.Append("b.Edit(c);\r\n");
            sb.Append("c.CoEstadoPagina = Enu.CoEstadoPagina.Edicion;\r\n");
            sb.Append("}\r\n");
            sb.Append("return Json(new  {\r\n");
            sb.Append("data = c, success = true, Message = CO_Constante.msgExito(\"Se grabo el registro\")\r\n");
            sb.Append("});\r\n");
            sb.Append("}\r\n");
            sb.Append("else {\r\n");
            sb.Append("return Json(new {\r\n");
            sb.Append("data = c, success = false, Message = Mensajee()\r\n");
            sb.Append("});\r\n");
            sb.Append("}\r\n");
            sb.Append("}\r\n");
            sb.Append("catch (Exception ex) {  throw ex; }\r\n");
            sb.Append("finally {\r\n");
            sb.Append("b.Dispose(); b = null;\r\n");
            sb.Append("}\r\n");
            sb.Append("}\r\n\r\n");



            sb.Append("[HttpPost]\r\n");
            sb.Append("public IHttpActionResult Del([FromBody] Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n");
            sb.Append("Bl" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " b = new Bl" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "();\r\n");
            sb.Append("try {\r\n");
            sb.Append("b.Del(c);\r\n");
            sb.Append("return Json(new { data = c, success = true, Message = CO_Constante.msgExito(\"Se eliminó el registro\") });\r\n");
            sb.Append("}\r\n");
            sb.Append("catch (Exception ex) { throw ex; }\r\n");
            sb.Append("finally { b.Dispose(); b = null; }\r\n");
            sb.Append("}\r\n\r\n");


            sb.Append("[HttpPost]\r\n");
            sb.Append("public IHttpActionResult EditEst([FromBody] Be" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " c) {\r\n");
            sb.Append("Bl" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + " b = new Bl" + TABLA.NOMBRE_SIN_SIGLA_INICIAL + "();\r\n");
            sb.Append("try {\r\n");
            sb.Append("b.EditEst(c);\r\n");
            sb.Append("return Json(new { data = c, success = true, Message = CO_Constante.msgExito(\"Se actualizo el registro\") });\r\n");
            sb.Append("}\r\n");
            sb.Append("catch (Exception ex) { throw ex; }\r\n");
            sb.Append("finally { b.Dispose(); b = null; }\r\n");
            sb.Append("}\r\n\r\n");



            sb.Append("}\r\n");
            sb.Append("}\r\n");
            return sb;
        }



    }
}
