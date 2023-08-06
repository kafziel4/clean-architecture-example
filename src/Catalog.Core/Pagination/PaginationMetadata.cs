namespace Catalog.Core.Pagination
{
    public class PaginationMetadata
    {
        public int ItemCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }

        public PaginationMetadata(int totalItemCount, int pageSize, int currentPage)
        {
            ItemCount = totalItemCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            PageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        }
    }
}
