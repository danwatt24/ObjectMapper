using System;
using System.Collections.Generic;
using System.Text;
using static ObjectMapperTests.Objects.Objects;

namespace ObjectMapperTests.Factories
{
    public static class ObjectFactory
    {
        public static ObjectA MakeObjectA(
            int primitive = 12,
            string blah = "some string",
            float meh = 1.3f,
            string someOther = "another string",
            string yetAnother = "yet another"
        )
        {
            return new ObjectA
            {
                Primitive = primitive,
                Blah = blah,
                Meh = meh,
                SomeOther = someOther,
                YetAnother = yetAnother
            };
        }
    }
}
