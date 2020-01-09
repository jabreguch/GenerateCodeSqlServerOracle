using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace Logistica.Areas.Condominio.Controllers {
public class PeriodoController : BaseController {
public ActionResult Index(BePeriodo c) {
return View(c);
}

public PartialViewResult Get(BePeriodo c) {
BlPeriodo b = new BlPeriodo();
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

public PartialViewResult Gets(BePeriodo c) {
BlPeriodo b = new BlPeriodo();
try {
List<BePeriodo> r = new List<BePeriodo>(); 
r = b.Gets(c); 
return PartialView(r.Paginar<BePeriodo>(c.NUPAGINAACTUAL));
}
catch (Exception ex) { throw ex; }
finally {
b.Dispose(); b = null;
}
}

}
}
