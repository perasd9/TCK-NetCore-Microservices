namespace TypesOfSportingEvents.API.Core.Pagination
{
    public class PaginationList<TEntity>
    {
        public IEnumerable<TEntity> Items { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public bool HasPrevious => PageIndex > 1;
        public bool HasNext => PageIndex < TotalPages;

        public PaginationList()
        {
            
        }

        public PaginationList(List<TEntity> items, int count, int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            PageIndex = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }
    }
}
