﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class Incidentes
    {
     
        //return table activos items
        public Incidencia_Data Activos(MySql.Data.MySqlClient.MySqlConnection conn)
        {
            ArrayList Lista_Activos = new ArrayList();
            string Query = "SELECT `Activo`.`Nombre` FROM `mydb`.`Activo` ; ";
            string SecondQuery = "Select COUNT(*) FROM mydb.Activo;";
            var Cmd = new MySql.Data.MySqlClient.MySqlCommand(SecondQuery, conn);
            var Reader = Cmd.ExecuteReader();
            Reader.Read();
            //get quantity of items
            int Activos_Quantity = Convert.ToInt32(Reader["COUNT(*)"].ToString());

            Reader.Close();


            Cmd = new MySql.Data.MySqlClient.MySqlCommand(Query, conn);
            Reader = Cmd.ExecuteReader();


            for (int i = 0; i < Activos_Quantity; i++) {
                Reader.Read();
                Lista_Activos.Add(Reader["Nombre"].ToString());
            }
            Incidencia_Data Lista = new Incidencia_Data {
                Activos = Lista_Activos


            };

            Reader.Close();
            return Lista;
        }

        public int Id_Incidencia(MySql.Data.MySqlClient.MySqlConnection conn) {
            string Query = "SELECT `Incidencia`.`Id_Incidencia` FROM `mydb`.`Incidencia` order by Id_Incidencia DESC LIMIT 1; ";

            var Cmd = new MySql.Data.MySqlClient.MySqlCommand(Query, conn);
            var Reader = Cmd.ExecuteReader();
            Reader.Read();
            //get quantity of items
            int Id_Incidencia = Convert.ToInt32(Reader["Id_Incidencia"].ToString())+1;

            Reader.Close();
            return Id_Incidencia ;

        }

        public int Id_Usuario(MySql.Data.MySqlClient.MySqlConnection conn,String Current_User)
        {
            string Query = "SELECT `Usuario`.`Id_Usuario` FROM `mydb`.`Usuario` WHERE Correo_Electronico = '"+Current_User+"'; ";

            var Cmd = new MySql.Data.MySqlClient.MySqlCommand(Query, conn);
            var Reader = Cmd.ExecuteReader();
            Reader.Read();
            //get quantity of items
            int Id_Incidencia = Convert.ToInt32(Reader["Id_Usuario"].ToString());

            Reader.Close();
            return Id_Incidencia ;

        }

        public int Id_Activo(MySql.Data.MySqlClient.MySqlConnection conn,String Item_Name) {

            string Query = "SELECT `Activo`.`Id_Activo` FROM `mydb`.`Activo` WHERE Nombre = '" + Item_Name + "'; ";

            var Cmd = new MySql.Data.MySqlClient.MySqlCommand(Query, conn);
            var Reader = Cmd.ExecuteReader();
            Reader.Read();
            //get quantity of items
            int Id_Activo = Convert.ToInt32(Reader["Id_Activo"].ToString());

            Reader.Close();
            return Id_Activo;


        }


        public void Insert_Data(MySql.Data.MySqlClient.MySqlConnection conn,String ID,String Cedula, String Date, String About, String Service, String Type, String Lista,String Priority,String Current_User) {


            String DB_Insert;
            Reportes Report = new Reportes();
            if (Date.Contains("/"))
            {
                Date = Date.Replace('/', '-');
            }
            Report.Insert_Report(conn,Cedula,Date,About);
           
            //Insert in db 
            DB_Insert = " INSERT INTO `mydb`.`Incidencia` (`Id_Incidencia`, `Id_Usuario`, `Id_Activo`, `Cedula_Solicitante`,`Tipo_Solicitud`, `Modo_Incidente`, `Prioridad`, `Asunto`, `Detalle`, `Categoria_Incidente`, `Fecha_Registro`, `Fecha_Atencion`, `Estado`, `Fecha_Finalizacion`) "+
                                                  "VALUES ("+Id_Incidencia(conn)+","+ Id_Usuario(conn,Current_User) +", "+Id_Activo(conn,Lista)+", "+Cedula+" , '"+Service+"', '"+"-"+"' , '"+Priority+"', '"+About+"', '"+"-"+"', '"+Type+"', '"+Date+"' , '"+ "-" + " ', '"+ "Activo"+ "' , '" + "-" + " ' ); ";
            //error on query key constraint cedula solicitante fails on reference to id_reporte on table reportes (insert first in reportes or something with same cedula and id)
            var Insert = new MySql.Data.MySqlClient.MySqlCommand(DB_Insert, conn);//Insert comm
            var executer = Insert.ExecuteNonQuery();//execute non query 
            


        }

        public String Current_User(MySql.Data.MySqlClient.MySqlConnection conn)
        {

            string Query = "SELECT `Current_User_Temp`.`Current_User` FROM `mydb`.`Current_User_Temp`; ";

            var Cmd = new MySql.Data.MySqlClient.MySqlCommand(Query, conn);
            var Reader = Cmd.ExecuteReader();
            Reader.Read();
            //get quantity of items
            String User = Reader["Current_User"].ToString();

            Reader.Close();
            return User;


        }

          public String Id_OfCurrentUser(MySql.Data.MySqlClient.MySqlConnection conn)
        {
            String Email = Current_User(conn);
            string Query = "SELECT `Usuario`.`Id_Rol` FROM `mydb`.`Usuario` WHERE Correo_Electronico = '"+Email+"'; ";

            var Cmd = new MySql.Data.MySqlClient.MySqlCommand(Query, conn);
            var Reader = Cmd.ExecuteReader();
            Reader.Read();
            //get quantity of items
            String User = Reader["Id_Rol"].ToString();

            Reader.Close();
            return User;


        }

        public void Insert_Temp_User(MySql.Data.MySqlClient.MySqlConnection conn,String Current_User)
        {
            String DB_Insert;
            String DB_Truncate = "TRUNCATE Current_User_Temp;";

            var Insert = new MySql.Data.MySqlClient.MySqlCommand(DB_Truncate, conn);//Insert comm
            var executer = Insert.ExecuteNonQuery();//execute non query 

          
            //Insert in db 
            DB_Insert = " INSERT INTO `mydb`.`Current_User_Temp` (`Current_User`) VALUES ('" + Current_User + "');";
         
             Insert = new MySql.Data.MySqlClient.MySqlCommand(DB_Insert, conn);//Insert comm
             executer = Insert.ExecuteNonQuery();//execute non query 



        }
        public String GetUser_byID( String ID)
        {
            MySql.Data.MySqlClient.MySqlConnection conn = Connection();
            conn.Open();

            string Query = "SELECT `Usuario`.`Nombre_Usuario` FROM `mydb`.`Usuario` WHERE Id_Usuario = " + ID + "; ";

            var Cmd = new MySql.Data.MySqlClient.MySqlCommand(Query, conn);
            var Reader = Cmd.ExecuteReader();
            Reader.Read();
            //get quantity of items
           String Id_Incidencia = Reader["Nombre_Usuario"].ToString();

            Reader.Close();
            return Id_Incidencia;

        }
        public String GetUserID_ByName(MySql.Data.MySqlClient.MySqlConnection conn,String ID)
        {
  

            string Query = "SELECT `Usuario`.`Id_Usuario` FROM `mydb`.`Usuario` WHERE Nombre_Usuario = '" + ID+ "'; ";

            var Cmd = new MySql.Data.MySqlClient.MySqlCommand(Query, conn);
            var Reader = Cmd.ExecuteReader();
            Reader.Read();
            //get quantity of items
            String Id_Usuario = Reader["Id_Usuario"].ToString();

            Reader.Close();
            return Id_Usuario;

        }



        public Incidencia_Data ListarIncidencias(MySql.Data.MySqlClient.MySqlConnection conn) {

            ArrayList Lista_Incidente = new ArrayList();
            string Query = "SELECT * FROM Report;";
            string SecondQuery = "Select COUNT(*) FROM mydb.Incidencia;";
            var Cmd = new MySql.Data.MySqlClient.MySqlCommand(SecondQuery, conn);
            var Reader = Cmd.ExecuteReader();
            Reader.Read();
            //get quantity of items
            int Incidencia_Quantity = Convert.ToInt32(Reader["COUNT(*)"].ToString());

            Reader.Close();


            Cmd = new MySql.Data.MySqlClient.MySqlCommand(Query, conn);
            Reader = Cmd.ExecuteReader();


            for (int i = 0; i < Incidencia_Quantity; i++)
            {
                Reader.Read();
                Lista_Incidente.Add(Reader["Id_Incidencia"].ToString());
                Lista_Incidente.Add(Reader["Asunto"].ToString());
                Lista_Incidente.Add(Reader["Fecha_Registro"].ToString());
                Lista_Incidente.Add(GetUser_byID(Reader["Id_Usuario"].ToString()));
                Lista_Incidente.Add(Reader["Tipo_Solicitud"].ToString());
                Lista_Incidente.Add(Reader["Categoria_Incidente"].ToString());

                Lista_Incidente.Add(Reader["Prioridad"].ToString());
                Lista_Incidente.Add(Reader["Estado"].ToString());
            }
            Incidencia_Data Lista = new Incidencia_Data
            {
                Activos = Lista_Incidente


            };

            Reader.Close();
            return Lista;



        }



        public ArrayList ListOfPersonnel(MySql.Data.MySqlClient.MySqlConnection conn) {


            ArrayList Personnel = new ArrayList();
            string Query = "SELECT `Usuario`.`Nombre_Usuario` FROM `mydb`.`Usuario` WHERE Id_Rol = " + 17 + "; ";
            String SecondQuery = "SELECT COUNT(*) FROM Usuario WHERE Id_Rol = 17;";

            var Cmd = new MySql.Data.MySqlClient.MySqlCommand(SecondQuery, conn);
            var Reader = Cmd.ExecuteReader();
            Reader.Read();
            //get quantity of items
            int Count = Convert.ToInt32(Reader["COUNT(*)"].ToString());


            Reader.Close();

            Cmd = new MySql.Data.MySqlClient.MySqlCommand(Query, conn);
             Reader = Cmd.ExecuteReader();

            for (int i = 0; i < Count; i++)
            {
                Reader.Read();

                Personnel.Add(Reader["Nombre_Usuario"].ToString());
            }

            Reader.Close();
            return Personnel;

        }

        public String Cedula_ByID(MySql.Data.MySqlClient.MySqlConnection conn,String Code) {
          
            string Query = "SELECT Incidencia.Cedula_Solicitante FROM Incidencia WHERE Incidencia.Id_Incidencia = "+Code+"; ";

            var Cmd = new MySql.Data.MySqlClient.MySqlCommand(Query, conn);
            var Reader = Cmd.ExecuteReader();
            Reader.Read();
            //get quantity of items
            String Id_Usuario = Reader["Cedula_Solicitante"].ToString();

            Reader.Close();
            return Id_Usuario;



        }
        public void UpdateAssignment(MySql.Data.MySqlClient.MySqlConnection conn, String Code,String Personnel) {
            DateTime date = DateTime.Today;
            String today = date.ToString("d", CultureInfo.CreateSpecificCulture("ja-JP"));
        
 
            String DB_Change = "UPDATE `mydb`.`Incidencia` SET `Fecha_Atencion` = '"+ today+"' WHERE `Id_Incidencia` = "+Code+";";

            var Insert = new MySql.Data.MySqlClient.MySqlCommand(DB_Change, conn);//Insert comm
            var executer = Insert.ExecuteNonQuery();//execute non query 

            DB_Change = "UPDATE `mydb`.`Reportes` SET `Fecha_Asignacion` = '"+today+"' WHERE `Id_Reportes` = '"+ Cedula_ByID(conn,Code)  +"';";

            Insert = new MySql.Data.MySqlClient.MySqlCommand(DB_Change, conn);//Insert comm
             executer = Insert.ExecuteNonQuery();//execute non query 


        }















        //AWS connectiMySql.Data.MySqlClient.MySqlConnection connon
        public MySql.Data.MySqlClient.MySqlConnection Connection()
        {
            String MyConnectionString;
            //Connection String to connect aws rds DB instance
            MyConnectionString = "Server=helpdesk.cyeip6jtr6ck.us-east-1.rds.amazonaws.com;database=mydb;uid=HD_Master;" +
                    "pwd=wotilark123;port=3306;";
            // Open connection
            MySql.Data.MySqlClient.MySqlConnection conn;
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = MyConnectionString;

            return conn;
        }



    }
}