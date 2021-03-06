﻿using NUnit.Framework;
using ObjectMapperTests.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using static ObjectMapperTests.Objects.Arrays;
using static ObjectMapperTests.Objects.Objects;

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
            var a = new SimpleArrayA { Primitive = new int[0] };
            var b = _mapper.Map<SimpleArrayA>(a);
            Assert.AreNotSame(b.Primitive, a.Primitive);
        }

        [Test]
        public void PrimitiveArrayCopied()
        {
            var a = new SimpleArrayA { Primitive = new[] { 1, 2, 3, 4 } };
            var b = _mapper.Map<SimpleArrayB>(a);
            for (var i = 0; i < a.Primitive.Length; i++)
                Assert.AreEqual(a.Primitive[i], b.Primitive[i]);
        }

        [Test]
        public void DestArrayNotAssignable()
        {
            var a = new SimpleArrayA { Primitive = new int[0] };
            var c = _mapper.Map<SimpleArrayC>(a);
            Assert.IsNull(c.Primitive);
        }

        [Test]
        public void ArrayOfObjects()
        {
            var a = new ObjectArrayA { ObjectA = new[] { ObjectFactory.MakeObjectA() } };
            var b = _mapper.Map<ObjectArrayB>(a);
            for (var i = 0; i < a.ObjectA.Length; i++)
            {
                var expected = a.ObjectA[i];
                var actual = b.ObjectA[i];
                ObjectAIsSame(expected, actual);
            }
        }

        [Test]
        public void Simple2dArray()
        {
            var array2d = new int[1][];
            array2d[0] = new[] { 1 };
            var a = new Simple2dArrayA { Primitive = array2d };
            var b = _mapper.Map<Simple2dArrayB>(a);
            for (var i = 0; i < a.Primitive.Length; i++)
            {
                var arrExpected = a.Primitive[i];
                var arrActual = b.Primitive[i];
                Assert.AreNotSame(arrExpected, arrActual);
                for (var j = 0; j < arrExpected.Length; j++)
                {
                    Assert.AreEqual(arrExpected[j], arrActual[j]);
                }
            }
        }
    }
}
