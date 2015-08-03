using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

    
        static void Main(string[] args)
        {
            //Console.WriteLine("Main threadId is:" +
            //                   Thread.CurrentThread.ManagedThreadId);
            //Message message = new Message();
            //Thread thread = new Thread(new ThreadStart(message.ShowMessage));
            //thread.Start();
            //Console.WriteLine("Do something ..........!");
            //Console.WriteLine("Main thread working is complete!");
            //Console.ReadLine();
            FunctionAsync();
        }



        public static async Task FunctionAsync()
        {
            Task<string> t1 = GetExpensiveStringAsync();
            Task<string> t2 = GetAnotherExpensiveStringAsync();

            await Task.WhenAll(t1, t2);

            string s1 = t1.Result;
            string s2 = t1.Result;
            Console.WriteLine(s1 + s2);
            Console.ReadLine();
        }

        private static Task<string> GetExpensiveStringAsync()
        {
            return Task<string>.Factory.StartNew(() => GetExpensiveString());
        }

        private static Task<string> GetAnotherExpensiveStringAsync()
        {
           // return Task<string>.Factory.StartNew(() => GetAnotherExpensiveString());

            return Task<string>.Factory.StartNew(() =>
            {
                for (int i = 0; i < 1; i++)
                    Thread.Sleep(1000); // allow other threads to jump in

                return DateTime.Now.ToLongTimeString();
            });
        }


        private static string GetExpensiveString()
        {
            for (int i = 0; i < 1; i++)
                Thread.Sleep(1000); // allow other threads to jump in

            return DateTime.Now.ToLongTimeString();
        }


        private static string GetAnotherExpensiveString()
        {
            for (int i = 0; i < 1; i++)
                Thread.Sleep(1000); // allow other threads to jump in

            return DateTime.Now.ToLongTimeString();
        }
        public void ShowMessage()
        {
            string message = string.Format("Async threadId is :{0}",
                                            Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(message);

            for (int n = 0; n < 10; n++)
            {
                Thread.Sleep(300);
                Console.WriteLine("The number is:" + n.ToString());
            }
        }


        public class Message
        {
            public void ShowMessage()
            {
                string message = string.Format("Async threadId is :{0}",
                                                Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(message);

                for (int n = 0; n < 10; n++)
                {
                    Thread.Sleep(300);
                    Console.WriteLine("The number is:" + n.ToString());
                }
            }
        }
    }
}
