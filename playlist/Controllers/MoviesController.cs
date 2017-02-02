using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestTwo_20151.Models;
using TestTwo_20151.ViewModels;

namespace TestTwo_20151.Controllers
{
    public class MoviesController : Controller
    {
        private RepoMovie man = new RepoMovie();
        private RepoDirector dir = new RepoDirector();
        private RepoGenre gen = new RepoGenre();

        public ActionResult Index()
        {
            // GET: Movies
            return View(man.AllMovies());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieFull movie = man.GetMovieFull(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        public ActionResult Create()
        {
            var addForm = new MovieAddForm();
            addForm.Genres = gen.getMultiSelectGenresList();
            addForm.Director = dir.getSelectDirectorsList();
            return View(addForm);
        }

        // POST: Genres/Create
        [HttpPost]
        public ActionResult Create(MovieAdd newItem)
        {
            if (ModelState.IsValid)
            {
                var addedItem = man.AddMovie(newItem);

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

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue) { return HttpNotFound(); }
            var fetchedObject = man.GetMovieFull(id.Value);

            if (fetchedObject == null)
            {
                return HttpNotFound();
            }
            else
            {
                var editForm = AutoMapper.Mapper.Map<MovieEditForm>(fetchedObject);
                editForm.GenreList = new MultiSelectList(gen.GetGenresForList(), "Id", "Name", fetchedObject.Genres.Select(m => m.Id));
                editForm.DirectorList = new SelectList(dir.GetDirectorsForList(), "Id", "Name", fetchedObject.Director.Id);
                return View(editForm);
            }

        }

        [HttpPost]
        public ActionResult Edit(int? id, MovieEdit newItem)
        {
            if (ModelState.IsValid & id == newItem.Id)
            {
                var editedItem = man.EditMovie(newItem);

                if (editedItem == null)
                {
                    var editForm = AutoMapper.Mapper.Map<MovieEditForm>(newItem);
                    editForm.GenreList = new MultiSelectList(gen.GetGenresForList(), "Id", "Name", newItem.GenreId);
                    editForm.DirectorList = new SelectList(dir.GetDirectorsForList(), "Id", "Name", newItem.DirectorId);

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
                var editForm = AutoMapper.Mapper.Map<MovieEditForm>(newItem);
                editForm.GenreList = new MultiSelectList(gen.GetGenresForList(), "Id", "Name", newItem.GenreId);
                editForm.DirectorList = new SelectList(dir.GetDirectorsForList(), "Id", "Name", newItem.DirectorId);

                ModelState.AddModelError("modelState", "There was an error. (The incoming data is invalid.)");
                return View(editForm);
            }
        }


        public ActionResult Delete(int? id)
        {
            if (!id.HasValue) { return HttpNotFound(); }
            var itemToDelete = man.GetMovieFull(id.Value);

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
            man.DeletMovieById(id.Value);
            return RedirectToAction("index");
        }
    }
}
