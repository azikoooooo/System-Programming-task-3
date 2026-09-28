using System;
using System.Threading;

namespace System_Programmin_task3
{
    internal class Program
    {
        static object lockObject = new object();

        static string cardNumber = "4169738820844003";
        static string cardHolder = "Azer Allahverdiyev";
        static double balance = 150.0;
        static int timer = 30;

        static void InsertCard(string card)
        {
            cardNumber = card;
            Console.WriteLine($"[ATM] Kart daxil edildi: {cardNumber}");
            Console.WriteLine("---------------------------------------------");
        }

        static void LoadData()
        {
            Console.WriteLine("[ATM] Melumatlar yuklenir...");
            Thread.Sleep(500);

            Console.WriteLine($"Kart sahibi: {cardHolder}");
            Console.WriteLine($"Cari balans: {balance} azn");
            Console.WriteLine($"Sessiya: {timer}");
            Console.WriteLine("---------------------------------------------");
        }

        static void TransferMoney(object? client)
        {
            double amount = 100.0;

            Console.WriteLine($"{client} {amount} azn kocurmek ucun muraciet etdi...");

            lock (lockObject)
            {
                Console.WriteLine($"--> {client} emeliyyata basladi.");

                if (balance >= amount)
                {
                    Thread.Sleep(300);
                    balance -= amount;
                    Console.WriteLine($"[UGURLU] {client} terefinden {amount} azn kocuruldu. Qalan balans: {balance} azn");
                }
                else
                {
                    Console.WriteLine($"[UGURSUZ] {client} ucun balansda kifayet qeder vesait yoxdur! Cari balans: {balance} azn");
                }

                Console.WriteLine($"<-- {client} emeliyyati bitirdi.");
                Console.WriteLine("---------------------------------------------");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("========== ATM TERMINAL ==========\n");

            InsertCard("4169738820844003");

            LoadData();

            Thread thread1 = new Thread(TransferMoney);
            Thread thread2 = new Thread(TransferMoney);

            thread1.Start("Emeliyyat 1 (ATM)");
            thread2.Start("Emeliyyat 2 (Online Bank)");

            thread1.Join();
            thread2.Join();

            Console.WriteLine("\nButun emeliyyatlar tamamlandi.");
            Console.WriteLine($"Yekun balans: {balance} azn");

            Console.ReadLine();
        }
    }
}
