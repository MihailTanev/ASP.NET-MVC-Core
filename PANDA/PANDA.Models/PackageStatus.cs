namespace Panda.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PackageStatus
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
