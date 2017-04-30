using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{

    /// <summary>
    /// Ariel Sigo
    /// 
    /// Updated:
    /// 2017/04/29
    /// 
    /// Class of Supplier Catalog View
    /// </summary>
    public class SupplierCatalogViewModel
    {
        private bool _IsApproved;

        private string _FarmAddress;

        private string _FarmCity;

        private string _FarmState;

        private bool _Active;

        private User _user;


        public int SupplierID { get; set; }

        public bool IsApproved { get;  set; }

        public String IsApprovedString 
        { 
            get
            {
                return _IsApproved == true ?  "Approved" : "Not Approved";
            } 
        }

        public int UserId { get; set; }

        public int? ApprovedBy { get; set; }

        public User UserData
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }

        }

        public string ApprovedByName
        {
            get
            {
                return _user.FirstName + " " + _user.LastName;
            }
        }


        public String FarmTaxID { get; set; }


        public string FarmName { get; set; }

        public string FarmAddress
        {
            get
            {
                return _FarmAddress;
            }
            set
            {
                _FarmAddress = value;
            }
        }

        public string FarmAddressString
        {
            get
            {
                return _FarmAddress + "\n" + _FarmCity + ", " + _FarmState;
            }
        }

        public string FarmCity
        {
            get
            {
                return _FarmCity;
            }
            set
            {
                _FarmCity = value;
            }
        }

        public string FarmState
        {
            get
            {
                return _FarmState;
            }
            set
            {
                _FarmState = value;
            }
        }

        public bool Active
        {
            get
            {
                return _Active;
            }
            set
            {
                _Active = value;
            }
        }

        public string ActiveString
        {
            get
            {
                return _Active == true ? "Active" : "Inactive"; 
            }
        }
    }
}
