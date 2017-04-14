using System;

namespace Ramda.NET.Tests
{
    public class BaseFind
    {
        protected static readonly object obj1 = new { X = 100 };
        protected static readonly object obj2 = new { X = 200 };
        protected static readonly dynamic isXNotNull = R.Compose(new dynamic[] { R.Not(R.__), R.IsNil(R.__), R.Prop("X") });
        protected static object[] a = new[] { 11, 10, 9, "cow", obj1, 8, 7, 100, 200, 300, obj2, 4, 3, 2, 1, 0 };
        protected static Func<object, Func<int, bool>, bool> parseAndExec = (n, f) => {
            int result;
            var str = n.ToString();

            if (int.TryParse(str, out result)) {
                return f(result);
            }

            return false;
        };

        protected static readonly Func<object, bool> even = n => parseAndExec(n, result => result % 2 == 0);
        protected static readonly Func<object, bool> gt100 = n => parseAndExec(n, result => result > 100);
        protected static readonly dynamic intoArray = R.Into(new object[0]);
        protected static readonly Func<object, bool> isStr = x => x.GetType().Equals(typeof(string));
        protected static readonly Func<dynamic, bool> xGt100 = o => {
            if (isXNotNull(o)) {
                return (int)(o.X) > 100;
            }

            return false;
        };
    }
}