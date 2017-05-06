using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramda.NET.Tests
{
    public class _Nothing : _Maybe,  IAppable
    {
        public _Nothing(object value) : base(value) {
        }

        public override string ToString() {
            return "Nothing()";
        }

        public object Map(Func<object, object> f) {
            return this;
        }

        public object Ap(IMappable mappable) {
            return this;
        }
    }
}
