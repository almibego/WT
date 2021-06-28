using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace W1.TagHelpers
{
    [HtmlTargetElement("pager", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PagerTagHelper : TagHelper
    {
        LinkGenerator _linkGenerator;

        [HtmlAttributeName("page-current")]
        public int PageCurrent { get; set; }
        [HtmlAttributeName("page-total")]
        public int PageTotal { get; set; }
        [HtmlAttributeName("pager-class")]
        public string PagerClass { get; set; }
        [HtmlAttributeName("action")]
        public string Action { get; set; }
        [HtmlAttributeName("controller")]
        public string Controller { get; set; }
        [HtmlAttributeName("group-id")]
        public int? GroupId { get; set; }

        public PagerTagHelper(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";

            //output.TagMode = TagMode.StartTagAndEndTag;

            var ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("pagination");
            ulTag.AddCssClass(PagerClass);

            for (int i = 1; i <= PageTotal; i++)
            {
                var url = _linkGenerator.GetPathByAction(Action, Controller,
                    new
                    {
                        pageNo = i,
                        group = GroupId == 0
                                 ? null
                                 : GroupId
                    });

                var item = GetPagerItem(
                    url: url, text: i.ToString(),
                    active: i == PageCurrent,
                    disabled: i == PageCurrent);

                ulTag.InnerHtml.AppendHtml(item);
            }
            output.Content.AppendHtml(ulTag);
        }

        private TagBuilder GetPagerItem(string url, string text, bool active = false, bool disabled = false)
        {
            var liTag = new TagBuilder("li");
            liTag.AddCssClass("page-item");
            liTag.AddCssClass(active ? "active" : "");
            //liTag.AddCssClass(disabled ? "disabled" : "");

            var aTag = new TagBuilder("a");
            aTag.AddCssClass("page-link");
            aTag.Attributes.Add("href", url);
            aTag.InnerHtml.Append(text);

            liTag.InnerHtml.AppendHtml(aTag);

            return liTag;
        }
    }
}