using System;
using System.Reflection;

namespace EighthTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var assemblyLoader = new AssemblyLoader(Assembly.GetExecutingAssembly().Location);
            assemblyLoader.ShowPublicTypes();
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ExportClassAttribute : Attribute { }

    [ExportClass]
    public class PublicClass1 { }

    [ExportClass]
    public class PublicClass2 { }

    public enum PublicEnum { };

    internal class InternalClass { }
}

namespace B
{
    public class PublicClass1 { }
    public class PublicClass2 { }

    public enum PublicEnum { };

    internal class InternalClass { }
}

public class PublicClass1 { }
public class PublicClass2 { }

public enum PublicEnum { };

internal class InternalClass { }