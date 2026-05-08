using GameApp;
using System;
using System.Threading;

namespace GameProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
         
            GamePlayer p1 = new GamePlayer("Pudge");
            GamePlayer p2 = new GamePlayer("DragonSlayer", 3);
            GamePlayer p3 = new GamePlayer("Slark", 2);

            Console.WriteLine("--- 1. ПОЧАТКОВi ГРАВЦi ---");
            Console.WriteLine(p1);
            Console.WriteLine(p2);
            Console.WriteLine(p3);

            
            Console.WriteLine("\n--- 2. ДЕМОНСТРАЦiЯ LEVEL UP (Pudge +150 XP) ---");
            bool isUp = p1.GainExperience(150);
            Console.WriteLine($"Чи вiдбувся Level Up: {isUp}");
            Console.WriteLine(p1); 

            
            Console.WriteLine("\n--- 3. СИМУЛЯЦiЯ БОЮ ---");
            GamePlayer.SimulateBattle(p1, p2);

            
            GamePlayer winner = p1.IsAlive ? p1 : p2;
            Console.WriteLine($"\nПеремiг: {winner.Nickname}. Зцiлюємо його наполовину...");
            winner.Heal(winner.MaxHP / 2);
            Console.WriteLine($"Статус переможця пiсля лiкування: {winner}");

          
            Console.WriteLine("\n=== 4. СТАРТ iГРОВОЇ СЕСiЇ (GameSession) ===");
            GameSession session = new GameSession();
            session.AddPlayer(p1);
            session.AddPlayer(p2);
            session.AddPlayer(p3);

            Console.WriteLine("Проводимо 2 раунди (всiм живим +10 HP та +50 XP)...");
            session.NextRound(); 
            session.NextRound(); 

            
            Thread.Sleep(2000);

          
            session.PrintLeaderboard();

            Console.WriteLine("\n--- 5. ФiНАЛЬНА СТАТИСТИКА СЕСiЇ ---");
            Console.WriteLine(session.GetSessionStats());

            Console.WriteLine("\n--- 6. ГЛОБАЛЬНi СТАТИЧНi ДАНi ---");
            Console.WriteLine($"Загальна кiлькiсть створених об'єктiв: {GamePlayer.PlayerCount}");
            Console.WriteLine($"Рекордний рiвень серед усiх: {GamePlayer.MaxLevelRecord}");
        }
    }
}