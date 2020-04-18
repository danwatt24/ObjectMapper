using NUnit.Framework;
using ObjectMapper;

namespace ObjectMapperTests
{
    public class PrimitiveTests
    {
        private IObjectMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = new ObjectMapper.ObjectMapper();
        }

        [Test]
        public void NullSource()
        {
            var primitiveB = _mapper.map<PrimitiveB>(null);
            Assert.IsNull(primitiveB);
        }

        [Test]
        public void ReturnsBasicObject()
        {
            var primitiveA = new PrimitiveA();
            var primitiveB = _mapper.map<PrimitiveB>(primitiveA);
            Assert.IsNotNull(primitiveB);
            Assert.AreEqual(primitiveB.GetType(), typeof(PrimitiveB));
        }

        [Test]
        public void WithValue()
        {
            var primitiveA = new PrimitiveA { Primitive = 1 };
            var primitiveB = _mapper.map<PrimitiveB>(primitiveA);
            Assert.AreEqual(primitiveA.Primitive, primitiveB.Primitive);

            primitiveA.Primitive = 13;
            var primitiveC = _mapper.map<PrimitiveB>(primitiveA);
            Assert.AreEqual(primitiveA.Primitive, primitiveC.Primitive);
            Assert.AreNotEqual(primitiveB.Primitive, primitiveC.Primitive);
        }

        [Test]
        public void MissingDestProp()
        {
            var primitiveC = new PrimitiveC();
            var primitiveA = _mapper.map<PrimitiveA>(primitiveC);
            Assert.AreEqual(primitiveC.Primitive, primitiveA.Primitive);
        }

        [Test]
        public void MissingDestPropWithValues()
        {
            var primitiveC = new PrimitiveC { Primitive = 32, HereOnly = 12 };
            var primitiveA = _mapper.map<PrimitiveA>(primitiveC);
            Assert.AreEqual(primitiveC.Primitive, primitiveA.Primitive);
        }

        [Test]
        public void SkipsUnassignable()
        {
            var primitiveD = new PrimitiveD { Primitive = "blah" };
            var primitiveA = _mapper.map<PrimitiveA>(primitiveD);
            Assert.NotNull(primitiveA);
        }

        [Test]
        public void PropMismatch()
        {
            var hasProps = new HasMoreProps { Blah = "blah", Meh = 10f, Primitive = 12, SomeOther = "other", YetAnother = "yet" };
            var primitiveA = _mapper.map<PrimitiveA>(hasProps);
            Assert.AreEqual(hasProps.Primitive, primitiveA.Primitive);

            primitiveA = new PrimitiveA { Primitive = 76 };
            hasProps = _mapper.map<HasMoreProps>(primitiveA);
            Assert.AreEqual(primitiveA.Primitive, hasProps.Primitive);
            Assert.AreEqual(default(float), hasProps.Meh);
            Assert.AreEqual(default(string), hasProps.Blah);
        }

        private class PrimitiveA
        {
            public int Primitive { get; set; }
        }

        private class PrimitiveB
        {
            public int Primitive { get; set; }
        }

        private class PrimitiveC : PrimitiveA
        {
            public int HereOnly { get; set; }
        }

        private class PrimitiveD
        {
            public string Primitive { get; set; }
        }

        private class HasMoreProps
        {
            public int Primitive { get; set; }
            public string Blah { get; set; }
            public float Meh { get; set; }
            public string SomeOther { get; set; }
            public string YetAnother { get; set; }
        }
    }
}