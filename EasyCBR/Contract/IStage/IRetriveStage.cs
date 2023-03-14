using EasyCBR.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCBR.Contract.IStage
{
    public interface IRetriveStage<TCase>
        where TCase : class
    {
        IReuseStage<TCase> Reuse(ChooseType chooseType = ChooseType.MaxSimilarity);
        List<TCase> Run();
    }
}
