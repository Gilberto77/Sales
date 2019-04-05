﻿namespace Sales.Common.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        // [Required]  [StringLength(50)] public string Nombre{ get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }


        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                    
            {
                    return "noproduct";
                }
                return $"https://salesbackend20181215081959.azurewebsites.net{this.ImagePath.Substring(1)}";
            }
        }
    }
}
