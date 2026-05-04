using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Buisness.clsLicense;

namespace Buisness
{
public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enIssueReason
        {
            FirstTime = 1, Renew = 2,
            DamagedReplacement = 3, LostReplacement = 4
        };

        public clsDrivers DriverInfo;
       
       
        public int LicenseID { set; get; }
        public int ApplicationID { set; get; }
        public int DriverID { set; get; }
        public int LicenseClass { set; get; }
        public clsLicenseClass LicenseClassIfo;
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public string Notes { set; get; }
        public float PaidFees { set; get; }
        public bool IsActive { set; get; }
        public enIssueReason IssueReason { set; get; }
        //public string IssueReasonText
        //{
        //    get
        //    {
        //        return GetIssueReasonText(this.IssueReason);
        //    }
        //}
        //public clsDetainedLicense DetainedInfo { set; get; }
        public int CreatedByUserID { set; get; }
        //public bool IsDetained
        //{
        //    get { return clsDetainedLicense.IsLicenseDetained(this.LicenseID); }
        //}
        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }
        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }

    }
}
