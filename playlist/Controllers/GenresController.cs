using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestTwo_20151.ViewModels;

namespace TestTwo_20151.Controllers
{
    public class GenresController : Controller
    {

        private RepoGenre gen = new RepoGenre();
        private RepoMovie mov = new RepoMovie();

        // GET: Genres
        public ActionResult Index()
        {
            return View(gen.AllGenres());
           
        }

        // GET: Genres/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue) { return HttpNotFound(); }
            var fetchedObject = gen.GetGenreFull(id.Value);

            if (fetchedObject == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(fetchedObject);
            }
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            var addForm = new GenreAddFrom();
            addForm.Movies = mov.getSelectMovieList();
            return View(addForm);
        }

        // POST: Genres/Create
        [HttpPost]
        public ActionResult Create(GenreAdd newItem)
        {
            if (ModelState.IsValid)
            {
                var addedItem = gen.AddGenre(newItem);

                if (addedItem == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View();
            }
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue) { return HttpNotFound(); }
            var fetchedObject = gen.GetGenreFull(id.Value);

            if (fetchedObject == null)
            {
                return HttpNotFound();
            }
            else
            {
                var editForm = AutoMapper.Mapper.Map<GenreEditForm>(fetchedObject);
                editForm.MovieList = new MultiSelectList(mov.getSelectMovieList(), "Id", "MovieTitle", fetchedObject.Movies.Select(m => m.Id));
                return View(editForm);
            }
        }

        // POST: Genres/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, GenreEdit newItem)
        {
            if (ModelState.IsValid & id == newItem.Id)
            {               
                var editedItem = gen.EditGenre(newItem);

                if (editedItem == null)
                {
                    var editForm = AutoMapper.Mapper.Map<GenreEditForm>(newItem);
                    editForm.MovieList = new MultiSelectList(mov.GetMoviesForList(), "Id", "MovieTitle", newItem.MovieId);


                    ModelState.AddModelError("modelState", "There was an error. (The edited data could not be saved.)");
                    return View(editForm);
                }
                else
                {
                    return RedirectToAction("details", new { id = newItem.Id });
                }
            }
            else
            {
                var editForm = AutoMapper.Mapper.Map<GenreEditForm>(newItem);
                editForm.MovieList = new MultiSelectList(mov.GetMoviesForList(), "Id", "MovieTitle", newItem.MovieId);

                ModelState.AddModelError("modelState", "There was an error. (The incoming data is invalid.)");
                return View(editForm);
            }
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue) { return HttpNotFound(); }
            var itemToDelete = gen.GetGenreFull(id.Value);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(itemToDelete);
            }
        }

        // POST: Genres/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            if (!id.HasValue) { return HttpNotFound(); }
            gen.DeletGenreById(id.Value);
            return RedirectToAction("index");
        }
    }
}
