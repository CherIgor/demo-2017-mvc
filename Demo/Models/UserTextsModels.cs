using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Demo.Services.Models;
using Microsoft.Ajax.Utilities;

namespace Demo.Models
{
    public class UserTextsViewModel
    {
        public IEnumerable<UserTextModel> OldUserTexts;

        public UserTextViewModel NewUserText;
    }

    public class UserTextViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Text")]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [AllowHtml]
        public string Text { get; set; }

        public UserTextModel ViewModelToModel()
        {
            return new UserTextModel
            {
                Name = Name,
                Text = Text
            };
        }
    }
}
