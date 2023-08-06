namespace Catalog.Core.Pagination
{
    public class PagedCollection<T>
    {
        public IEnumerable<T> Collection { get; set; }
        public PaginationMetadata PaginationMetadata { get; set; }

        public PagedCollection(IEnumerable<T> collection, PaginationMetadata paginationMetadata)
        {
            Collection = collection;
            PaginationMetadata = paginationMetadata;
        }
    }
}
