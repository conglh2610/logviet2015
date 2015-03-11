using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class SettingCriteria : Criteria
    {


public Guid? Id {get;set;}


public string DefaultGLng {get;set;}


public string DefaultGLa {get;set;}


public float? DefaultRadius {get;set;}


public bool? IsEnableChat {get;set;}


public int? DefaultZoom {get;set;}


public bool? AllowLocationTracking {get;set;}
    }
}
