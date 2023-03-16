namespace EasyCBR.Contract.IStage
{
    public interface IReuseStage<TCase>
        where TCase : class
    {
        IReviseStage<TCase> Revise(object correctValue = default);
        TCase Run();
    }
}
