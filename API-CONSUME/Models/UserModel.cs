using System.ComponentModel.DataAnnotations;

namespace API_Consume.Models
{
    public class UserModel
    {
        public int User_ID { get; set; }


        public string User_code { get; set; }
   
        public string User_name { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LocationName { get; set; }
      
        public int ServiceID { get; set; }
        public int SLAID { get; set; }
     
        public int UserTypeID { get; set; }
        
        public bool Active { get; set; }
        public int Createdby { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Modifiedby { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string ImageName { get; set; }
    }
}
