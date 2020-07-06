using System.Threading.Tasks;

namespace Common
{
    public interface IAuthorisationProvider
    {
        Task Assert();
    }

    public interface IAuthorisationComparisionProvider : IAuthorisationProvider
    {
    }

    public interface IAuthorisationLegacyProvider : IAuthorisationProvider
    {
    }

    public class AuthorisationLegacyProvider : IAuthorisationLegacyProvider
    {
        private readonly IContextProvider _contextProvider;

        public AuthorisationLegacyProvider(IContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public Task Assert()
        {
            var userId = _contextProvider.GetUserId();
            return Task.CompletedTask;
        }
    }

    public class AuthorisationComparisionProvider : IAuthorisationComparisionProvider
    {
        private readonly ICachedContextProvider _cachedContextProvider;
        private readonly IAuthorisationLegacyProvider _legacyProvider;

        public AuthorisationComparisionProvider(ICachedContextProvider cachedContextProvider,
            IAuthorisationLegacyProvider legacyProvider)
        {
            _cachedContextProvider = cachedContextProvider;
            _legacyProvider = legacyProvider;
        }

        public Task Assert()
        {
            var user = _cachedContextProvider.GetUserId();
            return Task.CompletedTask;
        }
    }
}