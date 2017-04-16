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
                                      var typedAIsAnonymous = false;
                                      var typedBIsAnonymous = false;

                                      if (typedA.Value.IsDictionary() && typedB.Value.IsDictionary()) {
                                          var innerA = typedA.Value as IDictionary<string, object>;
                                          var innerB = typedB.Value as IDictionary<string, object>;

                                          return innerA.ContentEquals(innerB);
                                      }

                                      typedAIsAnonymous = typedA.Value.IsAnonymousType();
                                      typedBIsAnonymous = typedB.Value.IsAnonymousType();

                                      if (typedAIsAnonymous || typedBIsAnonymous) {
                                          ExpandoObject innerA = typedA.Value.ToDynamic();
                                          ExpandoObject innerB = typedB.Value.ToDynamic();

                                          return innerA.ContentEquals(innerB);
                                      }

                                      if (typedA.Value.IsList() && typedB.Value.IsList()) {
                                          var innerA = typedA.Value as IList;
                                          var innerB = typedB.Value as IList;

                                          if (innerA.Count == innerB.Count) {
                                              var hash = new HashSet<object>(innerA.Select(o => o));

                                              foreach (var item in innerB) {
                                                  if (!hash.Contains(item)) {
                                                      return false;
                                                  }
                                              }

                                              return true;
                                          }

                                          return false;
                                      }

                                      return typedA.Value.Equals(typedB.Value);
                                  });
        }

        public static bool ContentEquals(object dictionary, object expando) {
            return dictionary.ToExpando().ContentEquals(expando.ToExpando());
        }

        public static dynamic ToDynamic(this object value) {
            if (value.IsExpandoObject()) {
                return value;
            }

            Type type = value.GetType();
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
