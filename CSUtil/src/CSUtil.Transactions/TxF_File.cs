using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CSUtil.Transactions
{
  public partial class TxF
  {
    /// <summary>
    /// �g�����U�N�V�����t�@�C��������s���܂��B
    /// </summary>
    public class TxFile
    {
      private readonly SafeTransactionHandle TxHandle;

      internal TxFile(SafeTransactionHandle txHandle)
      {
        TxHandle = txHandle;
      }



    }
  }
}
