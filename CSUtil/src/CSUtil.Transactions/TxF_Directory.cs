using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CSUtil.Transactions
{
  public partial class TxF
  {
    /// <summary>
    /// �g�����U�N�V�����f�B���N�g��������s���܂��B
    /// </summary>
    public class TxDirectory
    {
      private readonly SafeTransactionHandle TxHandle;

      internal TxDirectory(SafeTransactionHandle txHandle)
      {
        TxHandle = txHandle;
      }




    }
  }
}
