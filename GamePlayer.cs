using System;

namespace GameApp
{
    public class GamePlayer
    {
        private int _healthPoints;

        public string Nickname { get; set; }
        public int Level { get; private set; }
        public int Experience { get; private set; }
        public int MaxHP { get; private set; }

        public static int PlayerCount { get; private set; } = 0;
        public static int MaxLevelRecord { get; private set; } = 1;

        public int HealthPoints
        {
            get { return _healthPoints; }
            set
            {
                if (value < 0) _healthPoints = 0;
                else if (value > MaxHP) _healthPoints = MaxHP;
                else _healthPoints = value;
            }
        }

        public bool IsAlive => HealthPoints > 0;
        public int ExperienceToNextLevel => Level * 100;
        public double ProgressPercent => (double)Experience / ExperienceToNextLevel * 100.0;

        public GamePlayer(string nickname)
        {
            Nickname = nickname;
            Level = 1;
            MaxHP = 100;
            HealthPoints = 100;
            Experience = 0;
            PlayerCount++;
            UpdateRecord();
        }

        public GamePlayer(string nickname, int level)
        {
            Nickname = nickname;
            Level = level;
            MaxHP = 100 + (level - 1) * 20;
            HealthPoints = MaxHP;
            Experience = 0;
            PlayerCount++;
            UpdateRecord();
        }

        public GamePlayer(GamePlayer other)
        {
            Nickname = other.Nickname;
            Level = other.Level;
            MaxHP = other.MaxHP;
            HealthPoints = other.HealthPoints;
            Experience = other.Experience;
            PlayerCount++;
            UpdateRecord();
        }

        private void UpdateRecord()
        {
            if (Level > MaxLevelRecord)
            {
                MaxLevelRecord = Level;
            }
        }

        public void TakeDamage(int dmg)
        {
            HealthPoints -= dmg;
        }

        public void Heal(int amount)
        {
            HealthPoints += amount;
        }

        public bool GainExperience(int xp)
        {
            Experience += xp;
            bool leveledUp = false;

            while (Experience >= ExperienceToNextLevel)
            {
                LevelUp();
                leveledUp = true;
            }

            return leveledUp;
        }

        private void LevelUp()
        {
            Experience -= ExperienceToNextLevel;
            Level++;
            MaxHP += 20;
            HealthPoints = MaxHP;
            UpdateRecord();
        }

        public override string ToString()
        {
            string status = IsAlive ? "Живий" : "Мертвий";
            return $"[Lv.{Level}] {Nickname} | HP: {HealthPoints}/{MaxHP} | XP: {Experience}/{ExperienceToNextLevel} ({ProgressPercent:F1}%) | {status}";
        }

        public static GamePlayer GetStrongest(GamePlayer[] players)
        {
            if (players == null || players.Length == 0) return null;

            GamePlayer strongest = players[0];
            for (int i = 1; i < players.Length; i++)
            {
                if (players[i].Level > strongest.Level)
                {
                    strongest = players[i];
                }
                else if (players[i].Level == strongest.Level && players[i].HealthPoints > strongest.HealthPoints)
                {
                    strongest = players[i];
                }
            }
            return strongest;
        }

        public static void SimulateBattle(GamePlayer p1, GamePlayer p2)
        {
            Console.WriteLine($"\n--- Початок бою: {p1.Nickname} vs {p2.Nickname} ---");

            while (p1.IsAlive && p2.IsAlive)
            {
                int dmg1 = Random.Shared.Next(15, 35);
                p2.TakeDamage(dmg1);
                Console.WriteLine($"{p1.Nickname} завдає {dmg1} шкоди. {p2.Nickname} HP: {p2.HealthPoints}");

                if (!p2.IsAlive) break;

                int dmg2 = Random.Shared.Next(15, 35);
                p1.TakeDamage(dmg2);
                Console.WriteLine($"{p2.Nickname} завдає {dmg2} шкоди. {p1.Nickname} HP: {p1.HealthPoints}");
            }

            GamePlayer winner = p1.IsAlive ? p1 : p2;
            Console.WriteLine($"Бiй завершено! Переможець: {winner.Nickname}");
        }
    }
}