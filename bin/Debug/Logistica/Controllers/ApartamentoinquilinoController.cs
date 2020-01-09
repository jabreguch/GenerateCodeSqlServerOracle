using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace Logistica.Areas.Condominio.Controllers {
public class ApartamentoinquilinoController : BaseController {
public ActionResult Index(BeApartamentoInquilino c) {
return View(c);
}

public PartialViewResult Get(BeApartamentoInquilino c) {
BlApartamentoInquilino b = new BlApartamentoInquilino();
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

public PartialViewResult Gets(BeApartamentoInquilino c) {
BlApartamentoInquilino b = new BlApartamentoInquilino();
try {
List<BeApartamentoInquilino> r = new List<BeApartamentoInquilino>(); 
r = b.Gets(c); 
return PartialView(r.Paginar<BeApartamentoInquilino>(c.NUPAGINAACTUAL));
}
catch (Exception ex) { throw ex; }
finally {
b.Dispose(); b = null;
}
}

}
}
