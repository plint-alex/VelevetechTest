namespace BusinessLayer.CommonContracts
{
    public class PageInfoContract
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string SortByField { get; set; }

        public bool Desc { get; set; }
    }
}
