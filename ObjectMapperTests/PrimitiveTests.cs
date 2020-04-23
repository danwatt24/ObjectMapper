using NUnit.Framework;
using ObjectMapper;
using static ObjectMapperTests.Objects.Objects;

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
            var primitiveB = _mapper.Map<SimpleObjectB>(null);
            Assert.IsNull(primitiveB);
        }

        [Test]
        public void ReturnsBasicObject()
        {
            var primitiveA = new SimpleObjectA();
            var primitiveB = _mapper.Map<SimpleObjectB>(primitiveA);
            Assert.IsNotNull(primitiveB);
            Assert.AreEqual(primitiveB.GetType(), typeof(SimpleObjectB));
        }

        [Test]
        public void WithValue()
        {
            var primitiveA = new SimpleObjectA { Primitive = 1 };
            var primitiveB = _mapper.Map<SimpleObjectB>(primitiveA);
            Assert.AreEqual(primitiveA.Primitive, primitiveB.Primitive);

            primitiveA.Primitive = 13;
            var primitiveC = _mapper.Map<SimpleObjectB>(primitiveA);
            Assert.AreEqual(primitiveA.Primitive, primitiveC.Primitive);
            Assert.AreNotEqual(primitiveB.Primitive, primitiveC.Primitive);
        }

        [Test]
        public void MissingDestProp()
        {
            var primitiveC = new SimpleC();
            var primitiveA = _mapper.Map<SimpleObjectA>(primitiveC);
            Assert.AreEqual(primitiveC.Primitive, primitiveA.Primitive);
        }

        [Test]
        public void MissingDestPropWithValues()
        {
            var primitiveC = new SimpleC { Primitive = 32, HereOnly = 12 };
            var primitiveA = _mapper.Map<SimpleObjectA>(primitiveC);
            Assert.AreEqual(primitiveC.Primitive, primitiveA.Primitive);
        }

        [Test]
        public void SkipsUnassignable()
        {
            var primitiveD = new SimpleObjectD { Primitive = "blah" };
            var primitiveA = _mapper.Map<SimpleObjectA>(primitiveD);
            Assert.NotNull(primitiveA);
        }

        [Test]
        public void PropMismatch()
        {
            var hasProps = new ObjectA { Blah = "blah", Meh = 10f, Primitive = 12, SomeOther = "other", YetAnother = "yet" };
            var primitiveA = _mapper.Map<SimpleObjectA>(hasProps);
            Assert.AreEqual(hasProps.Primitive, primitiveA.Primitive);

            primitiveA = new SimpleObjectA { Primitive = 76 };
            hasProps = _mapper.Map<ObjectA>(primitiveA);
            Assert.AreEqual(primitiveA.Primitive, hasProps.Primitive);
            Assert.AreEqual(default(float), hasProps.Meh);
            Assert.AreEqual(default(string), hasProps.Blah);
        }
    }
}