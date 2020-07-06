using System.Threading.Tasks;

namespace Common
{
    public interface IAuthorisation
    {
        Task Assert();
    }
    
    public class Authorisation : IAuthorisation
    {
        private readonly IAuthorisationProviderFactory _authorisationProviderFactory;
        private readonly ICachedContextProvider _cachedContextProvider;

        public Authorisation(ICachedContextProvider cachedContextProvider, IAuthorisationProviderFactory authorisationProviderFactory)
        {
            _cachedContextProvider = cachedContextProvider;
            _authorisationProviderFactory = authorisationProviderFactory;
        }

        public async Task Assert()
        {
            _cachedContextProvider.CacheContext();
            var authorisationProvider =  _authorisationProviderFactory.GetAuthorisationProvider(0);
            await authorisationProvider.Assert();
        }
    }
}