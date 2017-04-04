namespace WebStore.Helpers
{
    public static class NumberConverter
    {
        public static int ConvertStringToInt(string value)
        {
            switch (value)
            {
                case "one":
                    return 1;
                case "two":
                    return 2;
                case "three":
                    return 3;
                case "four":
                    return 4;
                case "five":
                    return 5;
                default:
                    return 0;
            }
        }
    }
}