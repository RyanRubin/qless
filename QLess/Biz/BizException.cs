using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLess.Biz
{
    public class BizException : Exception
    {
        public BizException() : base() { }
        public BizException(string message) : base(message) { }
        public BizException(string message, Exception inner) : base(message, inner) { }
    }
}
