using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Join
    {
        [TestMethod]
        [Description("Join_Concatenates_A_List's_Elements_To_A_String,_With_An_Separator_String_Between_Elements")]
        public void Join_Concatenates_A_Lists_Elements_To_A_String_With_An_Separator_String_Between_Elements() {
            Assert.AreEqual(R.Join("~", new[] { 1, 2, 3, 4 }), "1~2~3~4");
        }
    }
}
