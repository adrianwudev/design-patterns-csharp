using System;
using System.Threading.Tasks;

namespace AsynchronousFactoryMethod
{
    public class Foo
    {
        private string FooName;
        private Foo()
        {
            FooName = "FooString";
        }
        private async Task<Foo> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        public static Task<Foo> CreateAsync()
        {
            var result = new Foo();
            return result.InitAsync();
        }

        public override string ToString()
        {
            return $"{nameof(FooName)}: {FooName}";
        }
    }
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var x = await Foo.CreateAsync();
            Console.WriteLine(x);
        }
    }
}
