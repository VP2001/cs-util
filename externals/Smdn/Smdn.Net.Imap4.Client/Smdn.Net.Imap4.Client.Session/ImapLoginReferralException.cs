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
using System.Runtime.Serialization;

namespace Smdn.Net.Imap4.Client.Session {
  // Exceptions:
  //   System.Excpetion
  //     System.SystemException
  //       Smdn.Net.Imap4.ImapExcetion
  //         Smdn.Net.Imap4.Session.ImapReferralException
  //           Smdn.Net.Imap4.Session.ImapLoginReferralException

  [Serializable]
  public class ImapLoginReferralException : ImapReferralException {
    public Uri ReferToUri {
      get; private set;
    }

    public ImapLoginReferralException(string message, Uri referToUri)
      : base(message)
    {
      ReferToUri = referToUri;
    }

    protected ImapLoginReferralException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      ReferToUri = (Uri)info.GetValue("ReferToUri", typeof(Uri));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);

      info.AddValue("ReferToUri", ReferToUri);
    }
  }
}
