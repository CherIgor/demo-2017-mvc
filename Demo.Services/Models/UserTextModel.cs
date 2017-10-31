using System;
using System.Linq.Expressions;
using Demo.Data.Models.Entity;

namespace Demo.Services.Models
{
    public class UserTextModel
    {
        public int UserTextId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Added { get; set; }

        public string AddedAsString
        {
            get { return Added.ToString(); }
        }

        public static Expression<Func<UserText, UserTextModel>> FromEntity
        {
            get
            {
                Expression<Func<UserText, UserTextModel>> expression = u => new UserTextModel
                {
                    UserTextId = u.UserTextId,
                    Name = u.Name,
                    Text = u.Text,
                    Added = u.Added
                };
                return expression;
            }
        }
    }
}
