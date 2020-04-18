using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectMapperTests
{
    public class ArrayTests
    {
        private ObjectMapper.ObjectMapper _mapper;
        [SetUp]
        public void Setup()
        {
            _mapper = new ObjectMapper.ObjectMapper();
        }

        [Test]
        public void PrimitiveArrayNotSame()
        {
            var a = new PrimitiveA { Primitive = new int[0] };
            var b = _mapper.map<PrimitiveB>(a);
            Assert.AreNotSame(b.Primitive, a.Primitive);
        }

        [Test]
        public void PrimitiveArrayCopied()
        {
            var a = new PrimitiveA { Primitive = new[] { 1, 2, 3, 4 } };
            var b = _mapper.map<PrimitiveB>(a);
            for (var i = 0; i < a.Primitive.Length; i++)
                Assert.AreEqual(a.Primitive[i], b.Primitive[i]);
        }

        [Test]
        public void DestArrayNotAssignable()
        {
            var a = new PrimitiveA { Primitive = new int[0] };
            var c = _mapper.map<PrimitiveC>(a);
            Assert.IsNull(c.Primitive);
        }

        private class PrimitiveA
        {
            public int[] Primitive { get; set; }
        }

        private class PrimitiveB
        {
            public int[] Primitive { get; set; }
        }

        private class PrimitiveC
        {
            public string[] Primitive { get; set; }
        }
    }
}
