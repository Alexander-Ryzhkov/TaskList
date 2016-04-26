using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using TaskList.Models;

namespace TaskList.Controllers
{
    public class ToDoesController : Controller
    {
        private ApplicationDbContext _context;

        public ToDoesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ToDoes
        public IActionResult Index()
        {
            return View(_context.ToDo.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ToDo toDo)
        {

            if (ModelState.IsValid)
            {
                _context.ToDo.Add(toDo);
                _context.SaveChanges();
                return View("Index", _context.ToDo.ToList());
            }
            return View(_context.ToDo.ToList());

        }

        // Post: ToDoes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string id)
        {
            int curID = System.Int32.Parse(id);
            ToDo toDo = _context.ToDo.Single(m => m.ID == curID);
            toDo.IsCompleted = !toDo.IsCompleted;
            _context.Update(toDo);
            _context.SaveChanges();
            return View("Index", _context.ToDo.ToList());
        }

        // GET: ToDoes/Sort
        public IActionResult Sort()
        {
            var result = _context.ToDo.OrderBy(x => x.Priority).OrderBy(x => x.Deadline).OrderBy(x => x.IsCompleted).ToList();
            return View("Index", result);
        }

        // GET: ToDoes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ToDo toDo = _context.ToDo.Single(m => m.ID == id);
            if (toDo == null)
            {
                return HttpNotFound();
            }

            return View(toDo);
        }

        // GET: ToDoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                _context.ToDo.Add(toDo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDo);
        }

        // GET: ToDoes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ToDo toDo = _context.ToDo.Single(m => m.ID == id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            return View(toDo);
        }

        // POST: ToDoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                _context.Update(toDo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDo);
        }

        // GET: ToDoes/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ToDo toDo = _context.ToDo.Single(m => m.ID == id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            _context.ToDo.Remove(toDo);
            _context.SaveChanges();
            return View("Index", _context.ToDo.ToList());
        }

        // POST: ToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            ToDo toDo = _context.ToDo.Single(m => m.ID == id);
            _context.ToDo.Remove(toDo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
