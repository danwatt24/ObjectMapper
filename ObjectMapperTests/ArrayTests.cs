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
        public void SimpleArrayNotSame()
        {
            var a = new SimpleA { Simple = new int[0] };
            var b = _mapper.map<SimpleB>(a);
            Assert.AreNotSame(b.Simple, a.Simple);
        }

        [Test]
        public void SimpleArrayCopied()
        {
            var a = new SimpleA { Simple = new[] { 1, 2, 3, 4 } };
            var b = _mapper.map<SimpleB>(a);
            for (var i = 0; i < a.Simple.Length; i++)
                Assert.AreEqual(a.Simple[i], b.Simple[i]);
        }

        private class SimpleA
        {
            public int[] Simple { get; set; }
        }

        private class SimpleB
        {
            public int[] Simple { get; set; }
        }
    }
}
