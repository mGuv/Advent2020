using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Advent2020.DI
{
    /// <summary>
    /// DI Container Builder for getting the applications DI layer
    /// </summary>
    public class ProviderBuilder
    {
        /// <summary>
        /// The internal Microsoft DI Container
        /// </summary>
        private readonly ServiceCollection container;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProviderBuilder()
        {
            this.container = new ServiceCollection();
        }

        /// <summary>
        /// Automatically searches the entire Assembly for injectable types and adds them.
        /// </summary>
        /// <returns>Self so it can be method chained</returns>
        public ProviderBuilder AutowireAssembly(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes()) {
                if (type.GetCustomAttributes(typeof(InjectableAttribute), true).Length > 0) {
                    this.container.AddSingleton(type);
                }
            }

            return this;
        }

        /// <summary>
        /// Creates the actual Container that can be utilised for DI
        /// </summary>
        /// <returns>The ready to use Container</returns>
        public IServiceProvider Build()
        {
            return this.container.BuildServiceProvider();
        }
    }
}
