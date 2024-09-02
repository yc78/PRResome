using Resume.DAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.DAL.Models.AboutMe
{
    public class AboutMe : BaseEntity<int>
    {
        #region Properties

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Mobile { get; set; }

        public string? Position { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string? Location { get; set; }

        public string Bio { get; set; }

        public string ImageName { get; set; }
        
        #endregion
    }
}
