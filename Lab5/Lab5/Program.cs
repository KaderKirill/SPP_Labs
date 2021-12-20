using System.Reflection;

namespace FifthTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var assemblyLoader = new AssemblyLoader(Assembly.GetExecutingAssembly().Location);
            assemblyLoader.ShowPublicTypes();
        }
    }
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