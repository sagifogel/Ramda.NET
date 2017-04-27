using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramda.NET
{
    public interface IDispersible<TSeperator, TResult>
    {
        TResult Intersperse(TSeperator seperator);
    }
}
