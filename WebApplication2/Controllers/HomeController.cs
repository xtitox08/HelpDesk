﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data;
using WebApplication2.Models;
using System.Collections;
using System.Net;
using System.Net.Mail;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {

        Incidentes Incidentes = new Incidentes();//instances needed 
        Usuarios Usuarios = new Usuarios();
        Mantenimientos Man = new Mantenimientos();
        Reportes Report = new Reportes();
        
        MySql.Data.MySqlClient.MySqlConnection conn;
        string MyConnectionString = "";




        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
        
            return View();
        }

        public ActionResult ListadoIncidencias()//returns the result of calling the view of Incidents list
        {

            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }
            if (Convert.ToInt32(Incidentes.Id_OfCurrentUser(conn)) == 15)//checks if it is a admin user
            {
                Incidencia_Data Result = Incidentes.ListarIncidencias(conn);
              ArrayList Personnel =  Incidentes.ListOfPersonnel(conn);
                ViewBag.Personnel = Personnel;//returns list of personnel
                ViewBag.Message = Result;//passing instance to view 
                int Quantity = Convert.ToInt32(Incidentes.Quantity(conn)  );//gets quantity of Incidents
                ViewBag.Quantity = Quantity;
                return View("Admin_Assignments");//returns admin view
            }
            else {
                Incidencia_Data Result = Incidentes.ListarIncidencias(conn);//executed when normal user trie to access the incident listing
                ViewBag.Message = Result;//passing instance to view 
                int Quantity = Convert.ToInt32(Incidentes.Quantity(conn));
                ViewBag.Quantity = Quantity;
                return View(); }//returns normal user view
          
        }
  

        public ActionResult RegistroIncidencias()//opens register of incidents
        {

            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }
           
           Incidencia_Data Result = Incidentes.Activos(conn);
            ViewBag.Message = Result;//passing instance to view 

            return View();
        }
        int ID_OF_User;
        public ActionResult AgregarUsuario()//add user view
        {
            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }
            if (Convert.ToInt32(Incidentes.Id_OfCurrentUser(conn)) == 15)
            {
                UserData_Send Result = ID_Recover();

                ViewBag.Message = Result;//passing instance to view 

                return View();

            }
            else {
                return View("Error");

            }
           
        }
        
        public ActionResult Mantenimientos()//Opens maintenance view 
        {
            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }
            if (Convert.ToInt32(Incidentes.Id_OfCurrentUser(conn)) == 15)
            {
                return View();
            }
            else {
                return View("Error");
            }
        }
        public ActionResult Reportes()//access reports view 
        {
            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }
            if (Convert.ToInt32(Incidentes.Id_OfCurrentUser(conn)) == 15)
            {
                Incidencia_Data Result = Report.ListarReportes(conn);
                ViewBag.Message = Result;//passing instance to view 

                return View();
            }
            else
            {
                return View("Error");
            }



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
        public ActionResult Categoria()//opens category view
        {

            return View();
        }
        public ActionResult Departamento()//opens department view
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

            if (result) {
                Incidentes.Insert_Temp_User(conn,UserName);
                return View("Index"); }//returns index

            ViewBag.AlertMessage = "Error, Usuario y/o Contraseña no valida;";

            return View();//returns login page


        }

        interface IHeaderInfo
        {
            string PageUser { get; set; }
        }

        [HttpPost]
        public ActionResult AgregarUsuario(String ID, String email, String UserName, String pass, String sede, String area, String department, String estado)
        {
            ViewBag.AlertMessage = "Usuario agregado con exito!";
            UserData_Send Result = ID_Recover();//metodo recupera id usuario nuevo 
            try
            {
                if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
                {
                    Connection();//Open connection to aws method
                }
                Usuarios.User_Data(conn, ID_OF_User, email, UserName, pass, sede, area, department, estado);//passing info to insert 
           
                ViewBag.Message = Result;//passing instance to view 
        
                return View(ID_OF_User);


            }
            catch (Exception) { ViewBag.AlertMessage = "Error en los espacios de rellenado, intentelo denuevo y verifique su informacion"; return View(ID_OF_User); }
        }


        [HttpPost]
        public ActionResult Activo(String ID, String Name,String Place,String Quantity)//Post action of activos
        {
            try
            {
                if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
                {
                    Connection();//Open connection to aws method
                }
                Man.Activo_Data(conn, ID, Name, Place, Quantity);
                ViewBag.AlertMessage = "Activo agregado con exito";
                ViewBag.Message = Man.Id_Activo(conn);
                return View();
            }
            catch (Exception) {
                ViewBag.AlertMessage = "Error, verifique sus espacios de entrada";
                return View(); }

        

        }

        [HttpPost]
        public ActionResult Sede(String ID, String Name, String Place)//post method of HQ 
        {
            try { 
            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }
            Man.Sede_Data(conn, ID, Name, Place);
                ViewBag.AlertMessage = "Activo agregado con exito";
                ViewBag.Message = Man.Id_Sede(conn);
            return View(ID);
        }
            catch (Exception) {
                ViewBag.AlertMessage = "Error, verifique sus espacios de entrada";
                return View(ID);
    }
}

        [HttpPost]
        public ActionResult RegistroIncidencias(String ID, String Cedula, String Date, String About, String Service, String Type, String Lista, String Priority)
        {//registry of incidents

            try
            {
                if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
                {
                    Connection();//Open connection to aws method
                }
                Incidencia_Data result = Incidentes.Activos(conn);
                ViewBag.Message = result;//passing instance to view 

                if (!Date.Substring(0, 4).Contains("/") && Date.Substring(5, 5).Length == 5)//checks if the date format is correct
                {


                    String Current_User = Incidentes.Current_User(conn);



                    Incidentes.Insert_Data(conn, ID, Cedula, Date, About, Service, Type, Lista, Priority, Current_User);

                    ViewBag.AlertMessage = "Incidencia Registrada";


                    return View();
                }
                else { ViewBag.AlertMessage = "Error, revisa correctamente los espacios rellenados y recuerda que la fecha es formato yyyy/mm/dd"; return View(); }//catchs error exception

            }
            catch (Exception) {
                ViewBag.AlertMessage = "Se produjo un error, revisa correctamente los espacios rellenados y recuerda que la fecha es formato yyyy/mm/dd"; return View();
                //return view on exception 
            }


            }
        [HttpPost]
        public ActionResult ListadoIncidencias(String Code, String Personnel)//post method of incident listing
        {

            if (MyConnectionString.Equals(String.Empty))//Open connection if not exist 
            {
                Connection();//Open connection to aws method
            }
            try
            {
                Incidencia_Data Result = Incidentes.ListarIncidencias(conn);
                
                ViewBag.Personnel = Incidentes.ListOfPersonnel(conn);//passing the personnel list to voew 
                ViewBag.Message = Result;//passing instance to view 

                Incidentes.UpdateAssignment(conn, Code, Personnel);
                ViewBag.AlertMessage = "Tecnico asignado! Presione Listado de Incidencia si requiere ver la tabla de incidencias ";
                return View("Admin_Assignments");
            }
            catch (Exception) { ViewBag.AlertMessage = "Se ha producido un error, verifiquelos campos de entrada e intentelo denuevo"; return View("Admin_Assignments"); }
            //returns corresponding view on error appearance 
         

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