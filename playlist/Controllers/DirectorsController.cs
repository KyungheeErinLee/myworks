using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTwo_20151.ViewModels;

namespace TestTwo_20151.Controllers
{
    public class DirectorsController : Controller
    {

        private RepoDirector dir = new RepoDirector();
        private RepoMovie mov = new RepoMovie();

        // GET: Directors
        public ActionResult Index()
        {
            return View(dir.AllDirectors());
            
        }

        // GET: Directors/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue) { return HttpNotFound(); }

            // Fetch the object, so that we can inspect its value
            var fetchedObject = dir.GetDirectorFull(id.Value);

            if (fetchedObject == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(fetchedObject);
            }
        }

        // GET: Directors/Create
        public ActionResult Create()
        {
            var addForm = new DirectorAddForm();           
            return View(addForm);
        }

        // POST: Directors/Create
        [HttpPost]
        public ActionResult Create(DirectorAdd newItem)
        {
            if (ModelState.IsValid)
            {               
                var addedItem = dir.AddDirector(newItem);

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

        // GET: Directors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue) { return HttpNotFound(); }
            var fetchedObject = dir.GetDirectorFull(id.Value);

            if (fetchedObject == null)
            {
                return HttpNotFound();
            }
            else
            {
                var editForm = AutoMapper.Mapper.Map<DirectorEditForm>(fetchedObject);
                return View(editForm);
            }
        }

        // POST: Directors/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, DirectorEdit newItem)
        {
            if (ModelState.IsValid & id == newItem.Id)
            {
                var editedItem = dir.EditDirector(newItem);

                if (editedItem == null)
                {                   
                    var editForm = AutoMapper.Mapper.Map<GenreEditForm>(newItem);                  

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

                ModelState.AddModelError("modelState", "There was an error. (The incoming data is invalid.)");
                return View(editForm);
            }
        }

        // GET: Directors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue) { return HttpNotFound(); }
            var itemToDelete = dir.GetDirectorFull(id.Value);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(itemToDelete);
            }
        }

        // POST: Directors/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            if (!id.HasValue) { return HttpNotFound(); }
            dir.DeleteDirectorById(id.Value);
            return RedirectToAction("index");
        }
    }
}
