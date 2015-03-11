using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain.ViewModel
{
    public class BusinessCategoryGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public List<BusinessCategoryGroup> Children { get; set; }
    }

    public class BusinessCommentViewModel
    {
        public BusinessComment Comment
        {
            get
        ;
            set
            ;
        }

        public User User
        {
            get
                ;
            set;
        }
    }

    public class ImageSlideShowViewModel {
        public string PathXML { get; set; }
        public List<Image> ListImages { get; set; }
    }

    public class JsonParseViewModel {        
        public int Key { get; set; }
        public string Value { get; set; }
    }
    public class FactualCategory {         
        public int  Id { get; set; }
        public FactualValueCategory[] labels { get; set; }
        public int[] parents { get; set; }
        public bool isabstract { get; set; }
    }

    public class FactualValueCategory {
       
        public string EN { get; set; }
        public string KR { get; set; }
        public string ZH_HANT { get; set; }
        public string ZH { get; set; }
        public string JP { get; set; }
        public string PT { get; set; }
        public string DE { get; set; }
        public string IT { get; set; }
        public string ES { get; set; }
        public string FR { get; set; }
       
    }

    public class FactualDownloadParameter
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Guid CategoryL1 { get; set; }
        public Guid CategoryL2 { get; set; }
        public Guid CategoryL3 { get; set; }
        public Guid CategoryL4 { get; set; }

        public List<BusinessCategoryViewModel> Category_ZipInfo { get; set; }
    }

    public class BusinessCategoryViewModel
    {
        public Guid BusinessCategoryId { get; set; }
        public string Zipcode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int  TotalDownloaded { get; set; }
        public int Availble { get; set; }
        public string URL { get; set; }
        public bool IsChoose { get; set; }
        public string CategoryInclude { get; set; }
        public Guid Category_ZipId { get; set; }
        
    }

    public class DownloadViewModel
    {
        public string State { get; set; }
        public string City { get; set; }
        public List<string> Zipcodes { get; set; }
        public string CategoryInclude { get; set; }
        public int TotalDownloaded { get; set; }
        public int Availble { get; set; }
    }
}
