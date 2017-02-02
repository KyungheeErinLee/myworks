using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTwo_20151.Models;

namespace TestTwo_20151.ViewModels
{
    public class RepoDirector : RepositoryBase
    {
        /// <summary>
        /// Creates List of DirectorForList to be presented in the Director List View
        /// </summary>
        /// <returns>List of DirectorForList</returns>
        public IEnumerable<DirectorForList> GetDirectorsForList()
        {
            var fetchedObjects = dc.Directors.OrderBy(director => director.Name);

            return Mapper.Map<IEnumerable<DirectorForList>>(fetchedObjects);
        }

        public IEnumerable<DirectorFull> AllDirectors()
        {
            var fetchedObjects = dc.Directors.Include("Movies").OrderBy(a => a.Name);

            return Mapper.Map<IEnumerable<DirectorFull>>(fetchedObjects);
        }

        public SelectList getSelectDirectorsList()
        {
            SelectList directorsList = new SelectList(dc.Directors, "Id", "Name");
            return directorsList;
        }

        public DirectorFull GetDirectorFull(int? id)
        {
            var fetchedObject = dc.Directors.Include("Movies").SingleOrDefault(a => a.Id == id);

            return (fetchedObject == null) ? null : Mapper.Map<DirectorFull>(fetchedObject);
        }

        public DirectorFull AddDirector(DirectorAdd newItem)
        {
            var addedItem = dc.Directors.Add(Mapper.Map<Director>(newItem));
            dc.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<DirectorFull>(addedItem);
        }


        public DirectorFull EditDirector(DirectorEdit newItem)
        {
            var fetchedObject = dc.Directors.Include("Movies").SingleOrDefault(a => a.Id == newItem.Id);

            if (fetchedObject == null)
            {
                return null;
            }
            else
            {

                dc.Entry(fetchedObject).CurrentValues.SetValues(newItem);

                dc.SaveChanges();

                return Mapper.Map<DirectorFull>(fetchedObject);
            }
        }

        public bool DeleteDirectorById(int id)
        {

            var itemToDelete = dc.Directors.Include("Movies").SingleOrDefault(a => a.Id == id); ;
            if (itemToDelete == null)
            {
                return false;
            }
            dc.Movies.RemoveRange(itemToDelete.Movies);

            dc.Directors.Remove(itemToDelete);
            dc.SaveChanges();

            return true;
        }
    }
}