using System;
using System.Linq;
using Moq;

namespace WhatsI
{
    class Program
    {
        static void Main(string[] args)
        {
            var i = 3;

            i += Calc1(i);

            i += Calc2(out i, i);            

            var mockedTestClass = new Mock<ITestClass>();
            mockedTestClass.SetReturnsDefault(25);
            mockedTestClass.Setup(m => m.Calc3(It.IsAny<int>())).Callback(() => i = 12);

            i += mockedTestClass.Object.Calc3(i);

            var places = new[] { "home", "work", "office" };
            i += Calc4(new Predicate<int>(v => places.Length == 3), i);

            Console.WriteLine(i);
        }

        private static int Calc1(int v)
        {
            return Enumerable.Range(1, v).Select(x => x).Sum();
        }

        private static int Calc2(out int x, int v)
        {
            x = 45;
            v++;
            return v;
        }

        private static int Calc4(Predicate<int> predicate, int v)
        {
            return predicate(v) ? 1 : 0;
        }
    }

    public interface ITestClass
    {
        int Calc3(int v);
    }

    public class TestClass : ITestClass
    {
        public int Calc3(int v)
        {
            return v + v;
        }
    }
}
