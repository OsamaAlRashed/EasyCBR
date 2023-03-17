namespace EasyCBR.Contract.IStage;

public interface IRetainStage<TCase>
    where TCase : class
{
    TCase Run();
}
