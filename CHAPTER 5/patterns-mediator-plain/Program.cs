using System;
using System.Linq;
using System.Collections.Generic;

namespace patterns_mediator_plain
{
    class Program
    {
        static void Main(string[] args){
            ConcreteMediator cm = new ConcreteMediator();
            Person a1 = new Adult("adult 1");            
            Person a2 = new Adult("adult 2");
            Person c1 = new Child("child 1");
            cm.Register(a1);
            cm.Register(a2);
            cm.Register(c1);
            a1.Send("Hello adults",audience.adult);
            a1.Send("Hello everyone");
            Console.Read();
        }
    }
   
    enum audience{
        adult,everyone
    }
    interface IMediator{
        public abstract void Register(Person p);        
        public abstract void Send(string from, string message, audience to);
    }
    class ConcreteMediator : IMediator{
        private Dictionary<string, Person> _persons =
            new Dictionary<string, Person>();       
        public void Register(Person p){
            _persons.TryAdd(p.Name, p);       
            p.concreteMediator = this;
        }
        public void Send(
            string from,string message, audience to){   
            
            var persons = (audience.adult == to) ? _persons.Values.Where(
                p=>p.Name != from && p.GetType().Equals(typeof(Adult))) : 
                _persons.Values.Where(p=>p.Name != from);
            if(persons.Count()>0)        
                foreach (var person in persons)
                    person.Receive(from, message);        
            
        }
    }
    class Person{        
        public string Name { get; }
        public Person(string name) => this.Name = name;
        public ConcreteMediator concreteMediator { set; get; }       
        public void Send(string message, audience to = audience.everyone){
            concreteMediator.Send(Name,message, to);
        }
        public void Receive(
            string from, string message){
            Console.WriteLine("{0} - received {1}: from {2}",
                Name,message,from);
        }
    }
    class Adult : Person{
        public Adult(string name): base(name){}       
    }
    class Child : Person{
        public Child(string name): base(name){}       
    }
}
