using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestTwo_20151.ViewModels
{

    public static partial class ConfigureMaps
    {

        public static void ForGenre()
        {           
            AutoMapper.Mapper.CreateMap<Models.Genre, ViewModels.GenreForList>();
            AutoMapper.Mapper.CreateMap<Models.Genre, ViewModels.GenreFull>();
            AutoMapper.Mapper.CreateMap<ViewModels.GenreAdd, Models.Genre>();
            AutoMapper.Mapper.CreateMap<ViewModels.GenreFull, ViewModels.GenreEditForm>();
            AutoMapper.Mapper.CreateMap<ViewModels.GenreEdit, ViewModels.GenreEditForm>();

        }
    }

    public class GenreForList
    {

        public int Id { get; set; }

        [Display(Name = "Genre Name")]
        [Required]
        public string Name { get; set; }


    }


    public class GenreFull : GenreForList
    {
        public GenreFull()
        {
            Movies = new List<MovieForList>();
        }

        public List<MovieForList> Movies { get; set; }


    }

    public class GenreAddFrom
    {

        [Display(Name = "Genre Name")]
        [Required]
        public string Name { get; set; }

        public MultiSelectList Movies { get; set; }

    }

    public class GenreAdd
    {
        [Display(Name = "Genre Name")]
        [Required]
        public string Name { get; set; }

        public List<int> MovieId { get; set; }

    }

    public class GenreEditForm
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Genre Name")]
        [Required]
        public string Name { get; set; }

        public MultiSelectList MovieList { get; set; }

    }

    public class GenreEdit
    {
        public int Id { get; set; }

        [Display(Name = "Genre Name")]
        [Required]
        public string Name { get; set; }
        public List<int> MovieId { get; set; }

    }
}