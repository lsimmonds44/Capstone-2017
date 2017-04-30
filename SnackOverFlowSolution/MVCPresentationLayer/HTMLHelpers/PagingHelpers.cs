using MVCPresentationLayer.Models;
using System;
using System.Text;
using System.Web.Mvc;

namespace MVCPresentationLayer.HtmlHelpers
{
    public static class PagingHelpers
    {
        /// <summary>
        /// Skyler Hiscock
        /// Referencing Adam Freeman (book: Pro ASP.NET MVC 5)
        /// 
        /// Created:
        /// 2017/04/29
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pagingInfo"></param>
        /// <param name="pageUrl"></param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("custom-button");
                }
                tag.AddCssClass("button-style2");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}