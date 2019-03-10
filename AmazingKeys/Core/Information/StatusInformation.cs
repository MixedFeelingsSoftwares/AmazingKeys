using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingKeys.Core.Information
{
    public class StatusInformation
    {
        #region Public Classes

        public static status Status { get; set; } = new status();

        public class status
        {
            #region Public Properties

            private int _errors;

            private int _success;

            private int _total;

            public event System.EventHandler onTotalChanged;

            public int Errors
            {
                get
                {
                    return _errors;
                }

                set
                {
                    if (_errors < value)
                    {
                        _errors = value;
                        Total++;
                    }
                }
            }

            public int Success
            {
                get
                {
                    return _success;
                }

                set
                {
                    if (_success < value)
                    {
                        _success = value;
                        Total++;
                    }
                }
            }

            public int Total
            {
                get
                {
                    return _total;
                }

                set
                {
                    if (_total != value)
                    {
                        _total = value;
                        if (onTotalChanged != null) { onTotalChanged(this, EventArgs.Empty); }
                    }
                }
            }

            public List<string> Urls { get; set; } = new List<string>();

            public MainWindow window { get; set; }

            #endregion Public Properties
        }

        #endregion Public Classes
    }
}