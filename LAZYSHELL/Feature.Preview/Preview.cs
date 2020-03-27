using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    public interface Preview
    {
        int[] GetPreview(params object[] args);
    }
}
