using OurHr.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{

    public class LogEvent
    {
        [Key]
        public int LogId { get; set; }
        public DateTime LogDate { get; set; }
        public string LogSteme { get; set; }
        public string LogIp { get; set; }

    }

    public class RoleTable
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

    }
    public class MyUserTbl
    {
        [Key]
        public int Id { get; set;}
        public string Staffid { get; set; }
        public string PID { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int EmpId { get; set; }

        //[ForeignKey("EmpId")]
        //public virtual EmpTbl EmpTbl { get; set; }

        [ForeignKey("RoleId")]
        public virtual RoleTable RoleTable { get; set; }

        [Required]
        public int LogId { get; set; }

        [ForeignKey("LogId")]
        public virtual LogEvent LogEvent { get; set; }
    }

    public class NewUserTbl
    {
        [Key]
       public int NewUserId { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public int EmpId { get; set; }

        //[ForeignKey("EmpId")]
        //public virtual EmpTbl EmpTbl { get; set; }

    }

    public class UseAuth
    {
        [Key]
        public int AuthId { get; set; }
        public int EmpId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SystemName { get; set; }
        public string IpAddress { get; set; }
        public string LogTimeDate { get; set; }
    }

    public class Usercreate
    {
        [Key]
        public int CreatId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string  CreatedBy { get; set; }
        public string ActivationDate { get; set; }
        public string Supense { get; set; }
        public int EmpId { get; set; }
        public int EmpidCreator { get; set; }
    }


    public class NewUser
    {
        [Key]
        public int CreatId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; }
        public string ActivationDate { get; set; }
        public string Supense { get; set; }
        public int EmpId { get; set; }
        public int EmpidCreator { get; set; }
    }


    public class PassChange
    {
        [Key]
        public int PassChangeId { get; set; }
        public string Email { get; set; }
        public string oldPass { get; set; }
        public string NewPass { get; set; }
        public DateTime DateTime { get; set; }
        public string SystemName { get; set; }
        public string IpAddress { get; set; }
    }

    public class PassPolicy
    {

    }

   

      

    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        public string RoleName { get; set; }
        public int EmpId { get; set; }     
        //[ForeignKey("EmpId")]
        //public virtual EmpTbl EmpTbl { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual RoleTable RoleTable { get; set; }

    }

   
    
}
