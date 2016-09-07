using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramda.NET
{
    public class Lambda
    {
        public delegate dynamic Lambda1(object arg = null);
        public delegate dynamic LambdaN(params object[] arguments);
        public delegate dynamic Lambda2(object arg1 = null, object arg2 = null);
        public delegate dynamic Lambda3(object arg1 = null, object arg2 = null, object arg3 = null);

    }
}
