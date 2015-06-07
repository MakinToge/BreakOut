// ***********************************************************************
// Assembly         : BreakOut
// ***********************************************************************
// <copyright file="Chrono.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// The BreakOut namespace.
/// </summary>
namespace BreakOut {
    /// <summary>
    /// Class Chrono.
    /// </summary>
    public class Chrono {
        /// <summary>
        /// Gets or sets the total seconds.
        /// </summary>
        /// <value>The total seconds.</value>
        public int TotalSeconds { get; set; }
        /// <summary>
        /// The milliseconds
        /// </summary>
        private int milliseconds;

        /// <summary>
        /// Gets or sets the milliseconds.
        /// </summary>
        /// <value>The milliseconds.</value>
        public int Milliseconds {
            get { return milliseconds; }
            set {
                milliseconds = value;
                if (milliseconds >= 1000) {
                    this.Seconds += 1;
                    milliseconds -= 1000;
                }

            }
        }
        /// <summary>
        /// The seconds
        /// </summary>
        private int seconds;

        /// <summary>
        /// Gets or sets the seconds.
        /// </summary>
        /// <value>The seconds.</value>
        public int Seconds {
            get { return seconds; }
            set {
                seconds = value;
                this.TotalSeconds += 1;
                if (seconds >= 60) {
                    this.Minutes += 1;
                    seconds -= 60;
                }
            }
        }
        /// <summary>
        /// The minutes
        /// </summary>
        private int minutes;
        /// <summary>
        /// Gets or sets the minutes.
        /// </summary>
        /// <value>The minutes.</value>
        public int Minutes {
            get { return minutes; }
            set {
                minutes = value;
                if (minutes >= 60) {
                    this.Hours += 1;
                    minutes -= 60;
                }
            }
        }
        /// <summary>
        /// Gets or sets the hours.
        /// </summary>
        /// <value>The hours.</value>
        public int Hours { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chrono"/> class.
        /// </summary>
        public Chrono() {
            this.Milliseconds = 0;
            this.Seconds = 0;
            this.Minutes = 0;
            this.Hours = 0;
            this.TotalSeconds = 0;
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() {
            string s, m;
            if (this.Seconds < 10) {
                s = string.Format("0{0}", this.Seconds);
            }
            else { s = this.Seconds.ToString(); }
            if (this.Minutes < 10) {
                m = string.Format("0{0}", this.Minutes);
            }
            else { m = this.Minutes.ToString(); }
            return string.Format("{0}:{1}", m, s);
        }
    }
}
