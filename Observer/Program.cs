using System;
using System.Collections.Generic;

namespace Observer
{
    public interface IObserver
    {
        void Update();
    }

    public interface IObservalbe
    {
        ICollection<IObserver> Observers { get; set; }

        void UpdateAll();
    }

    public class ConcreteObservable : IObservalbe
    {
        ICollection<IObserver> IObservalbe.Observers { get => _observers; set => _observers = value as List<IObserver>; }

        public ConcreteObservable()
        {
            _observers = new List<IObserver>();
        }

        public void UpdateAll()
        {
            foreach (var observer in _observers)
                observer.Update();
        }

        private List<IObserver> _observers { get; set; }
    }

    public class ConcreteObserverA : IObserver
    {
        public void Update() => Console.WriteLine("ConcreteObserverA - Updated!");
        
    }

    public class ConcreteObserverB : IObserver
    {
        public void Update() => Console.WriteLine("ConcreteObserverB - Updated!");
    }

    public class ConcreteObserverC : IObserver
    {
        public void Update() => Console.WriteLine("ConcreteObserverC - Updated!");
    }

    public class ConcreteObserverD : IObserver
    {
        public void Update() => Console.WriteLine("ConcreteObserverD - Updated!");
    }

    //public class Program
    //{
    //    static void Main()
    //    {
    //        IObservalbe observalbeObj = new ConcreteObservable();

    //        observalbeObj.Observers.Add(new ConcreteObserverA());
    //        observalbeObj.Observers.Add(new ConcreteObserverB());
    //        observalbeObj.Observers.Add(new ConcreteObserverC());
    //        observalbeObj.Observers.Add(new ConcreteObserverD());
    //        observalbeObj.Observers.Add(new ConcreteObserverD());
    //        observalbeObj.Observers.Add(new ConcreteObserverD());
    //        observalbeObj.Observers.Add(new ConcreteObserverD());


    //        observalbeObj.UpdateAll();
    //    }
    //}


}