using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCBR.Contract.IStage
{
    public interface IReviseStage<TCase> 
        where TCase : class
    {
        IRetainStage<TCase> Retain();
        TCase Run();

    }
}
