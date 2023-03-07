using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCBR.Contract.IStage
{
    public interface IRetainStage<TEntity>
        where TEntity : class
    {
        TEntity Run();
    }
}
