namespace Library.Data
{
    public static class DataConstants
    {
        public static class Book
        {
            public const int MaxBookTitle = 50;
            public const int MinBookTitle = 10;

            public const int MaxBookAuthor = 50;
            public const int MinBookAuthor = 5;

            public const int MaxBookDescription = 5000;
            public const int MinBookDescription = 5;
        }

        public static class Category
        {
            public const int MaxCategoryName = 50;
            public const int MinCategoryName = 5;
        }
    }
}
