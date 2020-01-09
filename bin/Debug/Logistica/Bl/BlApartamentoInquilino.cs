using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System;
using  Microsoft.AspNetCore.Mvc.Rendering;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	19-11-2018	Created
///</history>
public class BlApartamentoInquilino: BlBase { 
DaApartamentoInquilino data; 
public int Add( BeApartamentoInquilino c) { 
return data.Add (c);
}

public void Edit (BeApartamentoInquilino c) {
data.Edit(c);
}

public void EditEst (BeApartamentoInquilino c) {
 data.EditEst(c);
}

public void Del (BeApartamentoInquilino c) {
data.Del(c);
}

public List<BeApartamentoInquilino>  Gets (BeApartamentoInquilino c) {
List<BeApartamentoInquilino> r = new List<BeApartamentoInquilino>(); 
SqlConnection cn = new SqlConnection(TX_CADENA_CONEXION); 
SqlDataReader dr = null;
try {
dr = data.Gets(cn, c);
while (dr.Read()) {
BeApartamentoInquilino i = new BeApartamentoInquilino();
i.CoCondominio=dr.Num("CoCondominio");
i.CoEdificio=dr.Num("CoEdificio");
i.CoApartamento=dr.Num("CoApartamento");
i.CoInquilino=dr.Num("CoInquilino");
i.FeIniContrato=dr.Text("FeIniContrato");
i.FeFinContrato=dr.Text("FeFinContrato");
i.FeContrato=dr.Text("FeContrato");
i.NoArchivo=dr.Text("NoArchivo");
i.NoArchivoExtencion=dr.Text("NoArchivoExtencion");
r.Add(i);
}
return r;
} catch (Exception ex) {
throw ex;
} finally {
pCloseDR(cn, dr);
}
}

public BeApartamentoInquilino Get (BeApartamentoInquilino c) {
BeApartamentoInquilino i = null;
SqlConnection cn = new SqlConnection(TX_CADENA_CONEXION); 
SqlDataReader dr = null;
try {
dr = data.Get(cn, c);
if (dr.Read()) {
i = new BeApartamentoInquilino();
i.CoCondominio=dr.Num("CoCondominio");
i.CoEdificio=dr.Num("CoEdificio");
i.CoApartamento=dr.Num("CoApartamento");
i.CoInquilino=dr.Num("CoInquilino");
i.FeIniContrato=dr.Text("FeIniContrato");
i.FeFinContrato=dr.Text("FeFinContrato");
i.FeContrato=dr.Text("FeContrato");
i.NoArchivo=dr.Text("NoArchivo");
i.NoArchivoExtencion=dr.Text("NoArchivoExtencion");
}
return i;
} catch (Exception ex) {
throw ex;
} finally {
pCloseDR(cn, dr);
}
}

public BlApartamentoInquilino()
{
data = new DaApartamentoInquilino();
 }

//Destruir Objetos
bool disposed = false;
protected override void Dispose(bool disposing) {
if (disposed) return;
if (disposing) {
data = new DaApartamentoInquilino();
}
 disposed = true;
base.Dispose(disposing);
}

~BlApartamentoInquilino() { Dispose(false); }
}