using System;
using Microsoft.Extensions.DependencyInjection;

namespace Common
{
    public interface IAuthorisationProviderFactory
    {
        IAuthorisationProvider GetAuthorisationProvider(int num);
    }
    
    public class AuthorisationProviderFactory : IAuthorisationProviderFactory
    {
        private readonly Lazy<IAuthorisationComparisionProvider> _comparisionProvider;
        private readonly Lazy<IAuthorisationLegacyProvider> _legacyProvider;

        public AuthorisationProviderFactory(Lazy<IAuthorisationComparisionProvider> comparisionProvider, Lazy<IAuthorisationLegacyProvider> legacyProvider)
        {
            _comparisionProvider = comparisionProvider;
            _legacyProvider = legacyProvider;
        }

        public IAuthorisationProvider GetAuthorisationProvider(int number)
        {
            switch (number)
            {
                case 0:
                    return _comparisionProvider.Value;
                default:
                    return _legacyProvider.Value;
            }
        }
    }
}