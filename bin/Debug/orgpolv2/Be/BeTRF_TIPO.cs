using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	04/12/2018	Created
///</history>
public class BeTRF_TIPO: BeBase {
[DataMember(EmitDefaultValue = false, Name = "TXVALOR")] public  string  TXVALOR { get; set; }
[DataMember(EmitDefaultValue = false, Name = "TXSIGLA")] public  string  TXSIGLA { get; set; }
[DataMember(EmitDefaultValue = false, Name = "TXDESCRIPCION")] public  string  TXDESCRIPCION { get; set; }
[DataMember(EmitDefaultValue = false, Name = "IDTIPOREF")] public  int  IDTIPOREF { get; set; }
[DataMember(EmitDefaultValue = false, Name = "NUORDEN")] public  int  NUORDEN { get; set; }
[DataMember(EmitDefaultValue = false, Name = "IDGRUPO")] public  int  IDGRUPO { get; set; }
[DataMember(EmitDefaultValue = false, Name = "IDTIPO")] public  int  IDTIPO { get; set; }
[DataMember(EmitDefaultValue = false, Name = "TXTIPO")] public  string  TXTIPO { get; set; }
 bool disposed = false;
protected override void Dispose(bool disposing){
if (disposed) return;
if (disposing){
}
disposed = true;
}
~BeTRF_TIPO() { Dispose(false); }
}