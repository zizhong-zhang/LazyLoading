using Microsoft.Extensions.DependencyInjection;
using System;

namespace Common
{
    public interface IGuidGenerator
    {
        Guid GetGuid();
    }

    public class GuidGenerator : IGuidGenerator
    {
        private readonly IServiceProvider _serviceProvider;
        public GuidGenerator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Guid GetGuid()
        {
            return _serviceProvider.GetRequiredService<ITestGuidGenerator>().GetGuid();
        }
    }

    public interface ITestGuidGenerator : IGuidGenerator { }
    public class TestGuidGenerator : ITestGuidGenerator
    {
        private readonly Guid _guid = Guid.NewGuid();
        public Guid GetGuid()
        {
            return _guid;
        }
    }
}
