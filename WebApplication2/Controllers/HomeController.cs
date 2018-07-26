using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ListadoIncidencias()
        {
            ViewBag.Message = "ListadoIncidencias";

            return View();
        }

        public ActionResult RegistroIncidencias()
        {
            ViewBag.Message = "RegistroIncidencias";

            return View();
        }
        public ActionResult AgregarUsuario()
        {
            ViewBag.Message = "formulario";

            return View();
        }



        [HttpPost]
        public ActionResult AgregarUsuario(String ID, String email, String UserName, String pass, String sede, String area, String department)
        {
            String someValue;
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "Server=helpdesk.cyeip6jtr6ck.us-east-1.rds.amazonaws.com;database=mydb;uid=HD_Master;" +
                "pwd=wotilark123;port=3306;";

         
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

      
            string query = "SELECT * FROM Rol_Usuario";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            var reader = cmd.ExecuteReader();

            reader.Read();
            
                someValue = reader["Id_Rol"].ToString();

                // obtiene primer valor
            
            return Content(ID + email + UserName + pass + sede + area+someValue);

        }


    }
}