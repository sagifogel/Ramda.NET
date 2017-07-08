using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace Ramda.NET.Tests
{
    public static class Extension
    {
        public static bool ContentEquals(this IDictionary<string, object> dictionary, IDictionary<string, object> otherDictionary) {
            return otherDictionary.OrderBy(kvp => kvp.Key)
                                  .SequenceEqual(dictionary.OrderBy(kvp => kvp.Key), (a, b) => {
                                      var typedA = (KeyValuePair<string, object>)a;
                                      var typedB = (KeyValuePair<string, object>)b;

                                      return typedA.Value.EqualsInternal(typedB.Value);
                                  });
        }

        public static bool EqualsInternal(this object obj1, object obj2) {
            if (obj1.IsDictionary() && obj2.IsDictionary()) {
                var innerA = obj1 as IDictionary<string, object>;
                var innerB = obj2 as IDictionary<string, object>;

                if (innerA == null) {
                    innerA = obj1.ToExpando();
                }

                if (innerB == null) {
                    innerB = obj2.ToExpando();
                }

                return innerA.ContentEquals(innerB);
            }

            var typedAIsAnonymous = obj1.IsAnonymousType();
            var typedBIsAnonymous = obj2.IsAnonymousType();

            if (typedAIsAnonymous || typedBIsAnonymous) {
                ExpandoObject innerA = obj1.ToDynamic();
                ExpandoObject innerB = obj2.ToDynamic();

                return innerA.ContentEquals(innerB);
            }

            if (obj1.IsList() && obj2.IsList()) {
                var innerA = obj1 as IList;
                var innerB = obj2 as IList;

                if (innerA.Count == innerB.Count) {
                    for (int i = 0; i < innerB.Count; i++) {
                        var innerAItem = innerA[i];
                        var innerBItem = innerB[i];

                        if (!innerAItem.GetType().Equals(innerBItem.GetType())) {
                            var innerBItemMap = innerB[i].ToDynamic() as IDictionary<string, object>;
                            var innerAItemMap = innerA[i].ToDynamic() as IDictionary<string, object>;

                            if (!innerBItemMap.ContentEquals(innerAItemMap)) {
                                return false;
                            }
                        }
                        else if (!innerAItem.EqualsInternal(innerBItem)) {
                            return false;
                        }
                    }

                    return true;
                }

                return false;
            }

            return obj1.Equals(obj2);
        }

        public static bool ContentEquals(object dictionary, object expando) {
            return dictionary.ToExpando().ContentEquals(expando.ToExpando());
        }

        public static dynamic ToDynamic(this object value) {
            if (value.IsExpandoObject()) {
                return value;
            }

            var type = value.GetType();
            IDictionary<string, object> expando = new ExpandoObject();
            var dictionary = value as IDictionary;

            if (dictionary != null) {
                dictionary.Keys.ForEach(key => expando[key.ToString()] = dictionary[key]);
            }
            else if (type.Assembly.Equals(typeof(object).Assembly)) {
                return value;
            }
            else {
                value.ToMemberDictionary()
                     .ForEach(kv => {
                         expando.Add(kv.Key, kv.Value);
                     });
            }

            return expando as ExpandoObject;
        }

        public static ExpandoObject ToExpando(this object value) {
            return value.ToDynamic();
        }

        public static bool SequenceEqual(this IEnumerable first, IEnumerable second) {
            return first.SequenceEqual(second, (a, b) => {
                if (a.IsEnumerable() && b.IsEnumerable()) {
                    return ((IEnumerable)a).SequenceEqual((IEnumerable)b);
                }

                return a.Equals(b);
            });
        }
    }
}
