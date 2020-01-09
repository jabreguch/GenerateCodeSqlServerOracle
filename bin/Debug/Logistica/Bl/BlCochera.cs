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
public class BlCochera: BlBase { 
DaCochera data; 
public int Add( BeCochera c) { 
return data.Add (c);
}

public void Edit (BeCochera c) {
data.Edit(c);
}

public void EditEst (BeCochera c) {
 data.EditEst(c);
}

public void Del (BeCochera c) {
data.Del(c);
}

public List<BeCochera>  Gets (BeCochera c) {
List<BeCochera> r = new List<BeCochera>(); 
SqlConnection cn = new SqlConnection(TX_CADENA_CONEXION); 
SqlDataReader dr = null;
try {
dr = data.Gets(cn, c);
while (dr.Read()) {
BeCochera i = new BeCochera();
i.CoCochera=dr.Num("CoCochera");
i.CoCondominio=dr.Num("CoCondominio");
i.CoEdificio=dr.Num("CoEdificio");
i.NuCochera=dr.Text("NuCochera");
i.NuArea=dr.Num("NuArea");
i.NuPiso=dr.Num("NuPiso");
r.Add(i);
}
return r;
} catch (Exception ex) {
throw ex;
} finally {
pCloseDR(cn, dr);
}
}

public BeCochera Get (BeCochera c) {
BeCochera i = null;
SqlConnection cn = new SqlConnection(TX_CADENA_CONEXION); 
SqlDataReader dr = null;
try {
dr = data.Get(cn, c);
if (dr.Read()) {
i = new BeCochera();
i.CoCochera=dr.Num("CoCochera");
i.CoCondominio=dr.Num("CoCondominio");
i.CoEdificio=dr.Num("CoEdificio");
i.NuCochera=dr.Text("NuCochera");
i.NuArea=dr.Num("NuArea");
i.NuPiso=dr.Num("NuPiso");
}
return i;
} catch (Exception ex) {
throw ex;
} finally {
pCloseDR(cn, dr);
}
}

public BlCochera()
{
data = new DaCochera();
 }

//Destruir Objetos
bool disposed = false;
protected override void Dispose(bool disposing) {
if (disposed) return;
if (disposing) {
data = new DaCochera();
}
 disposed = true;
base.Dispose(disposing);
}

~BlCochera() { Dispose(false); }
}