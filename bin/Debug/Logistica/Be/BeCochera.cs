using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	19-11-2018	Created
///</history>
public class BeCochera: BeBase {
[DataMember(EmitDefaultValue = false, Name = "CoCochera")] public  int  CoCochera { get; set; }
[DataMember(EmitDefaultValue = false, Name = "CoCondominio")] public  int  CoCondominio { get; set; }
[DataMember(EmitDefaultValue = false, Name = "CoEdificio")] public  int  CoEdificio { get; set; }
[DataMember(EmitDefaultValue = false, Name = "NuCochera")] public  string  NuCochera { get; set; }
[DataMember(EmitDefaultValue = false, Name = "NuArea")] public  int  NuArea { get; set; }
[DataMember(EmitDefaultValue = false, Name = "NuPiso")] public  int  NuPiso { get; set; }
 bool disposed = false;
protected override void Dispose(bool disposing){
if (disposed) return;
if (disposing){
}
disposed = true;
}
~BeCochera() { Dispose(false); }
}