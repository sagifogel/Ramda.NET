using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramda.NET
{
    public static partial class R
    {
        public class Tuple
        {
            public object Item1 { get; private set; }
            public object Item2 { get; private set; }

            public static Tuple Create(object item1, object item2) {
                return new Tuple(item1, item2);
            }

            private Tuple(object item1, object item2) {
                Item1 = item1;
                Item2 = item2;
            }
        }
    }
}
