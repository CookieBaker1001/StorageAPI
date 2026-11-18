namespace StorageAPI.Models
{
    public class DataStats
    {
        public int TotalItemCount { get; set; }
        public int TotalItemPrice { get; set; }
        public decimal MedianPrice { get; set; }

        public DataStats(int totalItemCount, int totalItemPrice, decimal medianPrice)
        {
            TotalItemCount = totalItemCount;
            TotalItemPrice = totalItemPrice;
            MedianPrice = medianPrice;
        }
    }
}
