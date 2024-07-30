using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Interim.Data;
using Interim.Models;

namespace Interim.Controllers
{
    public class SingupController : Controller
    {
        private readonly InterimContext _context;

        public SingupController(InterimContext context)
        {
            _context = context;
        }

        //// GET: Singup
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Singup.ToListAsync());
        //}

        //// GET: Singup/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var singup = await _context.Singup
        //        .FirstOrDefaultAsync(m => m.UserId == id);
        //    if (singup == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(singup);
        //}

        // GET: Singup/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Singup model)
        {

            if (ModelState.IsValid)
            {
                var entry=_context.Singup.Where(e=>e.Email== model.Email).SingleOrDefault();
                if (entry != null)
                {
                    System.Diagnostics.Debug.WriteLine(model.Email.ToString());
                    TempData["UserEmail"] = model.Email;
                    return RedirectToAction("Create", "Login", new { id=model.UserId});

                }



                else
                {
                    
                    var data = new Singup()
                {
                    Email = model.Email,
                    Password = model.Password,
                };

                _context.Singup.Add(data);
                _context.SaveChanges();
                return RedirectToAction("Create", "Biometric", new { id=model.UserId});
                }

            }

            else
            {
                TempData["errorMessage"] = "Empty form!";
                return View(model);
            }
        
        }


        public IActionResult Admin_login(Singup model) {

            //var em = TempData["UserEmail"];
            //var data = _context.Singup.Where(e => e.Email == model.Email).SingleOrDefault();
            if (model.Password == "admin")
            {
                return RedirectToAction("Index", "Biometric");
            }
            else
                return View(model);
            
        }


        


        

        //// POST: Singup/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("UserId,Email,Password")] Singup singup)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(singup);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(singup);
        //}

        //// GET: Singup/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var singup = await _context.Singup.FindAsync(id);
        //    if (singup == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(singup);
        //}

        //// POST: Singup/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("UserId,Email,Password")] Singup singup)
        //{
        //    if (id != singup.UserId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(singup);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SingupExists(singup.UserId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(singup);
        //}

        //// GET: Singup/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var singup = await _context.Singup
        //        .FirstOrDefaultAsync(m => m.UserId == id);
        //    if (singup == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(singup);
        //}

        //// POST: Singup/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var singup = await _context.Singup.FindAsync(id);
        //    if (singup != null)
        //    {
        //        _context.Singup.Remove(singup);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool SingupExists(int id)
        {
            return _context.Singup.Any(e => e.UserId == id);
        }
    }
}
