using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TFPTest.Models.ViewModel
{
    public class ViewModel
    {
        public List<CountryList> countryLists { get; set; }
        public UserAccount userAccount { get; set; }

        public RegistrationClass registrationClass { get; set; }
        //public MessageClass messageClass { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageFile { get; set; }
        public List<UserAccount> UserAccountList { get; set; }
        public IEnumerable<SelectListItem> StatusSelectListItems
        {
            get { return new SelectList(countryLists, "CountryName", "CountryName"); }
        }
        public ViewModel()
        {
            UserAccountList = new List<UserAccount>();
;       }  

    }
}