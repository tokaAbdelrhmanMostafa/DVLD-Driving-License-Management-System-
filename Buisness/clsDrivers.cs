using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public class clsDrivers
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public clsPerson PersonInfo;
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { set; get; }
        public DateTime CreatedDate { get; }

        public clsDrivers()
        {
            PersonInfo = new clsPerson();
            this.DriverID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;
        }
        public clsDrivers
            (int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)

        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.PersonInfo = clsPerson.Find(PersonID);
            Mode = enMode.AddNew;

        }
        private bool _AddNewDriver()
        {
            return clsDriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
        }
        public static clsDrivers FindByDriverID(int DriverID)
        {
            int PersonID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;

            if (clsDriverData.GetDriverInfoByDriverID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))

                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;

        }

        public static clsDrivers FindByPersonID(int PersonID)
        {
            int DriverID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.GetDriverInfoByPersonID
                 (PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))

                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
            
            

        }
        private bool _UpdateDriver()
        {
            //call DataAccess Layer 

            return clsDriverData.UpdateDriver
                (this.DriverID, this.PersonID, this.CreatedByUserID);

        }

        public static DataTable GetAllDrivers() { return clsDriverData.GetAllDrivers(); }
 
    public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        Mode = enMode.AddNew;
                        return true;
                    }
                    else {  return false;}
                case enMode.Update:
                    return _UpdateDriver();

            }
            return false;
        }

        //public static DataTable GetLicenses(int DriverID)
        //{
        //    return clsLicense.GetDriverLicenses(DriverID);
        //}


    }
}
