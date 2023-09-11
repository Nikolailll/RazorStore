using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorStore.Services.CustomTagHelpers
{
	[HtmlTargetElement(Attributes = "if")]
	public class IfTagHelper : TagHelper
	{
		[HtmlAttributeName("if")]	
		public bool RenderContent { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if(RenderContent == false)
            {
                output.TagName = null;
                output.SuppressOutput();
            }
        }

        public override int Order => int.MinValue;
    }
}

