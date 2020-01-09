using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http;
namespace Logistica.Areas.Condominio.Controllers.api {
public class DepartamentoinquilinoApiController : BaseApiController {
[HttpPost]
 public IHttpActionResult Graba([FromBody] BeDepartamentoInquilino c) {
BlDepartamentoInquilino b = new BlDepartamentoInquilino();
try {
if (ModeloValido) {
if (c.CoEstadoPagina == Enu.CoEstadoPagina.Nuevo) {
c.CoCondominio = b.Add(c);
c.CoEstadoPagina = Enu.CoEstadoPagina.Edicion;
}
else if (c.CoEstadoPagina == Enu.CoEstadoPagina.Edicion) {
b.Edit(c);
c.CoEstadoPagina = Enu.CoEstadoPagina.Edicion;
}
return Json(new  {
data = c, success = true, Message = CO_Constante.msgExito("Se grabo el registro")
});
}
else {
return Json(new {
data = c, success = false, Message = Mensajee()
});
}
}
catch (Exception ex) {  throw ex; }
finally {
b.Dispose(); b = null;
}
}

[HttpPost]
public IHttpActionResult Del([FromBody] BeDepartamentoInquilino c) {
BlDepartamentoInquilino b = new BlDepartamentoInquilino();
try {
b.Del(c);
return Json(new { data = c, success = true, Message = CO_Constante.msgExito("Se eliminó el registro") });
}
catch (Exception ex) { throw ex; }
finally { b.Dispose(); b = null; }
}

[HttpPost]
public IHttpActionResult EditEst([FromBody] BeDepartamentoInquilino c) {
BlDepartamentoInquilino b = new BlDepartamentoInquilino();
try {
b.EditEst(c);
return Json(new { data = c, success = true, Message = CO_Constante.msgExito("Se actualizo el registro") });
}
catch (Exception ex) { throw ex; }
finally { b.Dispose(); b = null; }
}

}
}
