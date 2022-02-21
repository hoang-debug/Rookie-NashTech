namespace Day_9.Common
{
    public static class LambdaExtensions
    {
        public static Func<T, bool> Not<T>(this Func<T, bool> predicate)

        {

            return a => !predicate(a);

        }



        public static Func<T, bool> And<T>(this Func<T, bool> left, Func<T, bool> right)

        {

            return a => left(a) && right(a);

        }



        public static Func<T, bool> Or<T>(this Func<T, bool> left, Func<T, bool> right)

        {

            return a => left(a) || right(a);

        }
    }
}