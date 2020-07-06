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
        private readonly IServiceProvider _serviceProvider;

        public AuthorisationProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IAuthorisationProvider GetAuthorisationProvider(int number)
        {
            switch (number)
            {
                case 0:
                    return _serviceProvider.GetRequiredService<IAuthorisationComparisionProvider>();
                default:
                    return _serviceProvider.GetRequiredService<IAuthorisationLegacyProvider>();
            }
        }
    }
}