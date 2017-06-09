using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

/// <summary>
/// Summary description for ServiceValidation
/// </summary>
public class ServiceValidation : SoapHeader
{
    #region Properties

    private string wsToken;

    public string WSToken
    {
        get { return wsToken; }
        set { wsToken = value; }
    }

    #endregion

    #region Constructor

    public ServiceValidation()
    {
       
    }

    //public ServiceValidation(string token)
    //{
    //    this.wsToken = token;
    //}

    #endregion
}
