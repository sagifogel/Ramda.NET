using System.Collections;
using System.Collections.Generic;
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

                                      if (typedA.Value.IsDictionary() && typedB.Value.IsDictionary()) {
                                          var innerA = typedA.Value as IDictionary<string, object>;
                                          var innerB = typedB.Value as IDictionary<string, object>;

                                          return innerA.ContentEquals(innerB);
                                      }

                                      return typedA.Value.Equals(typedB.Value);
                                  });
        }

        public static bool ContentEquals(this IDictionary<string, object> dictionary, ExpandoObject expando) {
            var otherDictionary = expando as IDictionary<string, object>;

            return dictionary.ContentEquals(otherDictionary);
        }
    }
}
