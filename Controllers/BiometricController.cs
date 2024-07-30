using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Interim.Data;
using Interim.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Interim.Controllers
{
    public class BiometricController : Controller
    {
        private readonly InterimContext _context;

        public BiometricController(InterimContext context)
        {
            _context = context;
        }

        //// GET: Biometric
        public async Task<IActionResult> Index()
        {
            return View(await _context.Biometric.ToListAsync());
        }

        //// GET: Biometric/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var biometric = await _context.Biometric
        //        .FirstOrDefaultAsync(m => m.RegistrationID == id);
        //    if (biometric == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(biometric);
        //}

        // GET: Biometric/Create
        public IActionResult Create(int id)
        {
            int bmi;
            if (BiometricExists(id))
            {
                var data=_context.Biometric.Where(e=>e.RegistrationID==id).SingleOrDefault();
                
                bmi = BMI(data.Height, data.Weight);
                return RedirectToAction("Details", "Biometric", new { id = id, BMI = bmi });
                
            }
            return View();
        }

        //[HttpPost]
        //public IActionResult Create(Biometric model) { 

        //    if (ModelState.IsValid)
        //    {
        //        var data = new Biometric()
        //        {
        //            Name = model.Name,
        //            Height = model.Height,
        //            Weight = model.Weight,
        //            BloodGroup = model.BloodGroup,
        //            Disease = model.Disease,

        //        };
        //        TempData["Name"] = model.Name;
        //        _context.Add(data);
        //        _context.SaveChanges();
        //        return RedirectToAction("Details","Biometric");

        //    }

        //    return View(model);
        //}


        
        public IActionResult Details(int? id)
        {
            var data = _context.Biometric.Find(id);
            if (data != null)
            {
                TempData["Name"] = data.Name;
                return View(data);
            }
            else
                return NotFound();
        }


        //public IActionResult Edit()
        //{
            
        //    return View();
        //}


        //[HttpPost]
        //public IActionResult Edit(Biometric model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var data = new Biometric()
        //        {
        //            Name = model.Name,
        //            Height = model.Height,
        //            Weight = model.Weight,
        //            BloodGroup = model.BloodGroup,
        //            Disease = model.Disease,

        //        };
        //        _context.Biometric.Update(data);
        //        _context.SaveChanges();
        //        return RedirectToAction("Details", "Biometric");


        //    }
        //    return View(model);
        //}


        //public ActionResult Edit([Bind("RegistrationID,Name,Height,Weight,BloodGroup,Disease")] Biometric model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var data = _context.Biometric.Where(e => e.RegistrationID == model.RegistrationID);
        //        if (data == null)
        //        {
        //            return RedirectToAction("Create", "Biometric");
        //        }
        //        else
        //        {
        //            System.Diagnostics.Debug.WriteLine("Value EXIXTS");
        //            return View(model);
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View(model);
        //}








        //// POST: Biometric/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationID,Name,Height,Weight,BloodGroup,Disease")] Biometric biometric)
        {
            int bmi;
            
            if (ModelState.IsValid)
            {
                bmi = BMI(biometric.Height, biometric.Weight);
                
                _context.Add(biometric);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Biometric", new { id=biometric.RegistrationID,BMI=bmi});
            }
            return View(biometric);
        }

        //// GET: Biometric/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biometric = await _context.Biometric.FindAsync(id);
            if (biometric == null)
            {
                return NotFound();
            }
            return View(biometric);
        }

        //// POST: Biometric/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistrationID,Name,Height,Weight,BloodGroup,Disease")] Biometric biometric)
        {
            int bmi;
            if (id != biometric.RegistrationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bmi = BMI(biometric.Height, biometric.Weight);
                    _context.Update(biometric);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BiometricExists(biometric.RegistrationID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Biometric", new { id=id,BMI=bmi});
            }
            return View(biometric);
        }

        //// GET: Biometric/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biometric = await _context.Biometric
                .FirstOrDefaultAsync(m => m.RegistrationID == id);
            if (biometric == null)
            {
                return NotFound();
            }

            return View(biometric);
        }

        //// POST: Biometric/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var biometric = await _context.Biometric.FindAsync(id);
            var data= await _context.Singup.FindAsync(id);
            if (biometric != null && data!=null)
            {
                _context.Biometric.Remove(biometric);
                _context.Singup.Remove(data);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }





        //ADMIN EDIT

        public async Task<IActionResult> Edit_Admin(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biometric = await _context.Biometric.FindAsync(id);
            if (biometric == null)
            {
                return NotFound();
            }
            return View(biometric);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Admin(int id, [Bind("RegistrationID,Name,Height,Weight,BloodGroup,Disease")] Biometric biometric)
        {
            
            if (id != biometric.RegistrationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    _context.Update(biometric);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BiometricExists(biometric.RegistrationID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Biometric");
            }
            return View(biometric);
        }





        private bool BiometricExists(int id)
        {
            return _context.Biometric.Any(e => e.RegistrationID == id);
        }

        private int BMI(int height, int weight)
        {
            int bmi=(int)(weight/(height*height*0.09));


            return bmi;
        }
    }
}
