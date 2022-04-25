using NUnit.Framework;
using SingletonExercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.NUnitTests
{
    [TestFixture]
    public class SingletonExerciseTest
    {
        [Test]
        public void SingletonIsReferenceEqualTest()
        {
            var obj = new object();
            Assert.IsTrue(SingletonTester.IsSingleton(() => obj));
            Assert.IsFalse(SingletonTester.IsSingleton(() => new object()));
        }
    }
}
