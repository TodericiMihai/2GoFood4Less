using System.ComponentModel.DataAnnotations;

namespace _2GoFood4Less.Server.Models.Utils.Photo.PhotoObjects
{
    abstract public class Photo
    {
        [Key]
        public string Id { get; set; }
        public byte[] Bytes { get; set; }
        public string Description { get; set; }
        public string FileExtension { get; set; }
        public decimal Size { get; set; }
    }
}
