using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectMapper
{
    public interface IObjectMapper
    {
        Dest Map<Dest>(object source);
        object Map(object source, Type mapTo);
    }
}
