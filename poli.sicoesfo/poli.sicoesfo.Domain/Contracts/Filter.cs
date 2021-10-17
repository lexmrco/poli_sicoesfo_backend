
namespace poli.sicoesfo.Domain.Contracts
{
    public abstract class Filter
    {
         
        public Filter()
        {
            maxLimit = 1000;
            ItemsPerpage = defaultLimit;
            Page = defaultPage;
        }
        const int defaultLimit = 20;
        const int defaultPage = 1;
        readonly int maxLimit;
        int _itemsPerPage;
        public int ItemsPerpage
        {
            get { return _itemsPerPage; }
            set { _itemsPerPage = value <= maxLimit ? value: maxLimit; }
        }
        public int Page { get; set; }
        public string[] SorttBy { get; set; }
        public bool[] SorttByDesc { get; set; }
    }
}
