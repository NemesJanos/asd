using System;
using System.Runtime.Serialization;

namespace BeKi
{
    [Serializable]
    internal class ABKivetel : Exception
    {
        public ABKivetel(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}