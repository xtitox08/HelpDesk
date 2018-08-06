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

        Incidentes Incidentes = new Incidentes();
        Usuarios Usuarios = new Usuarios();
        Mantenimientos Man = new Mantenimientos();
        String Current_User;
        MySql.Data.MySqlClient.MySqlConnection conn;
        string MyConnectionString = "";




        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            Current_User = "";
            return View();
        }

        public ActionResult ListadoIncidencias()
        {
            ViewBag.Message = "ListadoIncidencias";

            return View();
        }

        public ActionResult RegistroIncidencias()
        {

            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }
           
           Incidencia_Data result = Incidentes.Activos(conn);
            ViewBag.Message = result;//passing instance to view 

            return View();
        }
        int ID_OF_User;
        public ActionResult AgregarUsuario()
        {

            UserData_Send Result = ID_Recover();

            ViewBag.Message = Result;//passing instance to view 

            return View();
        }
        
        public ActionResult Mantenimientos()
        {

            return View();
        }
        public ActionResult Reportes()
        {

            return View();
        }

        //Maintance options
    
        public ActionResult Activo()
        {
            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }
            ViewBag.Message = Man.Id_Activo(conn);

            return View();
        }


        public ActionResult Sede()
        {
            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }
            ViewBag.Message = Man.Id_Sede(conn);

            return View();

        }
        public ActionResult Categoria()
        {

            return View();
        }
        public ActionResult Departamento()
        {

            return View();
        }

        //--------------------Post methods-----------------------
        [HttpPost]
        public ActionResult Login(String UserName, String Password)
        {
            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }
         bool result =   Usuarios.Login_Verification(conn, UserName, Password);//returns wheter there is a match between user and pass or not

            if (result) { Current_User = UserName; return View("Index"); }//returns index

            return View();//returns login page


        }



        [HttpPost]
        public ActionResult AgregarUsuario(String ID, String email, String UserName, String pass, String sede, String area, String department, String estado)
        {
            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }
            Usuarios.User_Data(conn, ID_OF_User, email, UserName, pass, sede, area, department, estado);//passing info to insert 

            UserData_Send Result = ID_Recover();//metodo recupera id usuario nuevo 
            ViewBag.Message = Result;//passing instance to view 

            return View(ID_OF_User);

        }


        [HttpPost]
        public ActionResult Activo(String ID, String Name,String Place,String Quantity)
        {
           
            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }
            Man.Activo_Data(conn,ID,Name,Place,Quantity);
            ViewBag.Message = Man.Id_Activo(conn);
            return View();

        }

        [HttpPost]
        public ActionResult Sede(String ID, String Name, String Place)
        {

            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }
            Man.Sede_Data(conn, ID, Name, Place);
            ViewBag.Message = Man.Id_Sede(conn);
            return View(ID);

        }

        [HttpPost]
        public ActionResult RegistroIncidencias(String ID, String Cedula, String Date, String About, String Service, String Type, String Lista, String Priority)
        {
            return View(ID + Cedula + Date + About + Service + Type + Lista + Priority + Current_User);

            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }
            Incidentes.Insert_Data(conn,ID, Cedula,  Date,  About,  Service,  Type,  Lista,  Priority, Current_User);
            ViewBag.Message = Man.Id_Sede(conn);
            ;

        }
        //-----------------End of post methods--------------




        //AWS connectiMySql.Data.MySqlClient.MySqlConnection connon
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

       
        private UserData_Send ID_Recover() {
            //if (MyConnectionString.Equals(String.Empty))
            //{
            //    Connection();
            //}
             if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }

            ID_OF_User = Convert.ToInt32(Usuarios.User_Data(conn));//Result of obtaining ID data

            UserData_Send uds = new UserData_Send
            {//setting parameters of class

                User_Id = ID_OF_User
            };
           
            return uds;

        }

    }
}