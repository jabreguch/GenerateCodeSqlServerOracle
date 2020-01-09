using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http;
namespace Logistica.Areas.Maestros.Controllers.api {
public class ParametrosApiController : BaseApiController {
[HttpPost]
 public IHttpActionResult Graba([FromBody] BeParametros c) {
BlParametros b = new BlParametros();
try {
if (ModeloValido) {
if (c.CoEstadoPagina == Enu.CoEstadoPagina.Nuevo) {
c.NoCampo = b.Add(c);
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
public IHttpActionResult Del([FromBody] BeParametros c) {
BlParametros b = new BlParametros();
try {
b.Del(c);
return Json(new { data = c, success = true, Message = CO_Constante.msgExito("Se eliminó el registro") });
}
catch (Exception ex) { throw ex; }
finally { b.Dispose(); b = null; }
}

[HttpPost]
public IHttpActionResult EditEst([FromBody] BeParametros c) {
BlParametros b = new BlParametros();
try {
b.EditEst(c);
return Json(new { data = c, success = true, Message = CO_Constante.msgExito("Se actualizo el registro") });
}
catch (Exception ex) { throw ex; }
finally { b.Dispose(); b = null; }
}

}
}
