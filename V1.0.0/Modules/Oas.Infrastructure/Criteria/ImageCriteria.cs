using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class ImageCriteria : Criteria
    {


public Guid? Id {get;set;}


public Guid? BusinessId {get;set;}


public Guid? CarId {get;set;}


public Guid? CarItemId {get;set;}


public string Caption {get;set;}


public string Url {get;set;}


public bool? IsProfileImage {get;set;}


public Guid? BookingNoteId {get;set;}


public Guid? CarAccidentNoteId {get;set;}
    }
}
