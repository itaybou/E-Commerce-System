using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    public abstract class Notification
    {
        protected DateTime _time;

        protected Notification()
        {
            this._time = DateTime.Now;
        }

        public abstract string getMessage();
    }
}
