namespace Places.API.Endpoints.QueryParameters.Base
{
    public abstract class Params
    {
        public int PageNumber { get; set; } = 1;

        private int _pageSize = int.MaxValue;
        public int PageSize { get => _pageSize; set => _pageSize = value; }
    }
}
