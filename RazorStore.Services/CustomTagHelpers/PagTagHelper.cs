using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorStore.Services.CustomTagHelpers
{
    public class PagTagHelper<T> : TagHelper
    {
        public string EndPoint { get; set; }

        public PagePagination<T> Page { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {


            for (int i = 0; i < Page.TotalPage; i++)
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

