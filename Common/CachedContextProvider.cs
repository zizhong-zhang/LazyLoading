using System;

namespace Common
{
    public interface ICachedContextProvider
    {
        void CacheContext();
        Guid GetUserId();
    }
    
    public class CachedContextProvider : ICachedContextProvider
    {
        private readonly IContextProvider _contextProvider;
        private Guid _userId;

        public CachedContextProvider(IContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public void CacheContext()
        {
            _userId = _contextProvider.GetUserId();
        }

        public Guid GetUserId()
        {
            return _userId;
        }
    }
}