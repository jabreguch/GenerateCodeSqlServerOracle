using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace orgpolv2.Areas.Seguridad.Controllers {
public class UsersController : BaseController {
public ActionResult Index(Be_USERS c) {
return View(c);
}

public PartialViewResult Get(Be_USERS c) {
Bl_USERS b = new Bl_USERS();
try {
if (c.CoEstadoPagina == Enu.CoEstadoPagina.Nuevo) {
}
else if (c.CoEstadoPagina == Enu.CoEstadoPagina.Edicion) {
c = b.Get(c);
c.CoEstadoPagina = @Enu.CoEstadoPagina.Edicion;
}
 return PartialView(c);
}
catch (Exception ex) { throw ex; }
finally {
b.Dispose(); b = null;
}
}

public PartialViewResult Gets(Be_USERS c) {
Bl_USERS b = new Bl_USERS();
try {
List<Be_USERS> r = new List<Be_USERS>(); 
r = b.Gets(c); 
return PartialView(r.Paginar<Be_USERS>(c.NUPAGINAACTUAL));
}
catch (Exception ex) { throw ex; }
finally {
b.Dispose(); b = null;
}
}

}
}
