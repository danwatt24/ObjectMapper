using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectMapper
{
    public interface IObjectMapper
    {
        T map<T>(object simpleA) where T : new();
    }
}
