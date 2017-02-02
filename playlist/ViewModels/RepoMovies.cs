using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTwo_20151.Models;


namespace TestTwo_20151.ViewModels
{
    public class RepoMovie : RepositoryBase
    {
        /// <summary>
        /// Creates List of MovieForList to be presented in the Movie List View
        /// </summary>
        /// <returns>List of MovieForList</returns>
        public IEnumerable<MovieForList> GetMoviesForList()
        {
            var fetchedObjects = dc.Movies.OrderBy(movie => movie.MovieTitle);

            return Mapper.Map<IEnumerable<MovieForList>>(fetchedObjects); ;
        }

        public IEnumerable<MovieFull> AllMovies()
        {
            var fetchedObjects = dc.Movies.Include("Genres").Include("Director").OrderBy(a => a.MovieTitle);

            return Mapper.Map<IEnumerable<MovieFull>>(fetchedObjects);
        }



        /// <summary>
        /// Creates a MovieFull object based on provided Id
        /// </summary>
        /// <param name="id">Movie Id</param>
        /// <returns>MovieFull object based on id</returns>
        public MovieFull GetMovieFull(int? id)
        {

            var fetchedObjects = dc.Movies.Include("Genres").Include("Director").FirstOrDefault(m => m.Id == id);
            return Mapper.Map<MovieFull>(fetchedObjects);
        }

        public MovieFull AddMovie(MovieAdd newItem)
        {
            Director director = dc.Directors.FirstOrDefault(m => m.Id == newItem.DirectorId);

            List<Genre> genres = new List<Genre>();
            foreach (var item in newItem.GenreId)
            {
                genres.Add(dc.Genres.FirstOrDefault(m => m.Id == item));
            }

            var addedItem = dc.Movies.Add(Mapper.Map<Movie>(newItem));
            addedItem.Director = director;
            addedItem.Genres = genres;

            dc.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<MovieFull>(addedItem);
        }

        public SelectList getSelectMovieList()
        {
            SelectList moviesList = new SelectList(dc.Movies, "Id", "MovieTitle");
            return moviesList;
        }

        public MultiSelectList getMultiSelectMoviesList()
        {
            MultiSelectList moviesList = new MultiSelectList(dc.Movies, "Id", "MovieTitle");
            return moviesList;
        }

        public bool DeletMovieById(int id)
        {
            var itemToDelete = dc.Movies.Include("Genres").Include("Director").SingleOrDefault(a => a.Id == id);
            if (itemToDelete == null)
            {
                return false;
            }
 
            dc.Movies.Remove(itemToDelete);
            dc.SaveChanges();

            return true;
        }


        public MovieFull EditMovie(MovieEdit newItem)
        {
            var fetchedObject = dc.Movies.Include("Genres").Include("Director").SingleOrDefault(a => a.Id == newItem.Id);

            if (fetchedObject == null)
            {
                return null;
            }
            else
            {
                dc.Entry(fetchedObject).CurrentValues.SetValues(newItem);
                Director director = dc.Directors.FirstOrDefault(m => m.Id == newItem.DirectorId);

                List<Genre> genres = new List<Genre>();
                foreach (var item in newItem.GenreId)
                {
                    genres.Add(dc.Genres.FirstOrDefault(m => m.Id == item));
                }



                fetchedObject.Director = director;
                fetchedObject.Genres = genres;


                dc.SaveChanges();

                return Mapper.Map<MovieFull>(fetchedObject);

            }
        }
    }
}