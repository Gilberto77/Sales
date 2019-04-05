namespace Sales.Common.Models
{
    using System;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Key]
        // Entity framework= cada tabla deve tener una clave numerica autoincrementable como clave primaria de la tabla
        public int ProductId
        {
            get;
            set;
        }
        public int? CategoryId { get; set; }
        
        [Required]    // Campo obligatorio
        [StringLength(50)]
        public string Description
        {
            get;
            set;
        }

        [DataType(DataType.MultilineText)]
        public string Remarks
        {
            get;
            set;
        }
        // Display es una anotacion muy util para cambiar nombre mas legible para el usuario
        [Display(Name = "Image")]
        public string ImagePath
        {
            get;
            set;
        }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public Decimal Price
        {
            get;
            set;
        }

        [Display(Name = "Is Available")]
        public bool IsAvailable
        {
            get;
            set;
        }

        [Display(Name = "Publish On")]
        [DataType(DataType.Date)]
        public DateTime PublishOn
        {
            get;
            set;
        }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude{get; set;}

            [ForeignKey("CategoryId")]
        [JsonIgnore]
        public virtual Category Category { get; set; }
        

        [NotMapped]
        public byte[] ImageArray { get; set; }

        public string ImageFullPath
        {
           get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "NoProduct";
                }
                return $"https://salesapi20181215084028.azurewebsites.net{this.ImagePath.Substring(1)}";
            }
        }

        public override string ToString()
        {
            return this.Description;
        }

    }
}
