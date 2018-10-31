using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class FileUpload
    {
        [Required]
        [Display(Name = "文件名")]
        [StringLength(60, MinimumLength = 3)]
        public string FileName { get; set; }

        [Required]
        [Display(Name = "公共描述")]
        public IFormFile UploadPublicDescribe { get; set; }

        [Required]
        [Display(Name = "后台描述")]
        public IFormFile UploadPrivateDescribe { get; set; }
    }
}
