using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models.User
{
    public class UserStatisticsModel
    {
        [Required(ErrorMessage = "Missing lower limit date to present statistics for.")]
        public DateTime From { get; set; }

        [Required(ErrorMessage = "Missing upper limit date to present statistics for.")]
        public DateTime To { get; set; }

        public UserStatisticsModel()
        {
            From = DateTime.Now.Date;
            To = DateTime.Now.Date;
        }
    }
}
