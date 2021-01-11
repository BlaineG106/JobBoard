using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSWDFinalProject.DATA.EF//.Metadata
{
    #region Positions Metadata
    public class PositionsMetadata
    {
        //public int PositionId { get; set; }
        [Required(ErrorMessage = "* Title is Required *")]
        [StringLength(50, ErrorMessage = "* Title must be 50 characters or less *")]
        public string Title { get; set; }

        [Display(Name = "Job Description")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string JobDescription { get; set; }
    }

    [MetadataType(typeof(PositionsMetadata))]
    public partial class Position { }
    #endregion

    #region Locations Metadata
    public class LocationsMetadata
    {
        [Required(ErrorMessage = "* Store Number is Required *")]
        [Display(Name = "Store Number")]
        [StringLength(15, ErrorMessage = "* Store Number must be 15 characters or less *")]
        public string StoreNumber { get; set; }

        [Required(ErrorMessage = "* City is Required *")]
        [StringLength(50, ErrorMessage = "* City must be 50 characters or less *")]
        public string City { get; set; }

        [Required(ErrorMessage = "* State is Required *")]
        [StringLength(2, ErrorMessage ="* State must be 2 characters or less *")]
        public string State { get; set; }

        [Required(ErrorMessage = "* Manager ID is Required *")]
        [StringLength(128, ErrorMessage = "* Manager ID must be 128 characters or less *")]
        [Display(Name = "Manager ID")]
        public string ManagerId { get; set; }
    }

    [MetadataType(typeof(LocationsMetadata))]
    public partial class Location { }
    #endregion

    #region UserDetails Metadata
    public class UserDetailsMetadata
    {
        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "* First Name is Required *")]
        [StringLength(50, ErrorMessage = "* First Name must be 50 characters or less *")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Last Name is Required *")]
        [StringLength(50, ErrorMessage = "* Last Name must be 50 characters or less *")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Resume")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string ResumeFilename { get; set; }
    }

    [MetadataType(typeof(UserDetailsMetadata))]
    public partial class UserDetail
    {
        [Display(Name = "Full Name")]
        public string FullName
        {
          get { return FirstName + " " + LastName; }
        }
    }
    #endregion

    #region OpenPositions Metadata
    public class OpenPositionsMetadata
    {
        [Required(ErrorMessage = "* Position ID is Required *")]
        [Range(0, int.MaxValue, ErrorMessage = "* Position ID must be a positive number *")]
        [Display(Name = "Position ID")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "* Location ID is Required *")]
        [Range(0, int.MaxValue, ErrorMessage = "* Location ID must be a positive number *")]
        [Display(Name = "Location ID")]
        public int LocationId { get; set; }
    }

    [MetadataType(typeof(OpenPositionsMetadata))]
    public partial class OpenPosition { }
    #endregion

    #region Applications Metadata
    public class ApplicationsMetadata
    {
        [Required(ErrorMessage = "* Open Position ID is Required *")]
        [Display(Name = "Open Position ID")]
        [Range(0, int.MaxValue, ErrorMessage = "* Open Position ID must be a positive number *")]
        public int OpenPositionId { get; set; }

        [Required(ErrorMessage = "* User ID is Required *")]
        [Display(Name = "User ID")]
        [StringLength(128, ErrorMessage = "* User ID must be 128 characters or less *")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "* Application Date is Required *")]
        [Display(Name = "Application Date")]        
        public System.DateTime ApplicationDate { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        [Display(Name = "Manager Notes")]
        public string ManagerNotes { get; set; }

        [Required(ErrorMessage = "* Application Status is Required *")]
        [Display(Name = "Application Status")]
        [Range(0, int.MaxValue, ErrorMessage = "* Application Status must be a positive number *")]
        public int ApplicationStatus { get; set; }

        [Required(ErrorMessage = "* Resume is Required *")]
        [Display(Name = "Resume")]
        [StringLength(75, ErrorMessage = "* Resume must be 75 characters or less *")]
        public string ResumeFilename { get; set; }
    }

    [MetadataType(typeof(ApplicationsMetadata))]
    public partial class Application { }
    #endregion

    #region ApplicationStatuses Metadata
    public class ApplicationStatusesMetadata
    {
        [Required(ErrorMessage = "* Status Name is Required *")]
        [Display(Name = "Status Name")]
        [StringLength(50, ErrorMessage = "* Status Name must be 50 characters or less *")]
        public string StatusName { get; set; }

        [Required(ErrorMessage = "* Status Description is Required *")]
        [Display(Name = "Status Description")]
        [StringLength(250, ErrorMessage = "* Status Description must be 250 characters or less *")]
        public string StatusDescription { get; set; }
    }

    [MetadataType(typeof(ApplicationStatusesMetadata))]
    public partial class ApplicationStatus { }
    #endregion
}
