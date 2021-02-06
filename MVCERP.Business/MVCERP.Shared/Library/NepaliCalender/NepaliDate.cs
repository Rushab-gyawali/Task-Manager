using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Library.NepaliCalender
{
    /// <summary>
    /// NepaliDate - data object class
    /// </summary>
    public class NepaliDate
    {
        private string _nepaliDate;

        /// <summary>
        /// String representation of Nepali Date. Format yyyy/m/d
        /// </summary>
        public string npDate
        {
            get { return _nepaliDate; }
            set { _nepaliDate = value; }
        }

        private int _npDaysInMonth;

        /// <summary>
        /// DaysInMonth of Nepali date
        /// </summary>
        public int npDaysInMonth
        {
            get { return _npDaysInMonth; }
            set { _npDaysInMonth = value; }
        }

        private int _npYear;

        /// <summary>
        /// Numeric Year of Nepali date
        /// </summary>
        public int npYear
        {
            get { return _npYear; }
            set { _npYear = value; }
        }

        private string _npMonth;

        /// <summary>
        /// Numeric Month of Nepali date
        /// </summary>
        public string npMonth
        {
            get { return _npMonth; }
            set { _npMonth = value; }
        }

        private string _npDay;

        /// <summary>
        /// Numeric Day of Nepali date
        /// </summary>
        public string npDay
        {
            get { return _npDay; }
            set { _npDay = value; }
        }
    }
}
