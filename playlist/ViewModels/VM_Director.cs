using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTwo_20151.Models;

namespace TestTwo_20151.ViewModels
{
    public static partial class ConfigureMaps
    {
        public static void ForDirector()
        {
            AutoMapper.Mapper.CreateMap<Models.Director, ViewModels.DirectorForList>();
            AutoMapper.Mapper.CreateMap<Models.Director, ViewModels.DirectorFull>();
            AutoMapper.Mapper.CreateMap<ViewModels.DirectorAdd, Models.Director>();
            AutoMapper.Mapper.CreateMap<ViewModels.DirectorFull, ViewModels.DirectorEditForm>();
            AutoMapper.Mapper.CreateMap<ViewModels.DirectorEdit, ViewModels.DirectorEditForm>();
        }
    }

    public class DirectorForList
    {
        public int Id { get; set; }

        [Display(Name = "Director Name")]
        [Required]
        public string Name { get; set; }
    }

    public class DirectorFull : DirectorForList
    {
        public DirectorFull()
        {
            this.Movies = new List<MovieForList>();
        }

        public IEnumerable<MovieForList> Movies { get; set; }
    }

    public class DirectorAddForm
    {

        [Display(Name = "Director Name")]
        [Required]
        public string Name { get; set; }

    }

    public class DirectorAdd
    {
        [Display(Name = "Director Name")]
        [Required]
        public string Name { get; set; }

    }

    public class DirectorEditForm
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Director Name")]
        [Required]
        public string Name { get; set; }

    }
    public class DirectorEdit
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}