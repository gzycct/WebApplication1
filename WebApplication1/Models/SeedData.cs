using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication1.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookContext(serviceProvider.GetRequiredService<DbContextOptions<BookContext>>()))
            {
                // Look for any Books.
                if (context.Book.Any())
                {
                    return;   // DB has been seeded
                }

                context.Book.AddRange(
                    new Book
                    {
                        Name = "Python编程 从入门到实践",
                        ReleaseDate = DateTime.Parse("2018-1-12"),
                        Author = "埃里克·马瑟斯",
                        Price = 75.99M,
                        Publishing = "机械出版社"
                    },

                    new Book
                    {
                        Name = "Java编程的逻辑",
                        ReleaseDate = DateTime.Parse("2018-1-13"),
                        Author = "马俊昌",
                        Price = 48.99M,
                        Publishing = "机械出版社"
                    },

                    new Book
                    {
                        Name = "统计思维:大数据时代瞬间洞察因果的关键技能",
                        ReleaseDate = DateTime.Parse("2017-12-23"),
                        Author = "西内启",
                        Price = 39.99M,
                        Publishing = "机械出版社"
                    },

                    new Book
                    {
                        Name = "微信营销",
                        ReleaseDate = DateTime.Parse("2018-01-05"),
                        Author = "徐林海",
                        Price = 33.99M,
                        Publishing = "机械出版社"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
