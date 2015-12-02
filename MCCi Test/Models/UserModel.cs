using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Routing;

namespace MCCi_Test.Models
{
    public class UserModel
    {
        [Required]
        [DisplayName("Prefix")]
        public string Prefix { get; set; }

        [Required]
        [UniqueID]
        [DisplayName("User ID")]
        public string UserID { get; set; }

        [Required]
        [DisplayName("Suffix")]
        public string Suffix { get; set; }

        public RouteValueDictionary RouteValues
        {
            get
            {
                var values = new RouteValueDictionary();
                values["Prefix"] = Prefix;
                values["UserID"] = UserID;
                values["Suffix"] = Suffix;
                return values;
            }
        }

    }

    //custom atribute class
    public class UniqueID : ValidationAttribute
    {
        //override validation method to check custom requirement
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var unique = true;

            foreach (var user in MCCi_Test.Controllers.DefaultController.users)
            {
                if (user.UserID == value.ToString())
                    unique = false;
            }

            if (!unique)
                return new ValidationResult("This user ID already exists.");

            return ValidationResult.Success;
        }
    }

}