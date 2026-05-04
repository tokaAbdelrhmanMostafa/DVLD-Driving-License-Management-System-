using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
namespace Buisness
{
    public class clsCountry
    {
        public string CountryName {  get; set; }
        public int CountryID {  get; set; }
        clsCountry() { this.CountryName = "";this.CountryID = -1; }
        clsCountry(int CountryID , string CountryName) { this.CountryName = CountryName;
            this.CountryID = CountryID;
        }
        public static clsCountry Find(int CountryID) {
            string CountryName = "";
            if (DataAccess.clsCountryData.GetCountryInfoByID(CountryID, ref CountryName))
            {
                return  new clsCountry(CountryID, CountryName);
            }
            else { return null; }



        }
        public static  clsCountry Find(string countryName )
        {
            int Id = -1;
            if (DataAccess.clsCountryData.GetCountryInfoByName( countryName,ref Id))
            {
                return new clsCountry(Id, countryName);
            }
            else { return null; }



        }

        public static DataTable GetAllCountries() { return clsCountryData.GetAllCountries(); }
    }
}
