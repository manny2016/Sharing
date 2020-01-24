

namespace Sharing.Core.Entities
{
    public class MCard
    {
        public  long Id { get; set; }
        public  string CardId { get; set; }
        public  long MerchantId { get; set; }
        public  string BrandName { get; set; }
        public  string Title { get; set; }
        public  string Prerogative { get; set; }
        public  int Quantity { get; set; }
        public  int TotalQuantity { get; set; }
        public  decimal Discount { get; set; }
        public  string LogoUrl { get; set; }
        public  string RawData { get; set; }


        public string GenerateMySqlInsertValuesString()
        {
            return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9})",
                this.MerchantId,
                this.CardId.ToSqlValue(),                
                this.Quantity,
                this.TotalQuantity,
                this.Discount,
                this.Title.ToSqlValue(),
                this.BrandName.ToSqlValue(),
                this.Prerogative.ToSqlValue(),
                this.LogoUrl.ToSqlValue(),
                this.RawData.ToSqlValue()
            );
        }
    }
}
