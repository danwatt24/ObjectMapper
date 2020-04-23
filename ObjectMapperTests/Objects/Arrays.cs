using System;
using System.Collections.Generic;
using System.Text;
using static ObjectMapperTests.Objects.Objects;

namespace ObjectMapperTests.Objects
{
    public static class Arrays
    {
        public class SimpleArrayA
        {
            public int[] Primitive { get; set; }
        }

        public class SimpleArrayB
        {
            public int[] Primitive { get; set; }
        }

        public class SimpleArrayC
        {
            public string[] Primitive { get; set; }
        }

        public class ObjectArrayA
        {
            public ObjectA[] ObjectA { get; set; }
        }

        public class ObjectArrayB
        {
            public ObjectA[] ObjectA { get; set; }
        }
    }
}
