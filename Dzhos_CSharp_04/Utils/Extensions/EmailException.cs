using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dzhos_CSharp_04.Utils.Extensions
{
    internal class EmailException: MyException
    {
        public EmailException(string message) : base(message)
        {

        }
    }
}
