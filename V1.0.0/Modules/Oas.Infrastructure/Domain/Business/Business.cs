 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    
    public class Business
    {
        [Key]
        public Guid Id { get; set; }

        public string UserId { get; set; }
        public Guid BusinessCategoryId { get; set; }
        public bool UpdatedDate { get; set; }
        public string Zipcode { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Information { get; set; }

        public string SortDescription { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public Status Status { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }


        [ForeignKey("BusinessCategoryId")]
        public BusinessCategory BusinessCategory { get; set; }


        public double? Latitude { get; set; }
        public double? Longtitude { get; set; }

        public bool Active { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; }

        public long TotalViewed { get; set; }


        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<BusinessComment> Comments { get; set; }


        [NotMapped]
        public string Category
        {
            get
            {
                if (BusinessCategory != null)
                    return BusinessCategory.Name;
                return String.Empty;
            }
        }

        [NotMapped]
        public string BusinessOwner
        {
            get
            {
                if (User != null)
                    return User.UserName;
                return String.Empty;
            }
        }


        [NotMapped]
        public string GooglePlaceIcon
        {
            get
            {
                if (BusinessCategory != null)
                    return BusinessCategory.GooglePlaceIconUrl;
                return string.Format("http://www.google.com/intl/en_us/mapfiles/ms/icons/red-dot.png");
            }
        }

        [NotMapped]
        public string ImgProfile
        {
            get
            {
                if (Images != null && Images.Count > 0)
                {
                    var biz = Images.FirstOrDefault(t => t.IsProfileImage);
                    if (biz != null)
                        return string.Format("{0}", Images.FirstOrDefault(t => t.IsProfileImage).Url);
                    else
                        return string.Format("/Upload/no-img.jpg");
                }
                return string.Format("/Upload/no-img.jpg");
            }
        }

        [NotMapped]
        public int TotalComments
        {
            get
            {
                if (Comments != null)
                {
                    return Comments.Count;
                }
                return 0;
            }
        }
    }
}