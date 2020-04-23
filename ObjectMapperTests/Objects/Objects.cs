using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectMapperTests.Objects
{
    public static class Objects
    {
        public class SimpleObjectA
        {
            public int Primitive { get; set; }
        }

        public class SimpleObjectB
        {
            public int Primitive { get; set; }
        }

        public class SimpleC : SimpleObjectA
        {
            public int HereOnly { get; set; }
        }

        public class SimpleObjectD
        {
            public string Primitive { get; set; }
        }

        public class ObjectA
        {
            public int Primitive { get; set; }
            public string Blah { get; set; }
            public float Meh { get; set; }
            public string SomeOther { get; set; }
            public string YetAnother { get; set; }
        }

        public static void ObjectAIsSame(ObjectA expected, ObjectA actual)
        {
            Assert.AreNotSame(expected, actual);
            Assert.AreEqual(expected.Primitive, actual.Primitive);
            Assert.AreEqual(expected.Blah, actual.Blah);
            Assert.AreEqual(expected.Meh, actual.Meh);
            Assert.AreEqual(expected.SomeOther, actual.SomeOther);
            Assert.AreEqual(expected.YetAnother, actual.YetAnother);
        }
    }
}
