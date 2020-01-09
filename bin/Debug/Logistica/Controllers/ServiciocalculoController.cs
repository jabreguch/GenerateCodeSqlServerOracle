using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace Logistica.Areas.Condominio.Controllers {
public class ServiciocalculoController : BaseController {
public ActionResult Index(BeServicioCalculo c) {
return View(c);
}

public PartialViewResult Get(BeServicioCalculo c) {
BlServicioCalculo b = new BlServicioCalculo();
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

public PartialViewResult Gets(BeServicioCalculo c) {
BlServicioCalculo b = new BlServicioCalculo();
try {
List<BeServicioCalculo> r = new List<BeServicioCalculo>(); 
r = b.Gets(c); 
return PartialView(r.Paginar<BeServicioCalculo>(c.NUPAGINAACTUAL));
}
catch (Exception ex) { throw ex; }
finally {
b.Dispose(); b = null;
}
}

}
}
