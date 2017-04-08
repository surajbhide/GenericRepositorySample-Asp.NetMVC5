using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF.Core.Data;
using EF.Data;

namespace EF.Web.Controllers
{
    public class BookController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        private Repository<Book> bookRepository;

        public BookController()
        {
            bookRepository = _unitOfWork.Repository<Book>();
        }

        public ActionResult Index() => View(bookRepository.Table.ToList());

        public ActionResult CreateEditBook(int? id)
        {
            Book model = new Book();
            if (id.HasValue)
                model = bookRepository.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateEditBook(Book model)
        {
            if (model.ID == 0)
            {
                model.DateModified = DateTime.Now;
                model.DateAdded = DateTime.Now;
                model.IP = Request.UserHostAddress;
                bookRepository.Insert(model);
            }
            else
            {
                var editModel = bookRepository.GetById(model.ID);
                editModel.Title = model.Title;
                editModel.Author = model.Author;
                editModel.ISBN = model.ISBN;
                editModel.DatePublished = model.DatePublished;
                editModel.DateModified = DateTime.Now;
                editModel.IP = Request.UserHostAddress;
                bookRepository.Update(editModel);
            }
            _unitOfWork.Complete();

            if (model.ID > 0)
                return RedirectToAction("Index");

            return View(model);
        }

        public ActionResult DeleteBook(int id)
        {
            Book model = bookRepository.GetById(id);
            return View(model);
        }
    }
}