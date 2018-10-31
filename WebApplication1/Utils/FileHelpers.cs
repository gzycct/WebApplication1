using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Utils
{
    public class FileHelpers
    {
        public static async Task<string> ProcessFormFile(IFormFile formFile, ModelStateDictionary modelState)
        {
            var fieldDisplayName = string.Empty;

            // 使用反射获得的IFormFile实例对象的文件名称。
            // 如果名称没有找到，将会有一个简单的错误消息，但不会显示文件名称
            MemberInfo property = typeof(FileUpload).GetProperty(formFile.Name.Substring(formFile.Name.IndexOf(".") + 1));

            if (property != null)
            {
                var displayAttribute = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;

                if (displayAttribute != null)
                {
                    fieldDisplayName = $"{displayAttribute.Name} ";
                }
            }

            //使用path.GetFileName获取一个带路径的全文件名。
            //通过HtmlEncode进行编码的结果必须在错误消息中返回。 
            var fileName = WebUtility.HtmlEncode(Path.GetFileName(formFile.FileName));

            if (formFile.ContentType.ToLower() != "text/plain")
            {
                modelState.AddModelError(formFile.Name, $"The {fieldDisplayName}file ({fileName}) must be a text file.");
            }

            //校验文件长度，如果文件不包含内容，则不必读取文件长度。
            //此校验不会检查仅具有BOM（字节顺序标记）作为内容的文件，
            //因此在读取文件内容后再次检验文件内容长度，以校验仅包含BOM的文件。
            if (formFile.Length == 0)
            {
                modelState.AddModelError(formFile.Name, $"The {fieldDisplayName}file ({fileName}) is empty.");
            }
            else if (formFile.Length > 1048576)
            {
                modelState.AddModelError(formFile.Name, $"The {fieldDisplayName}file ({fileName}) exceeds 1 MB.");
            }
            else
            {
                try
                {
                    string fileContents;

                    //使用StreamReader按UTF-8编码读取文件。
                    //如果上传文件是采用其他的编码。
                    //请使用32位编码，将UTF8Encoding改为UTF32Encoding
                    using (var reader = new StreamReader(formFile.OpenReadStream(), new UTF32Encoding(), detectEncodingFromByteOrderMarks: true))
                    {
                        fileContents = await reader.ReadToEndAsync();

                        // 检查文件长度，如果文件的唯一内容是BOM，在删除BOM后内容实际上是空的。
                        if (fileContents.Length > 0)
                        {
                            return fileContents;
                        }
                        else
                        {
                            modelState.AddModelError(formFile.Name, $"The {fieldDisplayName}file ({fileName}) is empty.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    modelState.AddModelError(formFile.Name, $"The {fieldDisplayName}file ({fileName}) upload failed. " + $"Please contact the Help Desk for support. Error: {ex.Message}");
                    // Log the exception
                }
            }

            return string.Empty;
        }
    }
}