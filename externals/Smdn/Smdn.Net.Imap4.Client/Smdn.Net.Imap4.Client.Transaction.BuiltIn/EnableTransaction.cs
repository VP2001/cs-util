// 
// Author:
//       smdn <smdn@mail.invisiblefulmoon.net>
// 
// Copyright (c) 2008-2010 smdn
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;

using Smdn.Net.Imap4.Protocol;
using Smdn.Net.Imap4.Protocol.Client;

namespace Smdn.Net.Imap4.Client.Transaction.BuiltIn {
  internal sealed class EnableTransaction : ImapTransactionBase<ImapCommandResult<ImapCapabilityList>>, IImapExtension {
    ImapCapability IImapExtension.RequiredCapability {
      get { return ImapCapability.Enable; }
    }

    public EnableTransaction(ImapConnection connection)
      : base(connection)
    {
    }

    protected override ProcessTransactionDelegate Reset()
    {
#if DEBUG
      if (!RequestArguments.ContainsKey("capability names"))
        return ProcessArgumentNotSetted;
      else
#endif
        return ProcessEnable;
    }

#if DEBUG
    private void ProcessArgumentNotSetted()
    {
      FinishError(ImapCommandResultCode.RequestError, "arguments 'capability names' must be setted");
    }
#endif

    // 3.1. The ENABLE Command
    //    Arguments: capability names
    //    Result:    OK: Relevant capabilities enabled
    //               BAD: No arguments, or syntax error in an argument
    private void ProcessEnable()
    {
      // ENABLE
      SendCommand("ENABLE", ProcessReceiveResponse, RequestArguments["capability names"]);
    }

    protected override void OnDataResponseReceived(ImapDataResponse data)
    {
      if (data.Type == ImapDataResponseType.Enabled)
        enabledCapabilities = ImapDataResponseConverter.FromEnabled(data);

      base.OnDataResponseReceived(data);
    }

    protected override void OnTaggedStatusResponseReceived(ImapTaggedStatusResponse tagged)
    {
      if (tagged.Condition == ImapResponseCondition.Ok)
        Finish(new ImapCommandResult<ImapCapabilityList>(enabledCapabilities, tagged.ResponseText));
      else
        base.OnTaggedStatusResponseReceived(tagged);
    }

    private ImapCapabilityList enabledCapabilities = null;
  }
}