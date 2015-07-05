using System;

namespace SunLine.Manager.WebApi
{
    public class ValidationNotInitializedException : Exception
    {
        public ValidationNotInitializedException() { }
        public ValidationNotInitializedException(string message) : base(message) { }
        public ValidationNotInitializedException(string message, Exception inner) : base(message, inner) { }
    }
}