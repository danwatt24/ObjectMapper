using NUnit.Framework;
using ObjectMapper;

namespace ObjectMapperTests
{
    public class SimpleTests
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
            var simpleB = _mapper.map<SimpleB>(null);
            Assert.IsNull(simpleB);
        }

        [Test]
        public void ReturnsBasicObject()
        {
            var simpleA = new SimpleA();
            var simpleB = _mapper.map<SimpleB>(simpleA);
            Assert.IsNotNull(simpleB);
            Assert.AreEqual(simpleB.GetType(), typeof(SimpleB));
        }

        [Test]
        public void WithValue()
        {
            var simpleA = new SimpleA { Simple = 1 };
            var simpleB = _mapper.map<SimpleB>(simpleA);
            Assert.AreEqual(simpleA.Simple, simpleB.Simple);

            simpleA.Simple = 13;
            var simpleC = _mapper.map<SimpleB>(simpleA);
            Assert.AreEqual(simpleA.Simple, simpleC.Simple);
            Assert.AreNotEqual(simpleB.Simple, simpleC.Simple);
        }

        [Test]
        public void MissingDestProp()
        {
            var simpleC = new SimpleC();
            var simpleA = _mapper.map<SimpleA>(simpleC);
            Assert.AreEqual(simpleC.Simple, simpleA.Simple);
        }

        [Test]
        public void MissingDestPropWithValues()
        {
            var simpleC = new SimpleC { Simple = 32, HereOnly = 12 };
            var simpleA = _mapper.map<SimpleA>(simpleC);
            Assert.AreEqual(simpleC.Simple, simpleA.Simple);
        }

        [Test]
        public void SkipsUnassignable()
        {
            var simpleD = new SimpleD { Simple = "blah" };
            var simpleA = _mapper.map<SimpleA>(simpleD);
            Assert.NotNull(simpleA);
        }

        [Test]
        public void PropMismatch()
        {
            var hasProps = new HasMoreProps { Blah = "blah", Meh = 10f, Simple = 12, SomeOther = "other", YetAnother = "yet" };
            var simpleA = _mapper.map<SimpleA>(hasProps);
            Assert.AreEqual(hasProps.Simple, simpleA.Simple);

            simpleA = new SimpleA { Simple = 76 };
            hasProps = _mapper.map<HasMoreProps>(simpleA);
            Assert.AreEqual(simpleA.Simple, hasProps.Simple);
            Assert.AreEqual(default(float), hasProps.Meh);
            Assert.AreEqual(default(string), hasProps.Blah);
        }

        private class SimpleA
        {
            public int Simple { get; set; }
        }

        private class SimpleB
        {
            public int Simple { get; set; }
        }

        private class SimpleC : SimpleA
        {
            public int HereOnly { get; set; }
        }

        private class SimpleD
        {
            public string Simple { get; set; }
        }

        private class HasMoreProps
        {
            public int Simple { get; set; }
            public string Blah { get; set; }
            public float Meh { get; set; }
            public string SomeOther { get; set; }
            public string YetAnother { get; set; }
        }
    }
}