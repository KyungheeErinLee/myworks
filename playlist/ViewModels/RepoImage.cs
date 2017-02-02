using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTwo_20151.Models;

namespace TestTwo_20151.ViewModels
{

    public class RepoImage : RepositoryBase
    {

        /// <summary>
        /// Creates List of GenreForList to be presented in the Genre List View
        /// </summary>
        /// <returns>List of GenreForList</returns>
        public IEnumerable<ImageFull> GetImagesForList()
        {
            var fetchedObjects = dc.Images.OrderBy(i => i.Name);

            return Mapper.Map<IEnumerable<ImageFull>>(fetchedObjects);
        }

        public SelectList getSelectImagesList()
        {
            SelectList ImagesList = new SelectList(dc.Images, "Id", "Name");
            return ImagesList;
        }


        public ImageFull GetImageFileById(int id)
        {
            var fetchedObject = dc.Images.SingleOrDefault(i => i.Id == id);
            return (fetchedObject == null) ? null : Mapper.Map<ImageFull>(fetchedObject);
        }

        public void LoadImage()
        {
            if (dc.Images.Count() == 0)
            {               
                string targetDirectory = HttpContext.Current.Server.MapPath("~/App_Data/images");

                if (Directory.Exists(targetDirectory))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(targetDirectory);
                    FileInfo[] fileInfo = directoryInfo.GetFiles();

                    foreach (FileInfo fi in fileInfo)
                    {

                        string contentType = MimeMapping.GetMimeMapping(fi.FullName);

                        System.Drawing.Image loadedImage = System.Drawing.Image.FromFile(fi.FullName);
                        System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();

                        Image im = new Image();

                        im.ImageFile = (byte[])converter.ConvertTo(loadedImage, typeof(byte[]));
                        im.Name = Path.GetFileNameWithoutExtension(fi.FullName);
                        im.ContentType = contentType;
                        dc.Images.Add(im);
                    }

                    dc.SaveChanges();
                }
            }
        }
    }
}