using System;
using System.Threading;
using System.Threading.Tasks;

namespace patterns_singleton
{
    class Program{
        static void Main(string[] args){            
            Parallel.For(0, 10, i =>{
                    Console.WriteLine("Iteration {0} - Identifier {1}",i,
                        ThreadSafeSingletonExample.instance.identitier);
                    Console.WriteLine("Iteration {0} - Identifier {1}", i,
                        NotThreadSafeSingletonExample.instance.identitier);
                });           
            
            Console.Read();
        }
    }

    class ThreadSafeSingletonExample{
        static readonly ThreadSafeSingletonExample _instance = 
            new ThreadSafeSingletonExample();
        static Guid _id = Guid.Empty;
        static ThreadSafeSingletonExample(){
            _id = Guid.NewGuid();
            Thread.Sleep(100);
        }
        public static ThreadSafeSingletonExample instance{
            get{
                return _instance;
            }
        }
        public string identitier { get { return _id.ToString(); } }
    }
    class NotThreadSafeSingletonExample{
        static NotThreadSafeSingletonExample _instance = null;
        static Guid _id = Guid.Empty;

        private NotThreadSafeSingletonExample(){
            Thread.Sleep(100);
            _id = Guid.NewGuid();
        }
        public static NotThreadSafeSingletonExample instance{
            get{
                if (_instance == null)
                    _instance = new NotThreadSafeSingletonExample();
                return _instance;
            }
        }
        public string identitier{get{return _id.ToString();}}
    }
}
