using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCBR.Contract.IStage
{
    public interface IReuseStage<TEntity>
        where TEntity : class
    {
        IReviseStage<TEntity> Revise(object correctValue);
        TEntity Run();

    }
}
