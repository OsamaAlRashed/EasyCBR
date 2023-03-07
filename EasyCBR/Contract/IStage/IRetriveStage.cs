using EasyCBR.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCBR.Contract.IStage
{
    public interface IRetriveStage<TEntity>
        where TEntity : class
    {
        IReuseStage<TEntity> Reuse(ChooseType chooseType = ChooseType.Top);
        List<TEntity> Run();
    }
}
