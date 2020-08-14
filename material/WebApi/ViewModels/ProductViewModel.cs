using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        [Range(0,999)]
        public decimal Weight{ get; set; }

        public object Category{ get; set; }

        public decimal ListPrice { get; set; }

    }
}
