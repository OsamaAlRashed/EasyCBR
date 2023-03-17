namespace EasyCBR.Contract.IStage;

public interface IReuseStage<TCase>
    where TCase : class
{
    IReviseStage<TCase> Revise(object correctValue);
    IReviseStage<TCase> Revise();
    TCase Run();
}
