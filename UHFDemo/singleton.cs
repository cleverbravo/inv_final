using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UHFDemo
{
    public class singleton
    {
        private static singleton instance = null;
        private singleton() { }
        public static singleton Instance
        {
            get {
                if (instance == null) {
                    instance = new singleton();
                }
                return instance;
            }

        }
    }
}
