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

        public static void ForMovie()
        {
            AutoMapper.Mapper.CreateMap<Models.Movie, ViewModels.MovieFull>();           
            AutoMapper.Mapper.CreateMap<Models.Movie, ViewModels.MovieForList>();
            AutoMapper.Mapper.CreateMap<ViewModels.MovieAdd, Models.Movie>();
            AutoMapper.Mapper.CreateMap<ViewModels.MovieFull, ViewModels.MovieEditForm>();
            AutoMapper.Mapper.CreateMap<ViewModels.MovieEdit, ViewModels.MovieEditForm>();
        }
    }

    public class MovieForList
    {
        public int Id { get; set; }

        [Display(Name = "Movie Title")]
        [Required]
        public string MovieTitle { get; set; }

    }

    /// <summary>
    /// MovieFull ViewModel to be used in Details 
    /// </summary>
    public class MovieFull : MovieForList
    {
        public MovieFull()
        {
            this.Genres = new List<GenreForList>();
        }

        [Display(Name = "Ticket Price")]
        public decimal TicketPrice { get; set; }

        [Display(Name = "Director Name")]
        public Director Director { get; set; }

        [Display(Name = "Genre Name")]
        public IEnumerable<GenreForList> Genres { get; set; }

    }
    public class MovieAddForm
    {

        [Display(Name = "Movie Title")]
        [Required]
        public string MovieTitle { get; set; }

        [Display(Name = "Ticket Price")]
        [Required]
        public decimal TicketPrice { get; set; }
        public MultiSelectList Genres { get; set; }
        public SelectList Director { get; set; }
    }
    public class MovieAdd
    {
        [Display(Name = "Movie Title")]
        [Required]
        public string MovieTitle { get; set; }

        [Display(Name = "Ticket Price")]
        [Required]
        public decimal TicketPrice { get; set; }
        public int DirectorId { get; set; }
        public List<int> GenreId { get; set; }

    }

    public class MovieEditForm
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        public string MovieTitle { get; set; }

        [Display(Name = "Ticket Price")]
        [Required]
        public decimal TicketPrice { get; set; }

        public MultiSelectList GenreList { get; set; }
        public SelectList DirectorList { get; set; }

    }

    public class MovieEdit
    {
        public int Id { get; set; }

        [Required]
        public string MovieTitle { get; set; }

        [Display(Name = "Ticket Price")]
        [Required]
        public decimal TicketPrice { get; set; }
        public int DirectorId { get; set; }
        public List<int> GenreId { get; set; }

    }
}