using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTwo_20151.Models;

namespace TestTwo_20151.ViewModels
{
    public class RepoGenre : RepositoryBase
    {
        /// <summary>
        /// Creates List of GenreForList to be presented in the Genre List View
        /// </summary>
        /// <returns>List of GenreForList</returns>
        public IEnumerable<GenreForList> GetGenresForList()
        {
            var fetchedObjects = dc.Genres.OrderBy(genre => genre.Name);

            return Mapper.Map<IEnumerable<GenreForList>>(fetchedObjects);
        }

        public SelectList getSelectGenresList()
        {
            SelectList genresList = new SelectList(dc.Genres, "Id", "Name");
            return genresList;
        }

        public MultiSelectList getMultiSelectGenresList()
        {
            MultiSelectList genresList = new MultiSelectList(dc.Genres, "Id", "Name");
            return genresList;
        }

        public IEnumerable<GenreFull> AllGenres()
        {
            var fetchedObjects = dc.Genres.Include("Movies").OrderBy(a => a.Name);

            return Mapper.Map<IEnumerable<GenreFull>>(fetchedObjects);
        }


        public GenreFull GetGenreFull(int? id)
        {
            var fetchedObject = dc.Genres.Include("Movies").SingleOrDefault(a => a.Id == id);
            return (fetchedObject == null) ? null : Mapper.Map<GenreFull>(fetchedObject);
        }

        public GenreFull AddGenre(GenreAdd newItem)
        {
            

            List<Movie> movies = new List<Movie>();
            foreach (var item in newItem.MovieId)
            {
                movies.Add(dc.Movies.FirstOrDefault(m => m.Id == item));
            }

            var addedItem = dc.Genres.Add(Mapper.Map<Genre>(newItem));
            addedItem.Movies = movies;


            dc.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<GenreFull>(addedItem);
        }

        public GenreFull EditGenre(GenreEdit newItem)
        {
            var fetchedObject = dc.Genres.Include("Movies").SingleOrDefault(a => a.Id == newItem.Id);

            if (fetchedObject == null)
            {
                return null;
            }
            else
            {

                dc.Entry(fetchedObject).CurrentValues.SetValues(newItem);


                List<Movie> movies = new List<Movie>();
                foreach (var item in newItem.MovieId)
                {
                    movies.Add(dc.Movies.FirstOrDefault(m => m.Id == item));
                }

                fetchedObject.Movies = movies;

                dc.SaveChanges();

                return Mapper.Map<GenreFull>(fetchedObject);
            }
        }

        public bool DeletGenreById(int id)
        {
            var itemToDelete = dc.Genres.Include("Movies").SingleOrDefault(a => a.Id == id); ;
            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                dc.Genres.Remove(itemToDelete);
                dc.SaveChanges();
                return true;
            }
        }
    }
}