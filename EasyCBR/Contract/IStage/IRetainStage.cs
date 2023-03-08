using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCBR.Contract.IStage
{
    public interface IRetainStage<TCase>
        where TCase : class
    {
        TCase Run();
    }
}
