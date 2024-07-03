using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmartlyCodingExercise.Web.Common
{
    public class DropDownCollections
    {
        public static IList<SelectListItem> GetMonthsList()
        {
            return
            [
                new SelectListItem { Text = "January", Value = "1" },
                new SelectListItem { Text = "February", Value = "2" },
                new SelectListItem { Text = "March", Value = "3" },
                new SelectListItem { Text = "April", Value = "4" },
                new SelectListItem { Text = "May", Value = "5" },
                new SelectListItem { Text = "June", Value = "6" },
                new SelectListItem { Text = "July", Value = "7" },
                new SelectListItem { Text = "August", Value = "8" },
                new SelectListItem { Text = "September", Value = "9" },
                new SelectListItem { Text = "October", Value = "10" },
                new SelectListItem { Text = "November", Value = "11" },
                new SelectListItem { Text = "December", Value = "12" }
            ];
        }
    }
}
