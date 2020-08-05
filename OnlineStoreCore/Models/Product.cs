namespace OnlineStoreCore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int MinimumInventory { get; set; }
        public int GroupId { get; set; }
        public virtual ProductGroup Group { get; set; }
        public int WareHouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }

   
}