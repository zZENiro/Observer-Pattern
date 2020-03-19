using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Observer.ExampleProj
{
    public abstract class Displayer
    {
        public Displayer(Observable observable)
        {
            _observable = observable;
        }

        protected Observable _observable;

        public abstract void Display();
    }

    public abstract class Observer
    {
        public Observer(Observable observalbe)
        {
            _observalbe = observalbe;
        }

        public abstract void Update();

        protected Observable _observalbe;
    }

    public abstract class Observable
    {
        public List<Observer> Observers { get => _observers; set => _observers = value; }

        public Observable()
        {
            _observers = new List<Observer>();
        }

        public void UpdateAll()
        {
            foreach (var observer in Observers)
                observer.Update();
        }

        protected List<Observer> _observers;
    }

    public class WeatherStation : Observable
    {
        public WeatherStation() : base()
        { }

        public int GetTemperature() => new Random().Next(0, 30);

        public int GetHumidity() => new Random().Next(0, 100);
    }

    public class TimeManager : Observable
    {
        public TimeManager() : base()
        { }

        public DateTime GetDate() => DateTime.Now;
    }

    public class TimeDisplayer : Displayer
    {
        public TimeDisplayer(Observable observable) : base(observable)
        { }

        public override void Display()
        {
            var timeManger = _observable as TimeManager;

            var dateInfo = timeManger.GetDate();

            Console.WriteLine($"{dateInfo.Year}:{dateInfo.Month}:{dateInfo.Day}:{dateInfo.Hour}:{dateInfo.Minute}:{dateInfo.Second}:{dateInfo.Millisecond}");
        }
    }

    public class WeatherDisplayer : Displayer
    {
        public WeatherDisplayer(Observable observable) : base(observable)
        { }

        public override void Display()
        {
            var weatherManager = _observable as WeatherStation;

            Console.WriteLine($"Temperature: {weatherManager.GetTemperature()}\tHumidity: {weatherManager.GetHumidity()}");
        }
    }

    public class WeatherObserver : Observer
    {
        Displayer _weatherDisplayer;

        public WeatherObserver(Observable observalbe) : base(observalbe)
        {
            _weatherDisplayer = new WeatherDisplayer(observalbe as WeatherStation);
        }

        public override void Update()
        {
            Console.WriteLine($"Weaher Updated...");
            _weatherDisplayer.Display();
        }
    }

    public class TimeObserver : Observer
    {
        Displayer _timeDisplayer;

        public TimeObserver(Observable observalbe) : base(observalbe)
        {
            _timeDisplayer = new TimeDisplayer(observalbe as TimeManager);
        }

        public override void Update()
        {
            Console.WriteLine("Time Updated...");
            _timeDisplayer.Display();
        }
    }

    public class Program
    {
        static void Main()
        {
            WeatherStation weatherStation = new WeatherStation();
            TimeManager timeManager = new TimeManager();

            TimeObserver timeObserver = new TimeObserver(timeManager);
            WeatherObserver weatherObserver = new WeatherObserver(weatherStation);

            weatherStation.Observers.Add(weatherObserver);
            timeManager.Observers.Add(timeObserver);

            for (int i = 0; i < 10; ++i)
            {
                weatherStation.UpdateAll();
                timeManager.UpdateAll();

                Thread.Sleep(500);

                Console.WriteLine("");
            }
        }
    }
}
