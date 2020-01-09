using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace Logistica.Areas.Condominio.Controllers {
public class DepartamentoinquilinoController : BaseController {
public ActionResult Index(BeDepartamentoInquilino c) {
return View(c);
}

public PartialViewResult Get(BeDepartamentoInquilino c) {
BlDepartamentoInquilino b = new BlDepartamentoInquilino();
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

public PartialViewResult Gets(BeDepartamentoInquilino c) {
BlDepartamentoInquilino b = new BlDepartamentoInquilino();
try {
List<BeDepartamentoInquilino> r = new List<BeDepartamentoInquilino>(); 
r = b.Gets(c); 
return PartialView(r.Paginar<BeDepartamentoInquilino>(c.NUPAGINAACTUAL));
}
catch (Exception ex) { throw ex; }
finally {
b.Dispose(); b = null;
}
}

}
}
