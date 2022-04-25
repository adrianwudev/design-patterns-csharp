using System;

namespace SingletonExercise
{
    public class SingletonTester
    {
        public static bool IsSingleton(Func<object> func)
        {
            // todo
            var obj1 = func.Invoke();
            var obj2 = func.Invoke();

            // ReferenceEquals(obj1, obj2);
            return obj1.Equals(obj2);
        }
        static void Main(string[] args)
        {
            Console.WriteLine();
        }
    }
}
