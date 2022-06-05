using Autofac;
using System;

namespace DecoratorInDependencyInjection
{
    public interface IReportingService
    {
        public void Report();
    }

    public class ReportingService : IReportingService
    {
        public void Report()
        {
            Console.WriteLine("Here is your report");
        }
    }

    public class SuccessService : IReportingService
    {
        public void Report()
        {
            Console.WriteLine("The action is successful");
        }
    }

    public class ReportingServiceWithLogging : IReportingService
    {
        private IReportingService _decorated;

        public ReportingServiceWithLogging(IReportingService decorated)
        {
            _decorated = decorated;
        }

        public void Report()
        {
            Console.WriteLine("Commencing log...");
            _decorated.Report();
            Console.WriteLine("Ending log...");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<ReportingService>().Named<IReportingService>("reporting");
            b.RegisterDecorator<IReportingService>(
                (context, service) => new ReportingServiceWithLogging(service), "reporting"
                );

            using (var c = b.Build())
            {
                var r = c.Resolve<IReportingService>();
                r.Report();
            }
        }
    }
}
