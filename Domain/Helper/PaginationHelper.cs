namespace TaskManagement.Domain.Helper
{
    public static class PaginationHelper
    {
        public static int CalculateTotalPages(int totalItems, int pageSize)
        {
            if (totalItems <= 0)
                return 1;
            return (int)Math.Ceiling((double)totalItems / pageSize);
        }
    }
}
