using System;

namespace Common
{
    public interface IContextProvider
    {
        Guid GetUserId();
    }
    
    public class ContextProvider : IContextProvider
    {
        private readonly Guid _userId = Guid.NewGuid();

        public Guid GetUserId()
        {
            return _userId;
        }
    }
}