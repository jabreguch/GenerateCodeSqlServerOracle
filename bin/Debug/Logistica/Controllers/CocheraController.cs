using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace Logistica.Areas.Condominio.Controllers {
public class CocheraController : BaseController {
public ActionResult Index(BeCochera c) {
return View(c);
}

public PartialViewResult Get(BeCochera c) {
BlCochera b = new BlCochera();
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

public PartialViewResult Gets(BeCochera c) {
BlCochera b = new BlCochera();
try {
List<BeCochera> r = new List<BeCochera>(); 
r = b.Gets(c); 
return PartialView(r.Paginar<BeCochera>(c.NUPAGINAACTUAL));
}
catch (Exception ex) { throw ex; }
finally {
b.Dispose(); b = null;
}
}

}
}
