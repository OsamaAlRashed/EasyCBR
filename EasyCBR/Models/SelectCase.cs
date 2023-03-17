namespace EasyCBR.Models;

internal class SelectCase<TCase>
{
    internal TCase Case { get; set; }
    internal object Value { get; set; }
    internal double Similarity { get; set; }
}
