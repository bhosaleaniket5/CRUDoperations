using CRUDoperations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDoperations.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmployees()
        {
            using (MyDataEntities dc = new MyDataEntities())
            {
                var employees = dc.Employees.OrderBy(a => a.FirstName).ToList();
                return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpGet]
        public ActionResult Save(int id)
        {
            using (MyDataEntities dc = new MyDataEntities())
            {
                var v = dc.Employees.Where(a => a.EmployeeID == id).FirstOrDefault();
                return view(v);
            }

        }
        [HttpPost]
        public ActionResult Save(Employee emp)
        {
            bool status = false;
            if(ModelState.IsValid)
            {
                using (MyDataEntities dc = new MyDataEntities())
                {
                    if(emp.EmployeeID>0)
                    {
                        //Edit the data
                        var v = dc.Employees.Where(a => a.EmployeeID == emp.EmployeeID).FirstOrDefault();
                        if(v!=null)
                        {
                            v.FirstName = emp.FirstName;
                            v.LastName = emp.LastName;
                            v.EmailID = emp.EmailID;
                            v.City = emp.City;
                            v.Country = emp.Country;


                        }
                    }
                    else
                    {
                        //save
                        dc.Employees.Add(emp);
                    }
                    dc.SaveChanges();
                    status = true;

                }
            }
            return new JsonResult { Data = new { status = status } };

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (MyDataEntities dc = new MyDataEntities())
            {
                var v = dc.Employees.Where(a => a.EmployeeID == id).FirstOrDefault();
                if (v!= null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            bool status = false;
            using (MyDataEntities dc = new MyDataEntities())
            {
                var v = dc.Employees.Where(a => a.EmployeeID == id).FirstOrDefault();
                if (v != null)
                {
                    dc.Employees.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }

}