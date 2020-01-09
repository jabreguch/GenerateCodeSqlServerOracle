using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http;
namespace ORGPOLV2.Areas.Maestros.Controllers.api {
public class TrftipoApiController : BaseApiController {
[HttpPost]
 public IHttpActionResult Graba([FromBody] BeTRF_TIPO c) {
BlTRF_TIPO b = new BlTRF_TIPO();
try {
if (ModeloValido) {
if (c.CoEstadoPagina == Enu.CoEstadoPagina.Nuevo) {
c.IDGRUPO = b.Add(c);
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
public IHttpActionResult Del([FromBody] BeTRF_TIPO c) {
BlTRF_TIPO b = new BlTRF_TIPO();
try {
b.Del(c);
return Json(new { data = c, success = true, Message = CO_Constante.msgExito("Se eliminó el registro") });
}
catch (Exception ex) { throw ex; }
finally { b.Dispose(); b = null; }
}

[HttpPost]
public IHttpActionResult EditEst([FromBody] BeTRF_TIPO c) {
BlTRF_TIPO b = new BlTRF_TIPO();
try {
b.EditEst(c);
return Json(new { data = c, success = true, Message = CO_Constante.msgExito("Se actualizo el registro") });
}
catch (Exception ex) { throw ex; }
finally { b.Dispose(); b = null; }
}

}
}
