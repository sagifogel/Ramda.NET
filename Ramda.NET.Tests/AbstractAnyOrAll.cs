using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramda.NET.Tests
{
    public abstract class AbstractAnyOrAll
    {
        protected Func<bool> T = () => true;
        protected Func<int, bool> odd = n => n % 2 == 1;
        protected dynamic intoArray = R.Into(new object[0]);
        protected Func<bool, bool> isFalse = x => x == false;
        protected Func<object, bool> even = n => ((int)n) % 2 == 0;

        protected class ListXf : ITransformer
        {
            public object Init() {
                return new object[0];
            }

            public object Result(object result) => result;

            public object Step(object result, object input) {
                return ((object[])result).Concat(new[] { input });
            }
        }
    }
}
