using System;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorStore.Services.CustomTagHelpers
{
    
    public class PaginTagHelper<T>:TagHelper
	{
      
        [HtmlAttributeName("end-point")]
        public string EndPoint { get; set; }

        //[HtmlAttributeName("pagin-page")]
        //public PagePagination<T> Page { get; set; }
       

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            

            for (int i = 0; i < 3; i++)
            {
                output.TagName = "a";
                output.TagMode = TagMode.StartTagAndEndTag;
                output.Attributes.SetAttribute("href", $"{EndPoint}/{i}");
                output.Attributes.SetAttribute("class", "btn btn-success");
                output.Content.SetContent($"{i}");

            }
        }
    }
}

