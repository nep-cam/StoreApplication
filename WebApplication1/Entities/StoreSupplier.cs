using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entities
{
    [Table("StoreSupplier")]
    public class StoreSupplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int StoreId { get; set; }
        public float Point { get; set; }
    }
}
