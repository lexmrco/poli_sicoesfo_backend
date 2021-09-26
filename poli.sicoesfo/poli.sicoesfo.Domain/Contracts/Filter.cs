
namespace poli.sicoesfo.Domain.Contracts
{
    public abstract class Filter
    {
        public Filter()
        {
            defaultLimit = 20;
            maxLimit = 1000;
        }
        readonly int defaultLimit;
        readonly int maxLimit;
        int _limit;
        public int Limit {
            get { return _limit > 0 ? _limit : defaultLimit; }
            set { _limit = value <= maxLimit ? value: maxLimit; }
        }
    }
}
