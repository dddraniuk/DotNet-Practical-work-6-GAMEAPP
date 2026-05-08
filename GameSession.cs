using GameApp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameProject
{
    public class GameSession
    {
        private List<GamePlayer> _players = new List<GamePlayer>();
        public int Round { get; private set; } = 1;
        public DateTime StartTime { get; private set; }

        public GameSession()
        {
            StartTime = DateTime.Now;
        }

        public void AddPlayer(GamePlayer player) => _players.Add(player);
        public void RemovePlayer(GamePlayer player) => _players.Remove(player);

        public void NextRound()
        {
            Round++;
            foreach (var p in _players.Where(p => p.IsAlive))
            {
                p.Heal(10);
                p.GainExperience(50);
            }
        }

        public List<GamePlayer> GetLeaderboard()
        {
            return _players.OrderByDescending(p => p.Level)
                           .ThenByDescending(p => p.Experience)
                           .ToList();
        }

        public string GetSessionStats()
        {
            TimeSpan duration = DateTime.Now - StartTime;
            int alive = _players.Count(p => p.IsAlive);
            int dead = _players.Count - alive;

            return $"Час: {duration.Minutes}хв {duration.Seconds}с | Раундiв: {Round} | Вижило: {alive} | Загинуло: {dead}";
        }

        public void PrintLeaderboard()
        {
            var lb = GetLeaderboard();
            Console.WriteLine("\n--- ТАБЛИЦЯ ЛiДЕРiВ ---");
            Console.WriteLine("{0,-15} | {1,-6} | {2,-8} | {3,-10}", "Нiк", "Рiвень", "Досвiд", "Статус");
            Console.WriteLine(new string('-', 50));
            foreach (var p in lb)
            {
                Console.WriteLine("{0,-15} | {1,-6} | {2,-8} | {3,-10}",
                    p.Nickname, p.Level, p.Experience, (p.IsAlive ? "Живий" : "Мертвий"));
            }
        }
    }
}