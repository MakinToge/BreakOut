using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut {
    public class Chrono {
        public int TotalSeconds { get; set; }
        private int milliseconds;

        public int Milliseconds {
            get { return milliseconds; }
            set {
                milliseconds = value;
                if (milliseconds >= 1000) {
                    this.Seconds += 1;
                    milliseconds -=1000;
                }
                
            }
        }
        private int seconds;
        
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
        private int minutes;
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
        public int Hours { get; set; }

        public Chrono() {
            this.Milliseconds = 0;
            this.Seconds = 0;
            this.Minutes = 0;
            this.Hours = 0;
            this.TotalSeconds = 0;
        }
        public override string ToString() {
            string s, m;
            if (this.Seconds < 10) {
                s = string.Format("0{0}",this.Seconds);
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
