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
using System.Collections.Generic;

using Smdn.Formats;

namespace Smdn.Net.Pop3 {
  public class PopCapabilityList : PopStringEnumList<PopCapability> {
    public PopCapabilityList()
      : base(false)
    {
    }

    public PopCapabilityList(IEnumerable<PopCapability> caps)
      : this(false, caps)
    {
    }

    public PopCapabilityList(bool readOnly, IEnumerable<PopCapability> caps)
      : base(readOnly, caps)
    {
    }

    public PopCapability FindByTag(PopCapability capability)
    {
      if (capability == null)
        throw new ArgumentNullException("capability");

      return FindByTag(capability.Tag);
    }

    public PopCapability FindByTag(string tag)
    {
      if (tag == null)
        throw new ArgumentNullException("tag");

      foreach (var capability in this) {
        if (string.Equals(capability.Tag, tag, StringComparison.OrdinalIgnoreCase))
          return capability;
      }

      return null;
    }

    public bool IsCapable(IPopExtension extension)
    {
      if (extension == null)
        throw new ArgumentNullException("extension");

      if (extension.RequiredCapability == null)
        return true;

      return IsCapable(extension.RequiredCapability);
    }

    public bool IsCapable(PopCapability requiredCapability)
    {
      if (requiredCapability == null)
        throw new ArgumentNullException("requiredCapability");

      var capability = FindByTag(requiredCapability);

      if (capability == null)
        return false;

      return capability.ContainsAllArguments(requiredCapability.Arguments);
    }
  }
}