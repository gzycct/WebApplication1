using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Describe
    {
        public int ID { get; set; }

        [Display(Name = "文件名称")]
        public string Name { get; set; }
        [Display(Name = "公共描述")]
        public string PublicDescribe { get; set; }


        [Display(Name = "公共描述大小(bytes)")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public long PublicScheduleSize { get; set; }

        [Display(Name = "后台描述")]
        public string PrivateDescribe { get; set; }


        [Display(Name = "后台描述大小 (bytes)")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public long PrivateScheduleSize { get; set; }


        [Display(Name = "上传时间(UTC)")]
        [DisplayFormat(DataFormatString = "{0:F}")]
        public DateTime UploadDateTime { get; set; }
    }
}
