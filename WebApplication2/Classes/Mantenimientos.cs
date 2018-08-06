using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class Mantenimientos
    {

        public Mantenimientos_Data Id_Activo (MySql.Data.MySqlClient.MySqlConnection conn ) {
            string query = "SELECT `Activo`.`Id_Activo` FROM `mydb`.`Activo` ORDER BY Id_Activo DESC LIMIT 1; ";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            var reader = cmd.ExecuteReader();

            reader.Read();
            
            String res = (Convert.ToInt32((reader["Id_Activo"].ToString()))+1).ToString();
            // obtiene primer valor

            Mantenimientos_Data Instance = new Mantenimientos_Data
            {

                ID = res

            };

            return Instance;

         
        }
        public Mantenimientos_Data Id_Sede (MySql.Data.MySqlClient.MySqlConnection conn) {
            string query = "SELECT `Sede`.`Id_Sede` FROM `mydb`.`Sede` ORDER BY Id_Sede DESC LIMIT 1; ";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            var reader = cmd.ExecuteReader();

            reader.Read();

            String res = (Convert.ToInt32((reader["Id_Sede"].ToString())) + 1).ToString();
            // obtiene primer valor

            Mantenimientos_Data Instance = new Mantenimientos_Data
            {

                ID = res

            };

            return Instance;

        }

   

        public void Sede_Data(MySql.Data.MySqlClient.MySqlConnection conn, String ID, String Name, String Place)
        {
            String DB_Insert;
            //Insert in db 
            DB_Insert = "INSERT INTO `mydb`.`Sede` (`Id_Sede`, `Nombre`, `Ciudad`) VALUES ( " + Convert.ToInt32(ID) + ",'" + Name + "','" + Place + "'"+ ");";

            var Insert = new MySql.Data.MySqlClient.MySqlCommand(DB_Insert, conn);//Insert comm
            var executer = Insert.ExecuteNonQuery();//execute non query 



        }












        public void Activo_Data(MySql.Data.MySqlClient.MySqlConnection conn ,String ID, String Name, String Place,String Quantity) {
      
            String Sede_ID;//variables for queries and insert
            string Query = ""; 
            String DB_Insert;

         
                    Query = "SELECT `Sede`.`Id_Sede` FROM `mydb`.`Sede` WHERE Ciudad = '"+ Place+"'; ";//get id by place
  
      
            //ID by place
            var ID_CMD = new MySql.Data.MySqlClient.MySqlCommand(Query, conn);//ejecuta query 
            var Read = ID_CMD.ExecuteReader();

            Read.Read();

          Sede_ID = Read["Id_Sede"].ToString();//ID SEDE
            Read.Close();


 



            //Insert in db 
            DB_Insert = "INSERT INTO `mydb`.`Activo` (`Id_Activo`, `Id_Sede_Activo`, `Nombre`, `Cantidad`) VALUES ( " + Convert.ToInt32(ID) + "," + Convert.ToInt32(Sede_ID) + ",'" + Name + "'," +Convert.ToInt32(Quantity) +  ");";

            var Insert = new MySql.Data.MySqlClient.MySqlCommand(DB_Insert, conn);//Insert comm
            var executer = Insert.ExecuteNonQuery();//execute non query 



        }




    }
}