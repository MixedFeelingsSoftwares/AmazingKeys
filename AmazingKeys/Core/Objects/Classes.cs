using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AmazingKeys.Core.Objects
{
    public class Classes
    {
        #region Public Classes

        public class RequestCallback
        {
            #region Public Properties

            public WebRequest result { get; set; }

            public string URL { get; set; }

            public WebProxy usedProxy { get; set; }

            #endregion Public Properties
        }

        #endregion Public Classes
    }
}