using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestTwo_20151.ViewModels
{
    public class ImageController : Controller
    {

        private RepoImage man = new RepoImage();

        [Route("image/{id}")]
        public ActionResult GetImageById(int? id)
        {
            // Notice that we changed the parameter type to nullable

            // Determine whether we can continue
            if (!id.HasValue) { return HttpNotFound(); }

            // Fetch the object, so that we can inspect its value
            var fetchedObject = man.GetImageFileById(id.Value);

            if (fetchedObject == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Return a file content result
                // Set the Content-Type header, and return the photo bytes
                return File(fetchedObject.ImageFile, fetchedObject.ContentType);
            }
        }

        // Content type to extension is NOT built into the .NET Framework (it appears)
        // Source or inspiration was here...
        // http://stackoverflow.com/questions/23087808/c-sharp-get-file-extension-by-content-type
        private string GetDefaultFileExtension(string contentType)
        {
            string result;
            Microsoft.Win32.RegistryKey key;
            object value;

            key = Microsoft.Win32.Registry.ClassesRoot
                .OpenSubKey(@"MIME\Database\Content Type\" + contentType, false);
            value = key != null ? key.GetValue("Extension", null) : null;
            result = value != null ? value.ToString() : string.Empty;

            return result;
        }

        public ActionResult Index()
        {
            man.LoadImage();
            return View();
        }
    }
}