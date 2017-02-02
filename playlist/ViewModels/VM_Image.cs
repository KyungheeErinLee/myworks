using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace TestTwo_20151.ViewModels
{
    public static partial class ConfigureMaps
    {
        public static void ForImage()
        {
            AutoMapper.Mapper.CreateMap<Models.Image, ViewModels.ImageBase>();
            AutoMapper.Mapper.CreateMap<Models.Image, ViewModels.ImageForList>();
            AutoMapper.Mapper.CreateMap<Models.Image, ViewModels.ImageFull>();
       }
    }

    public class ImageAddForm
    {
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "ContentTypes")]
        public string ContentType { get; set; }

        [Display(Name = "Upload Image")]
        [Required]
        public string ImageFile { get; set; }

    }

    public class ImageAdd
    {
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "ContentType")]
        [Required]
        public string ContentType { get; set; }
        [Required]
        public HttpPostedFileBase ImageUpload { get; set; }
    }

    public class ImageBase : ImageAdd
    {
        public int Id { get; set; }

        [Display(Name = "Image")]
        public byte[] ImageFile { get; set; }
    }


    public class ImageFull : ImageBase
    {
        private string url = "";


        public ImageFull()
        {
            url = HttpContext.Current.Request.Url.GetLeftPart(System.UriPartial.Authority);
        }
        public string ImageUrl
        {
            get
            {
                return string.Format("{0}/image/{1}", url, Id);
            }
        }     
    }


    public class ImageForList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public byte[] ImageFile { get; set; }
    }
}