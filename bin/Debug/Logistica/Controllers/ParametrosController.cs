using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace Logistica.Areas.Maestros.Controllers {
public class ParametrosController : BaseController {
public ActionResult Index(BeParametros c) {
return View(c);
}

public PartialViewResult Get(BeParametros c) {
BlParametros b = new BlParametros();
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

public PartialViewResult Gets(BeParametros c) {
BlParametros b = new BlParametros();
try {
List<BeParametros> r = new List<BeParametros>(); 
r = b.Gets(c); 
return PartialView(r.Paginar<BeParametros>(c.NUPAGINAACTUAL));
}
catch (Exception ex) { throw ex; }
finally {
b.Dispose(); b = null;
}
}

}
}
