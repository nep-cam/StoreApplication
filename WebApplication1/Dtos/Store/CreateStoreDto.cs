using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.Store
{
    public class CreateStoreDto
    
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [DataType(DataType.Time)]
        public DateTime OpenTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime CloseTime { get; set; }
    }
}
