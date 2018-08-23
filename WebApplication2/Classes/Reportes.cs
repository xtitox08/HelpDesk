using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class Reportes
    {

        public void Insert_Report(MySql.Data.MySqlClient.MySqlConnection conn, String Cedula, String Date,  String About) {//Insert the report data on db table 'Reportes' as is 

            String DB_Insert;
            //Insert in db 
            DB_Insert = "INSERT INTO `mydb`.`Reportes` (`Id_Reportes`, `Cedula_Solicitante`,`Fecha_Registro`, `Fecha_Asignacion`,`Asunto`) VALUES(" +Cedula + "," + Convert.ToInt32(Cedula) + 1+ ", '" + Date+"','"+"-"+"', '"+About+"'); ";

            var Insert = new MySql.Data.MySqlClient.MySqlCommand(DB_Insert, conn);//Insert comm
            var executer = Insert.ExecuteNonQuery();//execute non query 
         

        }

        public Incidencia_Data ListarReportes(MySql.Data.MySqlClient.MySqlConnection conn)//Returns the instance containing the list of incidents currently held in DB table ListadoReportes
        {
            Incidentes Incidentes = new Incidentes();
            ArrayList Lista_Incidente = new ArrayList();
            string Query = "SELECT * FROM `mydb`.`Reportes`; ";
            string SecondQuery = "Select COUNT(*) FROM Reportes;";
            var Cmd = new MySql.Data.MySqlClient.MySqlCommand(SecondQuery, conn);
            var Reader = Cmd.ExecuteReader();
            Reader.Read();
            //get quantity of items
            int Quantity = Convert.ToInt32(Reader["COUNT(*)"].ToString());

            Reader.Close();


            Cmd = new MySql.Data.MySqlClient.MySqlCommand(Query, conn);
            Reader = Cmd.ExecuteReader();


            for (int i = 0; i < Quantity; i++)//loop in each row of table Reportes
            {

                      Reader.Read();
                Lista_Incidente.Add(Reader["Id_Reportes"].ToString());
                Lista_Incidente.Add(Reader["Fecha_Registro"].ToString());
                Lista_Incidente.Add(Reader["Fecha_Asignacion"].ToString());
                Lista_Incidente.Add(Reader["Cedula_Solicitante"].ToString());  
                Lista_Incidente.Add(Reader["Asunto"].ToString());

            }
            Incidencia_Data Lista = new Incidencia_Data
            {
                Activos = Lista_Incidente


            };

            Reader.Close();
            return Lista;//returns list of Reports



        }



    }
}