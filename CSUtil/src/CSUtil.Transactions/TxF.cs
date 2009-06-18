using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Transactions;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;
using CSUtil;

namespace CSUtil.Transactions
{
  /// <summary>
  /// TxF(Transactional NTFS)�Ǘ��N���X
  /// </summary>
  public sealed partial class TxF
  {

    /// <summary>
    /// �t�@�C������I�u�W�F�N�g�B
    /// </summary>
    public readonly TxFile File;

    /// <summary>
    /// �f�B���N�g������I�u�W�F�N�g�B
    /// </summary>
    public readonly TxDirectory Directory;

    /// <summary>
    /// �R���X�g���N�^�B
    /// </summary>
    public TxF()
    {
      SafeTransactionHandle txHandle;
      IKernelTransaction kernelTx =
        (IKernelTransaction)TransactionInterop.GetDtcTransaction(Transaction.Current);
      kernelTx.GetHandle(out txHandle);
      File = new TxFile(txHandle);
      Directory = new TxDirectory(txHandle);
    }


    #region TxF API

    [ComImport]
    [Guid("79427A2B-F895-40e0-BE79-B57DC82ED231")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IKernelTransaction
    {
      void GetHandle(out SafeTransactionHandle ktmHandle);
    }

    [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
    internal sealed class SafeTransactionHandle : SafeHandleZeroOrMinusOneIsInvalid
    {

      private SafeTransactionHandle()
        : base(true)
      {
      }

      public SafeTransactionHandle(IntPtr preexistingHandle, bool ownsHandle)
        : base(ownsHandle)
      {
        SetHandle(preexistingHandle);
      }

      [DllImport("Kernel32.dll", SetLastError = true)]
      [ResourceExposure(ResourceScope.Machine)]
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      private static extern bool CloseHandle(IntPtr handle);

      [ResourceExposure(ResourceScope.Machine)]
      [ResourceConsumption(ResourceScope.Machine)]
      override protected bool ReleaseHandle()
      {
        return CloseHandle(handle);
      }

    }

    #endregion
  }
}