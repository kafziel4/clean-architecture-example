namespace Catalog.Core.Pagination
{
    public class PaginationMetadata
    {
        public int ItemCount { get; private set; }
        public int PageSize { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageCount { get; private set; }

        public PaginationMetadata(int itemCount, int pageSize, int currentPage)
        {
            ItemCount = itemCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            PageCount = (int)Math.Ceiling(itemCount / (double)pageSize);
        }
    }
}
