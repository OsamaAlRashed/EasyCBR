using System.Reflection;

namespace EasyCBR.Models;

public class ModelWithOutput<TCase, TOutput>
    where TCase : class
{
    public CBR<TCase, TOutput> Case { get; set; }
    public MemberInfo Output { get; set; }
}
