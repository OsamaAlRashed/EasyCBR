using System.Reflection;

namespace EasyCBR.Models;

public class ModelWithOutput<TCase>
    where TCase : class
{
    public CBR<TCase> Case { get; set; }
    public MemberInfo Output { get; set; }
}
