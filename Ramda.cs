using System;
using System.Collections.Generic;

namespace Ramda.NET
{
    public static partial class R
    {
        public static Currying.RamdaPlaceholder __ = new Currying.RamdaPlaceholder();

        public static dynamic CurryN(int length, Delegate fn) {
            return Currying.CurryN(length, fn);
        }

        public static dynamic Add(int arg1) {
            return Currying.Add(arg1);
        }

        public static dynamic Add(double arg1) {
            return Currying.Add(arg1);
        }

        public static dynamic Add(long arg1) {
            return Currying.Add(arg1);
        }

        public static dynamic Add(string arg1) {
            return Currying.Add(arg1);
        }

        public static dynamic Add(Currying.RamdaPlaceholder arg1) {
            return Currying.Add(arg1);
        }

        public static dynamic Add(Currying.RamdaPlaceholder arg1, Currying.RamdaPlaceholder arg2) {
            return Currying.Add(arg1, arg2);
        }


        public static dynamic Add(int arg1, int arg2) {
            return Currying.Add(arg1, arg2);
        }

        public static dynamic Add(double arg1, double arg2) {
            return Currying.Add(arg1, arg2);
        }

        public static dynamic Add(long arg1, long arg2) {
            return Currying.Add(arg1, arg2);
        }

        public static dynamic Add(string arg1, string arg2) {
            return Currying.Add(arg1, arg2);
        }

        public static dynamic Adjust<TValue>(Func<TValue, TValue> fn) {
            return Currying.Adjust(fn);
        }

        public static dynamic Adjust<TValue>(Func<TValue, TValue> fn, int idx) {
            return Currying.Adjust(fn, idx);
        }

        public static dynamic Adjust<TValue>(Func<TValue, TValue> fn, int idx, IList<TValue> list) {
            return Currying.Adjust(fn, idx, list);
        }

        public static dynamic Always<TValue>(TValue value) {
            return Currying.Always(value);
        }

        public static dynamic DefaultTo<TArg1>(TArg1 arg1) {
            return Currying.DefaultTo(arg1);
        }

        public static dynamic DefaultTo(Currying.RamdaPlaceholder arg1 = null) {
            return Currying.DefaultTo(arg1);
        }

        public static dynamic DefaultTo<TArg1, TArg2>(TArg1 arg1, TArg2 arg2) {
            return Currying.DefaultTo(arg1, arg2);
        }

        public static dynamic DissocPath(IList<string> list) {
            return Currying.DissocPath(list);
        }

        public static dynamic DissocPath() {
            return Currying.DissocPath(__, __);
        }

        public static dynamic DissocPath(IList<string> list, object obj) {
            return Currying.DissocPath(list, obj);
        }

        public static dynamic DissocPath(Currying.RamdaPlaceholder __, object obj) {
            return Currying.DissocPath(__, obj);
        }

        public static dynamic DissocPath(IList<string> list, Currying.RamdaPlaceholder __) {
            return Currying.DissocPath(list, __);
        }

        public static dynamic Evolve(Dictionary<string, object> transformations, object target) {
            return Currying.Evolve(transformations, target);
        }
    }
}
