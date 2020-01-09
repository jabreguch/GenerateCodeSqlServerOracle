using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace ORGPOLV2.Areas.Maestros.Controllers {
public class TrftipoController : BaseController {
public ActionResult Index(BeTRF_TIPO c) {
return View(c);
}

public PartialViewResult Get(BeTRF_TIPO c) {
BlTRF_TIPO b = new BlTRF_TIPO();
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

public PartialViewResult Gets(BeTRF_TIPO c) {
BlTRF_TIPO b = new BlTRF_TIPO();
try {
List<BeTRF_TIPO> r = new List<BeTRF_TIPO>(); 
r = b.Gets(c); 
return PartialView(r.Paginar<BeTRF_TIPO>(c.NUPAGINAACTUAL));
}
catch (Exception ex) { throw ex; }
finally {
b.Dispose(); b = null;
}
}

}
}
