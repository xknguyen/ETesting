using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using TwitterBootstrapMVC;
using WebCore.Enums;
using WebCore.Extensions;

namespace WebCore.UI
{
    public class BeautyAlert :IHtmlString
    {
        private readonly string _alertMessage;
        private bool _closable;
        private Icon _icon;
        private readonly IDictionary<string, object> _htmlAttribute;


        public BeautyAlert(string message)
        {
            _closable = false;
            _alertMessage = message;
           _htmlAttribute = new Dictionary<string, object>();
            _htmlAttribute.AddOrMergeCssClass("class", "alert");
        }

        public BeautyAlert Closable()
        {
            _closable = true;
            return this;
        }

        public BeautyAlert Data(object htmlDataAttributes)
        {
            _htmlAttribute.MergeHtmlAttributes(htmlDataAttributes.ToHtmlDataAttributes());
            return this;
        }

        public BeautyAlert HtmlAltribute(IDictionary<string,object> htmlAttribute )
        {
            _htmlAttribute.MergeHtmlAttributes(htmlAttribute);
            return this;
        }

        public BeautyAlert HtmlAltribute(object htmlAttribute)
        {

            _htmlAttribute.MergeHtmlAttributes(htmlAttribute.ToDictionary());
            return this;
        }

        public BeautyAlert Style(AlertType alertType)
        {
            Class(alertType.GetEnumDescription());
            return this;
        }

        public BeautyAlert Icon(Icon icon)
        {
            _icon = icon;
            return this;
        }

        public BeautyAlert Icon(string iconClass)
        {

            return Icon(new Icon(iconClass));
        }

        public BeautyAlert Id(string id)
        {
            _htmlAttribute.AddOrReplace("id", id);
            return this;
        }

        public BeautyAlert IsLongMessage()
        {
            _htmlAttribute.AddOrMergeCssClass("class", "alert-block");
            return this;
        }

        public BeautyAlert Class(string cssClass)
        {
            _htmlAttribute.AddOrMergeCssClass("class", cssClass);
            return this;
        }

        public string ToHtmlString()
        {
            var htmlAttributes = _htmlAttribute.FormatHtmlAttributes();
            var tag = new TagBuilder("div");
            tag.MergeAttributes(htmlAttributes);

            const string buttonTag = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>";

            tag.InnerHtml = string.Format("{0}{1}", _closable ? buttonTag : "", _alertMessage);


            if (_icon != null)
                tag.PrependHtml(_icon);

            return tag.ToString(TagRenderMode.Normal);
        }

        
    }
}
