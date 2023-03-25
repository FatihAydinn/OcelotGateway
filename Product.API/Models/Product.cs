namespace Product.API.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public Product() { }

        //constructor bir classtan nesne oluştururken içindeki değişkenlere değer atamanızı sağlar
        public Product(int id, string title)
        {
            this.ID = id;
            this.Title = title;
        }
    }
}
