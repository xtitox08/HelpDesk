using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {


        Usuarios Usuarios = new Usuarios();
        MySql.Data.MySqlClient.MySqlConnection conn;
        string MyConnectionString = "";
    

    

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
        int ID_OF_User;
        public ActionResult AgregarUsuario()
        {
           
            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }

            ID_OF_User = Convert.ToInt32(Usuarios.User_Data(conn));//Result of obtaining ID data

            UserData_Send uds = new UserData_Send {//setting parameters of class

                User_Id = ID_OF_User
            };
            ViewBag.Message = uds;//passing instance to view 

            return View();
        }
        
        
        
        [HttpPost]
        public ActionResult AgregarUsuario(String ID, String email, String UserName, String pass, String sede, String area, String department,String estado)
        {
            if (MyConnectionString.Equals(String.Empty))
            {
                Connection();
            }
            Usuarios.User_Data(conn, ID_OF_User, email,UserName,pass,sede,area,department,estado);//passing info to insert 

         
            return Content(ID + email + UserName + pass + sede + area);

        }



        //AWS connection
        public void Connection()
        {

            //Connection String to connect aws rds DB instance
            MyConnectionString = "Server=helpdesk.cyeip6jtr6ck.us-east-1.rds.amazonaws.com;database=mydb;uid=HD_Master;" +
                    "pwd=wotilark123;port=3306;";

            // Open connection
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = MyConnectionString;
            conn.Open();
            

        }

    }
}