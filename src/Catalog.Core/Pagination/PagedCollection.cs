namespace Catalog.Core.Pagination
{
    public class PagedCollection<T>
    {
        public IEnumerable<T> Collection { get; private set; }
        public PaginationMetadata PaginationMetadata { get; private set; }

        public PagedCollection(IEnumerable<T> collection, int itemCount, int pageSize, int currentPage)
        {
            Collection = collection;
            PaginationMetadata = new PaginationMetadata(itemCount, pageSize, currentPage);
        }
    }
}
