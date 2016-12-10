using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCore.Extensions
{
    public static class NumberExtensions
    {
        public static int? Reverse(this int? value)
        {
            return value*-1;
        }
    }
}
