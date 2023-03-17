using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCBR.Models
{
    public class ResultCase<TCase>
    {
        public TCase Case { get; set; }
        public double Result { get; set; }
    }
}
