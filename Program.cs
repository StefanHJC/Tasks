namespace Lesson
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathfinder1 = new Pathfinder(ChainOfLoggers.Create(new FileLogWritter()));
            var pathfinder2 = new Pathfinder(ChainOfLoggers.Create(new ConsoleLogWritter()));
            var pathfinder3 = new Pathfinder(ChainOfLoggers.Create(new SecureLogWritter(new FileLogWritter())));
            var pathfinder4 = new Pathfinder(ChainOfLoggers.Create(new SecureLogWritter(new ConsoleLogWritter())));
            var pathfinder5 = new Pathfinder(ChainOfLoggers.Create(new ConsoleLogWritter(), new SecureLogWritter(new FileLogWritter())));
        }
    }

    interface ILogger
    {
        public abstract void WriteError(string message);
    }

    class ConsoleLogWritter : ILogger
    {
        public virtual void WriteError(string message)
        {
            Console.WriteLine(message);
        }
    }

    class FileLogWritter : ILogger
    {
        public virtual void WriteError(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }

    class SecureLogWritter : ILogger
    {
        private ILogger _baseLogger;

        public SecureLogWritter(ILogger baseLogger)
        {
            _baseLogger = baseLogger;
        }

        public virtual void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                _baseLogger.WriteError(message);
        }
    }

    class ChainOfLoggers : ILogger
    {
        private IEnumerable<ILogger> _loggers;

        public ChainOfLoggers(IEnumerable<ILogger> loggers)
        {
            _loggers = loggers;
        }

        public void WriteError(string message)
        {
            foreach (var logger in _loggers)
                logger.WriteError(message);
        }

        public static ChainOfLoggers Create(params ILogger[] loggers)
        {
            return new ChainOfLoggers(loggers);
        }
    }

    class Pathfinder
    {
        private ChainOfLoggers _loggers;

        public Pathfinder(ChainOfLoggers loggers)
        {
            _loggers = loggers;
        }

        public void Find()
        {
            _loggers.WriteError("гыыыы");
        }
    }
}