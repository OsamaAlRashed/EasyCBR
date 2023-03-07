using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCBR.Contract.IStage
{
    public interface IReviseStage<TEntity> 
        where TEntity : class
    {
        IRetainStage<TEntity> Retain();
        TEntity Run();

    }
}
