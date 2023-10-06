using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorStore.Services.CustomTagHelpers
{
	public class SwiisTagHelper:TagHelper
	{
        public string EndPoint { get; set; }

        public int Size { get; set; }

        public int Quantity { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            if(Quantity > 0)
            {
                Quantity = 0;
            }
            else
            {
                Quantity = 1;
            }
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < Size; i++)
            {
                //output.TagName = "a";
                //output.TagMode = TagMode.StartTagAndEndTag;
                //output.Attributes.SetAttribute("href", $"{EndPoint}/{i}");
                //output.Attributes.SetAttribute("class", "btn btn-success");
                stringBuilder.Append($"""<a href="{Quantity}/{i+1}" class="btn btn-success">{i+1}</a>""");

            }
           
            output.Content.SetHtmlContent(stringBuilder.ToString());
            output.TagMode = TagMode.StartTagAndEndTag;


        }
    }
}

