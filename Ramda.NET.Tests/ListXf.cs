using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramda.NET.Tests
{
    public class ListXf : ITransformer
    {
        public object Init() {
            return new object[0];
        }

        public object Result(object result) {
            return result;
        }

        public object Step(object result, object input) {
            return ((IList)result).Concat(new[] { input });
        }
    }
}
