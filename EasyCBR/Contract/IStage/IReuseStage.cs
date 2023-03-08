using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCBR.Contract.IStage
{
    public interface IReuseStage<TCase>
        where TCase : class
    {
        IReviseStage<TCase> Revise(object correctValue);
        TCase Run();

    }
}
