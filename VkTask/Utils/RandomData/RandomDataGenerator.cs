using System;
using System.Linq;

namespace VkTask.Utils.RandomData
{
    public class RandomDataGenerator
    {
        private static readonly Random random = new Random();
        public static string RandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int RandomInt32(int fromIncl, int toExcl) => random.Next(fromIncl, toExcl);
    }
}
