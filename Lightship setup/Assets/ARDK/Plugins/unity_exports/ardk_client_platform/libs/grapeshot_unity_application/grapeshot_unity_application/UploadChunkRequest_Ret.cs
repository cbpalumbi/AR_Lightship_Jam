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

public class UploadChunkRequest_Ret : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal UploadChunkRequest_Ret(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(UploadChunkRequest_Ret obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~UploadChunkRequest_Ret() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          GrapeshotUploadFlatbuffersPINVOKE.delete_UploadChunkRequest_Ret(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public void ret(ror.schema.upload.UploadChunkRequest newValue) {
var newValue_table = FlatBuffers.TableUtils.GetTable(newValue);var newValue_length = newValue_table.bb.Length;var newValue_pos = newValue_table.bb_pos;var newValue_seg = newValue_table.bb.ToArraySegment(newValue_pos, newValue_length - newValue_pos);var newValue_managed_array = newValue_seg.Array;var newValue_managed_handle = System.Runtime.InteropServices.GCHandle.Alloc(newValue_managed_array, System.Runtime.InteropServices.GCHandleType.Pinned);var newValue_buffer_ptr = newValue_managed_handle.AddrOfPinnedObject();System.IntPtr newValue_fb_ptr;unsafe {    var newValue_buffer_unsafe_ptr = (byte*) newValue_buffer_ptr.ToPointer();    newValue_fb_ptr = (System.IntPtr) (newValue_buffer_unsafe_ptr + newValue_pos);}var newValue_intermediate = new FBIntermediateBuffer();newValue_intermediate.ptr = newValue_fb_ptr;
    try {
      GrapeshotUploadFlatbuffersPINVOKE.UploadChunkRequest_Ret_ret(swigCPtr, newValue_intermediate);
    } finally {
newValue_managed_handle.Free();
    }
  }

  public UploadChunkRequest_Ret() : this(GrapeshotUploadFlatbuffersPINVOKE.new_UploadChunkRequest_Ret(), true) {
  }

}

}
