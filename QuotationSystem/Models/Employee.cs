using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuotationSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="姓名")]
        public string Name { get; set; }

        [Required]
        [Display(Name="账号")]
        [MaxLength(20)]
        public string Account { get; set; }

        [Required]
        public Department Department { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20)]
        [Display(Name ="密码")]
        public string Password { get; set; }

        [Display(Name ="注册时间")]
        public DateTime RegisterTime { get; set; }

        [Display(Name ="最后登录时间")]
        public DateTime LastLogTime { get; set; }
    }

    public class EmployeeRegViewModel
    {
        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name="所属部门")]
        [Required]
        public List<SelectListItem> Departments { get; set; }

        [Required]
        [Display(Name = "账号")]
        [MaxLength(20)]
        [RegularExpression(@"^\w+",ErrorMessage ="账号只能是英文字母和数字组成")]
        public string Account { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }

    public class EmployeeLoginViewModel
    {
        [Required]
        [Display(Name = "账号")]
        [MaxLength(20)]
        public string Account { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }
}