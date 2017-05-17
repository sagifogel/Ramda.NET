using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ObjOf
    {
        [TestMethod]
        [Description("ObjOf_Creates_An_Object_Containing_A_Single_Key:Value_Pair")]
        public void ObjOf_Creates_An_Object_Containing_A_Single_Key_Value_Pair() {
            DynamicAssert.AreEqual(R.ObjOf("Foo", 42), new { Foo = 42 });
            DynamicAssert.AreEqual(R.ObjOf("foo")(42), new { Foo = 42 });
        }
    }
}
