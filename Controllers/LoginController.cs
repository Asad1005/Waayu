using Interim.Data;
using Interim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Interim.Controllers
{
    public class LoginController : Controller
    {
        private readonly InterimContext _context;

        public LoginController(InterimContext context)
        {
            _context = context;
        }

        // GET: Login
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Login.ToListAsync());
        //}

        // GET: Login/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var login = await _context.Login
        //        .FirstOrDefaultAsync(m => m.UserId == id);
        //    if (login == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(login);
        //}

        // GET: Login/Create
        public IActionResult Create(int id)
        {
            var data = _context.Singup.Find(id);
            if (data != null)
            {
                return View(data);
            }

            return View();
        }



        [HttpPost]
        public IActionResult Create(int id,Singup model)
        {

            System.Diagnostics.Debug.WriteLine(model.Email.ToString());
            //System.Diagnostics.Debug.WriteLine(model.Password.ToString());
            
            if (ModelState.IsValid)
            {


                var data = _context.Singup.Where(e => e.Email == model.Email).SingleOrDefault();

                
                if (data != null)
                {
                    if (data.Password == model.Password)

                    {


                        return RedirectToAction("Create", "Biometric", new { id = data.UserId });


                    }
                    else
                    {
                        TempData["wrongPassword"] = "Wrong Password";
                        return View(model);

                    }
                }
                else
                {
                    TempData["errorEmail"] = "Email not found";
                    return RedirectToAction("Create","Singup");

                }
                
            }
            return View(model);
        }





        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("UserId,Email,Password")] Login login)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(login);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(login);
        //}

        //// GET: Login/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var login = await _context.Login.FindAsync(id);
        //    if (login == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(login);
        //}

        //// POST: Login/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("UserId,Email,Password")] Login login)
        //{
        //    if (id != login.UserId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(login);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!LoginExists(login.UserId))
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
        //    return View(login);
        //}

        //// GET: Login/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var login = await _context.Login
        //        .FirstOrDefaultAsync(m => m.UserId == id);
        //    if (login == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(login);
        //}

        //// POST: Login/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var login = await _context.Login.FindAsync(id);
        //    if (login != null)
        //    {
        //        _context.Login.Remove(login);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool LoginExists(int id)
        //{
        //    return _context.Login.Any(e => e.UserId == id);
        //}

        //private int BMI(int height, int weight)
        //{
        //    int bmi = (int)(weight / (height * height * 0.09));


        //    return bmi;
        //}
    }
}
