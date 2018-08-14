using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Controllers
{
    public class Usuarios
    {

        string SomeValue;
        public string User_Data(MySql.Data.MySqlClient.MySqlConnection conn)
        {

            string query = "SELECT `Usuario`.`Id_Usuario`+1 FROM `mydb`.`Usuario`ORDER BY Id_Usuario DESC LIMIT 1;";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            var reader = cmd.ExecuteReader();

            reader.Read();

            SomeValue = reader["`Usuario`.`Id_Usuario`+1"].ToString();
            // obtiene primer valor
            reader.Close();

            return SomeValue;

        }


        public void User_Data(MySql.Data.MySqlClient.MySqlConnection conn, int ID, String email, String UserName, String pass, String sede, String area, String department, String estado)
        {


            String User_Role;//variables de rol y query
            String User_HQ;
            string Query = ""; string SecondQuery = "";
            String DB_Insert;

            switch (area)
            {
                case "Docente":
                    Query = "SELECT `Rol_Usuario`.`Id_Rol`FROM `mydb`.`Rol_Usuario` WHERE Tipo_Rol = 'Docente'; ";
                    break;//asignacion de query dependiendo del rol 
                case "Area Administrativa":
                    Query = "SELECT `Rol_Usuario`.`Id_Rol`FROM `mydb`.`Rol_Usuario` WHERE Tipo_Rol = 'Area Administrativa'; ";
                    break;
                case "Area Técnica":
                    Query = "SELECT `Rol_Usuario`.`Id_Rol`FROM `mydb`.`Rol_Usuario` WHERE Tipo_Rol = 'Area Tecnica'; ";
                    break;
                case "Gerencia":
                    Query = "SELECT `Rol_Usuario`.`Id_Rol`FROM `mydb`.`Rol_Usuario` WHERE Tipo_Rol = 'Gerencia'; ";
                    break;

            }
            switch (sede)
            {
                case "Sede Heredia":
                    SecondQuery = "SELECT `Sede`.`Id_Sede` FROM `mydb`.`Sede` WHERE Ciudad = 'Heredia'; ";
                    break;//asignacion de query dependiendo del rol 
                case "Sede Puntarenas":
                    SecondQuery = "SELECT `Sede`.`Id_Sede` FROM `mydb`.`Sede` WHERE Ciudad = 'Puntarenas';  ";
                    break;
                case "Sede Aranjuez":
                    SecondQuery = "SELECT `Sede`.`Id_Sede` FROM `mydb`.`Sede` WHERE Ciudad = 'Aranjuez'; ";
                    break;
                case "Sede Llorente":
                    SecondQuery = "SELECT `Sede`.`Id_Sede` FROM `mydb`.`Sede` WHERE Ciudad = 'Llorente'; ";
                    break;

            }
            //ID por sede
            var ID_CMD = new MySql.Data.MySqlClient.MySqlCommand(SecondQuery, conn);//ejecuta query 
            var Read = ID_CMD.ExecuteReader();

            Read.Read();

            User_HQ = Read["Id_Sede"].ToString();//ID SEDE
            Read.Close();


            // Rol por Tipo de usuario
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(Query, conn);//ejecuta query 
            var reader = cmd.ExecuteReader();

            reader.Read();

            User_Role = reader["Id_Rol"].ToString();//ID USUARIO
            reader.Close();



            //Inserta en la base de datos
            DB_Insert = "INSERT INTO `mydb`.`Usuario` (`Id_Usuario`,`Id_Sede_Usuario`,`Id_Rol`,`Nombre_Usuario`,`Correo_Electronico`,`Contraseña`,`Estado`,`Nivel_Atencion`,`Departmento`)" +
            "VALUES(" + ID + "," + Convert.ToInt32(User_HQ) + "," + Convert.ToInt32(User_Role) + ",'" + UserName + "','" + email + "','" + pass + "','" + estado + "','" + "Media" + "','" + department + "');";

            var Insert = new MySql.Data.MySqlClient.MySqlCommand(DB_Insert, conn);//Insert comm
            var executer = Insert.ExecuteNonQuery();//execute non query 






        }

        public bool Login_Verification(MySql.Data.MySqlClient.MySqlConnection conn,String UserName,String Password) {
            string query = "SELECT `Usuario`.`Contraseña` FROM `mydb`.`Usuario` where Correo_Electronico = '"+UserName+"';";
            
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            var reader = cmd.ExecuteReader();

            reader.Read();

            SomeValue = reader["Contraseña"].ToString();
            // obtiene primer valor
            reader.Close();
            
            if (!Password.Equals(SomeValue)) {
                return false;
            }
          
            return true;
        }
      
    }
}
