using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public class clsPerson
    {
        public enum enMode { AddNew=0,UpdateMode=1};
        public enMode Mode = enMode.AddNew;
        public enum enGendor { Male=0 , Female=1};
        public int PersonID {  get; set; }
       public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string FullName { get {  return FirstName + " "+ LastName; } }
    public string Email { get; set; }
        public string Phone { get; set; }
        public string Address {  get; set; }
        public string NationalNo { set; get; }
        public DateTime DateOfBith {  get; set; }
        public short Gendor {  get; set; }
        public int NationalityCountryID {  get; set; }
        public clsCountry CountryInfo;
        private string _ImagePath;

        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBith = DateTime.Now;
            this.Gendor = 0;
            this.NationalNo = "";
            this.NationalityCountryID = -1;
            this.Email = "";
            this.ImagePath = "";
            this.Address = "";
            Mode = enMode.AddNew;
            

        }
public clsPerson(int PersonID, string FirstName, string SecondName, 
    string ThirdName,
            string LastName, string NationalNo, DateTime DateOfBirth, short Gendor,
             string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.ThirdName = ThirdName;
            this.SecondName = SecondName;
            this.Email= Email;
            this.ImagePath = ImagePath;
            Mode = enMode.UpdateMode;
            this.NationalNo= NationalNo;
            this.DateOfBith = DateOfBirth;
            this.ImagePath = ImagePath;
            this.CountryInfo = clsCountry.Find(NationalityCountryID);
            this.Phone= Phone;
            this.NationalityCountryID = NationalityCountryID;
            this.Gendor = Gendor;
            this.Address = Address;
        }
        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(
               this.FirstName, this.SecondName,this.ThirdName,
                this.LastName , this.NationalNo , this.DateOfBith , this.Gendor,
         
                this.Address , this.Phone,this.Email , this.NationalityCountryID , this.ImagePath  );
         
            return PersonID != -1;
        }
        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(
                this.PersonID, this.FirstName, this.SecondName,
                this.ThirdName,
                this.LastName, this.NationalNo, this.DateOfBith, this.Gendor,

                this.Address, this.Phone, this.Email,
                this.NationalityCountryID, this.ImagePath
                )
                ;

        }

        public static clsPerson Find (int PersonID)
        {
            string FirstName="", SecondName="", ThirdName="", 
                LastName ="", NationalNo = "", Phone = "", Email = "",
                Address ="", ImagePath = "";
            short Gendor = 0; int NationalityCountryID = -1;
            DateTime DateOfBith = DateTime.Now;
            bool isFound = clsPersonData.GetPersonInfoByID(
                 PersonID, ref FirstName, ref SecondName,
               ref ThirdName,
               ref LastName, ref NationalNo,ref DateOfBith, 
              ref Gendor,

                ref Address, ref Phone, ref Email,
               ref NationalityCountryID, ref ImagePath
                );
            if (isFound)
            {
                return new clsPerson(PersonID, FirstName, SecondName, 
                    ThirdName,
                    LastName, NationalNo, DateOfBith, Gendor, Address, 
                    Phone, Email,
                     NationalityCountryID,ImagePath);

            }
            else return null;

        }

        public static clsPerson Find(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "",
            
                LastName = "",
                Address = "", Phone = "", Email = "", ImagePath = "";
            short Gendor = 0; int NationalityCountryID = -1;
            int PersonID = -1;
          DateTime DateOfBith = DateTime.Now;
            bool isFound = clsPersonData.GetPersonInfoByNationalNo
                (
              NationalNo, ref  PersonID, ref FirstName, ref SecondName,
               ref ThirdName,
               ref LastName, ref DateOfBith,
              ref Gendor,

                ref Address, ref Phone, ref Email,
               ref NationalityCountryID, ref ImagePath
                );
            if (isFound)
            {
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName,
                    LastName, NationalNo, DateOfBith, Gendor, Address,
                    Phone, Email,
                     NationalityCountryID, ImagePath);

            }
            else return null;

        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode= enMode.UpdateMode; 
                        return true;
                    }
                    else { return  false; }
                    case enMode.UpdateMode:
                    return _UpdatePerson();
            }
            return false;
        }
        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }
        public static bool DeletePerson(int ID)
        {
            return clsPersonData.DeletePerson(ID);
        }
        public static bool isPersonExist(int ID)
        {
            return clsPersonData.IsPersonExist(ID);
        }
        public static bool isPersonExist(string NationlNo)
        {
            return clsPersonData.IsPersonExist(NationlNo);
        }

    }
}
