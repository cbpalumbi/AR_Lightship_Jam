//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.0
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Grapeshot {

public class StageNamedInstance : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal StageNamedInstance(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(StageNamedInstance obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~StageNamedInstance() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          RORStagePINVOKE.delete_StageNamedInstance(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  static private global::System.IntPtr SwigConstructStageNamedInstance(System.Guid stage, string name) {
var stage_Bytes = stage.ToByteArray();var stage_ByteArrayHandle = System.Runtime.InteropServices.GCHandle.Alloc(stage_Bytes ,System.Runtime.InteropServices.GCHandleType.Pinned);var stage_buffer_ptr = stage_ByteArrayHandle.AddrOfPinnedObject();
    try {
      return RORStagePINVOKE.new_StageNamedInstance(stage_buffer_ptr, name);
    } finally {
stage_ByteArrayHandle.Free();
    }
  }

  public StageNamedInstance(System.Guid stage, string name) : this(StageNamedInstance.SwigConstructStageNamedInstance(stage, name), true) {
    if (RORStagePINVOKE.SWIGPendingException.Pending) throw RORStagePINVOKE.SWIGPendingException.Retrieve();
  }

}

}
