using System;

namespace Advent2020.DI
{
    /// <summary>
    /// Attribute to mark classes as injectable. Intended to be auto-registered as a Singleton within the Application
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectableAttribute : Attribute
    {
    }
}
