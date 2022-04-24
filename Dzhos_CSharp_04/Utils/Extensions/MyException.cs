using System;

namespace Dzhos_CSharp_04.Utils.Extensions
{
    internal class MyException: Exception
    {
        public MyException(string message) : base(message)
        {

        }
    }
}
