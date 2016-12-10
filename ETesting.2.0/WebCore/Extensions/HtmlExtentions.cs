using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using WebCore.Models;
using WebCore.UI;

namespace WebCore.Extensions
{
    public static class HtmlExtentions
    {
        public static MvcHtmlString Piture(this HtmlHelper htmlHelper, string source, string altText)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            source = urlHelper.Content(source);

            const string format = "<img src=\"{0}\" alt=\"{1}\" />";
            string imgTag = string.Format(format, source, altText);

            return MvcHtmlString.Create(imgTag);
        }


        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string source, string altText, string name = null,
            object htmlAtribute = null)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var tag = new TagBuilder("img");
            tag.MergeAttribute("src", urlHelper.Content(source));

            tag.MergeAttribute("alt", altText);

            if (!string.IsNullOrEmpty(name))
            {
                tag.MergeAttribute("name", name);
            }

            if (htmlAtribute != null)
            {
                tag.MergeAttributes(new RouteValueDictionary(htmlAtribute));
            }

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.SelfClosing));

        }

        public static MvcHtmlString ImageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string altText,
            object htmlAtribute = null)
        {
            var function = expression.Compile();
            var source = function(htmlHelper.ViewData.Model);
            var name = ExpressionHelper.GetExpressionText(expression);
            return Image(htmlHelper, source.ToString(), altText, name, htmlAtribute);

        }



        public static MvcHtmlString UploadFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.TextBoxFor(expression, new { type = "file" });
        }

        public static MvcHtmlString THead(this HtmlHelper htmlHelper, List<Column> columns, int sortIndex, int sortStatus)
        {
            var trTag = new TagBuilder("tr");

            for (var i = 0; i < columns.Count; i++)
            {
                var column = columns[i];
                var tag = new TagBuilder("th") { InnerHtml = column.ColumnName + " " };

                if (column.IsHiddenSm)
                {
                    tag.AddCssClass("hidden-sm hidden-xs");
                }

                // Lấy columnName
                if (column.Width != 0)
                {
                    tag.MergeAttribute("width", column.Width + "px");
                }

                // Set sort
                if (column.IsSortColumn)
                {

                    var columnIndex = i + 1;
                    var spanTag = new TagBuilder("span");
                    if (columnIndex == sortIndex)
                    {
                        var aTag = new TagBuilder("a");
                        switch (sortStatus)
                        {
                            case -1:
                                aTag.MergeAttribute("class", "up-order");
                                aTag.MergeAttribute("data-sort", columnIndex + "");
                                aTag.InnerHtml = "<i class=\"fa fa-sort-asc\"></i>";
                                break;
                            case 1:
                                aTag.MergeAttribute("class", "down-order");
                                aTag.MergeAttribute("data-sort", columnIndex + "");
                                aTag.InnerHtml = "<i class=\"fa fa-sort-desc\"></i>";
                                break;
                        }

                        spanTag.InnerHtml += aTag.ToString();
                    }
                    else
                    {
                        var aTag = new TagBuilder("a");
                        aTag.MergeAttribute("class", "up-order");
                        aTag.MergeAttribute("data-sort", columnIndex + "");
                        aTag.InnerHtml = "<i class=\"fa fa-sort-asc\"></i>";

                        spanTag.InnerHtml += aTag.ToString();

                        aTag = new TagBuilder("a");
                        aTag.MergeAttribute("class", "down-order");
                        aTag.MergeAttribute("data-sort", columnIndex + "");
                        aTag.InnerHtml = "<i class=\"fa fa-sort-desc\"></i>";
                        spanTag.InnerHtml += aTag.ToString();
                    }

                    tag.InnerHtml += spanTag.ToString();
                }
                trTag.InnerHtml += tag.ToString();
            }
            return MvcHtmlString.Create(trTag.ToString());
        }

        public static BeautyAlert Alert(this HtmlHelper htmlHelper, string message)
        {
            return new BeautyAlert(message);
        }

    }
}
