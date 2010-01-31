using System;

namespace WebApplication1
{
    public class NotValidRedirectException : Exception
    {
        public NotValidRedirectException(string s)
            : base(s)
        {
            
        }
    }
}