//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class MsgPayload_i2c_tran_v1_resp : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal MsgPayload_i2c_tran_v1_resp(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(MsgPayload_i2c_tran_v1_resp obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~MsgPayload_i2c_tran_v1_resp() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          msgModPINVOKE.delete_MsgPayload_i2c_tran_v1_resp(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public ushort chipAddr {
    set {
      msgModPINVOKE.MsgPayload_i2c_tran_v1_resp_chipAddr_set(swigCPtr, value);
    } 
    get {
      ushort ret = msgModPINVOKE.MsgPayload_i2c_tran_v1_resp_chipAddr_get(swigCPtr);
      return ret;
    } 
  }

  public ushort __reserved0 {
    set {
      msgModPINVOKE.MsgPayload_i2c_tran_v1_resp___reserved0_set(swigCPtr, value);
    } 
    get {
      ushort ret = msgModPINVOKE.MsgPayload_i2c_tran_v1_resp___reserved0_get(swigCPtr);
      return ret;
    } 
  }

  public ushort rdLen {
    set {
      msgModPINVOKE.MsgPayload_i2c_tran_v1_resp_rdLen_set(swigCPtr, value);
    } 
    get {
      ushort ret = msgModPINVOKE.MsgPayload_i2c_tran_v1_resp_rdLen_get(swigCPtr);
      return ret;
    } 
  }

  public ushort __reserved1 {
    set {
      msgModPINVOKE.MsgPayload_i2c_tran_v1_resp___reserved1_set(swigCPtr, value);
    } 
    get {
      ushort ret = msgModPINVOKE.MsgPayload_i2c_tran_v1_resp___reserved1_get(swigCPtr);
      return ret;
    } 
  }

  public SWIGTYPE_p_unsigned_char rdBuff {
    set {
      msgModPINVOKE.MsgPayload_i2c_tran_v1_resp_rdBuff_set(swigCPtr, SWIGTYPE_p_unsigned_char.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = msgModPINVOKE.MsgPayload_i2c_tran_v1_resp_rdBuff_get(swigCPtr);
      SWIGTYPE_p_unsigned_char ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_unsigned_char(cPtr, false);
      return ret;
    } 
  }

  public MsgPayload_i2c_tran_v1_resp() : this(msgModPINVOKE.new_MsgPayload_i2c_tran_v1_resp(), true) {
  }

}