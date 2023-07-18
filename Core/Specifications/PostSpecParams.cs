namespace Core.Specifications
{
    /* 
    This class is the represents the potential query parameters that can be used by the post specification to apply various operations such as 
    sorting, filtering, searching and pagination
    */
    public class PostSpecParams
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 6;
        private string _search;

        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int? CategoryId { get; set; }
        public string Sort { get; set; }
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}