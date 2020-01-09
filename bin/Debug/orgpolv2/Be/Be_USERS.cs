using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	19/11/2018	Created
///</history>
public class Be_USERS: BeBase {
[DataMember(EmitDefaultValue = false, Name = "CUENTA")] public  string  CUENTA { get; set; }
[DataMember(EmitDefaultValue = false, Name = "COD_OP")] public  int  COD_OP { get; set; }
[DataMember(EmitDefaultValue = false, Name = "X_USER_OLD_COUSUMOD")] public  string  X_USER_OLD_COUSUMOD { get; set; }
[DataMember(EmitDefaultValue = false, Name = "FEC_VEN_CTA")] public  DateTime  FEC_VEN_CTA { get; set; }
[DataMember(EmitDefaultValue = false, Name = "USERNAME")] public  string  USERNAME { get; set; }
[DataMember(EmitDefaultValue = false, Name = "IDUSERSDGU")] public  int  IDUSERSDGU { get; set; }
[DataMember(EmitDefaultValue = false, Name = "CLAVE")] public  string  CLAVE { get; set; }
[DataMember(EmitDefaultValue = false, Name = "IDUSERSROP")] public  int  IDUSERSROP { get; set; }
[DataMember(EmitDefaultValue = false, Name = "COD_ENTE")] public  string  COD_ENTE { get; set; }
[DataMember(EmitDefaultValue = false, Name = "FEC_VEN_PSW")] public  DateTime  FEC_VEN_PSW { get; set; }
[DataMember(EmitDefaultValue = false, Name = "AMBITO")] public  string  AMBITO { get; set; }
[DataMember(EmitDefaultValue = false, Name = "COPERFIL")] public  int  COPERFIL { get; set; }
[DataMember(EmitDefaultValue = false, Name = "COD_AREA")] public  string  COD_AREA { get; set; }
 bool disposed = false;
protected override void Dispose(bool disposing){
if (disposed) return;
if (disposing){
}
disposed = true;
}
~Be_USERS() { Dispose(false); }
}