using System;
namespace VismaUserCore.Exceptions
{
    public class BussinessException : Exception
    {
        public BussinessException()
        {

        }

        public BussinessException(string message) : base(message)
        {

        }


    }
}
