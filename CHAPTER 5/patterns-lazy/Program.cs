using System;
using System.Diagnostics;
using System.Threading;

namespace patterns_lazy
{
class Program
{  
    static void Main(string[] args)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        //does not cause a delay
        Lazy<DemoClass> _lazy = new Lazy<DemoClass>();
        Console.WriteLine(sw.ElapsedMilliseconds);
        //Uncomment the below line to see the delay of 5 secs.
        //DemoClass _notLazy = new DemoClass();
        //calling the object property causes its lazy loading
        Console.WriteLine(_lazy.Value.DemoProperty);
        Console.WriteLine(sw.ElapsedMilliseconds);
        Console.Read();
    }
}
public class DemoClass
{
    public readonly string DemoProperty = "a value";
    public DemoClass()
    {
        Thread.Sleep(5000);
    }
}
}
