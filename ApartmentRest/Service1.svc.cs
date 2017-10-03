using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ApartmentRest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public IList<Apartment> GetAllApartment()
        {
            const string sqlstring = "SELECT * from dbo.Apartment order by id";

            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(sqlstring, sqlConnection))
                {
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                      List<Apartment> liste = new List<Apartment>();
                        while (reader.Read())
                        {
                            var _Apartment = ReadApartment(reader);
                            liste.Add(_Apartment);
                        }
                        return liste;
                    }
                }
            }
            
            
        }

        public IList<Apartment> GetApartmentByPostalCode(string PostalCode)
        {

             string sqlstring = $"Select * from dbo.Apartment where postalcode = {PostalCode}";

            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (var sqlcommand = new SqlCommand(sqlstring, sqlConnection))
                {
                    using (var reader = sqlcommand.ExecuteReader())
                    {
                        List<Apartment> liste = new List<Apartment>();
                        while (reader.Read())
                        {
                            Apartment _Apparment = ReadApartment(reader);
                            liste.Add(_Apparment);
                        }
                        return liste;
                    }
                }
            }

        }


        //public IList<Apartment> GetAllApartmentByLocation(string location)
        //{
        //    string sqlstring = $"Select * from dbo.apartment like location = {location}";

        //    using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
        //    {
        //        sqlConnection.Open();
        //        using (var sqlcommand = new SqlCommand(sqlstring, sqlConnection))
        //        {
        //            using (var reader = sqlcommand.ExecuteReader())
        //            {
        //                List<Apartment> liste = new List<Apartment>();
        //                while (reader.Read())
        //                {
        //                    Apartment _apartment = ReadApartment(reader);
        //                    liste.Add(_apartment);
        //                }
        //                return liste;
        //            }
        //        }
        //    }
        //}

        //vores læse metode vi kan bruge sammen med reader - den sætter bare læste værdier ind på nyt obj
        private static Apartment ReadApartment(IDataRecord reader)
        {
            var Id = reader.GetInt32(0);
            var Price = reader.GetInt32(1);
            var Location = reader.GetString(2);
            var PostalCode = reader.GetInt32(3);
            var Size = reader.GetInt32(4);
            var NoRoom = reader.GetInt32(5);
            var WashingMachine = reader.GetBoolean(6);
            var Dishwasher = reader.GetBoolean(7);


            var i = new Apartment {Id = Id, Price = Price, Location = Location, PostalCode = PostalCode, Size = Size, NoRoom = NoRoom, WashingMachine = WashingMachine, Dishwasher = Dishwasher};

            return i;
        }
        //string til at hente vores connection string
        private static string GetConnectionString()
        {
            var connectionStringSettingsCollection = ConfigurationManager.ConnectionStrings;
            var connStringSettings = connectionStringSettingsCollection["MikDatabaseAzure"];
            return connStringSettings.ConnectionString;
        }


    }
}
