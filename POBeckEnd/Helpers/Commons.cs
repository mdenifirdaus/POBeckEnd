namespace POBeckEnd.Helpers
{
    public static class Commons
    {
        private static Random random = new Random();
        public static string GeneratePO()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ012345678910";
            var number = new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return $"{DateTime.Now.Year}{DateTime.Now.Day}{number}";
        }
    }
}
