using System.Collections.Generic;
using System.Web.UI;

namespace Neverer.UtilityClass
{
    public class HtmlTag
    {
        public HtmlTextWriterTag TagName { get; set; }
        public HtmlTag InnerTag { get; set; }
        public string InnerText { get; set; }
        public Dictionary<HtmlTextWriterAttribute, string> Attributes { get; set; }
        public Dictionary<HtmlTextWriterStyle, string> StyleAttributes { get; set; }

        private void init()
        {
            this.Attributes = new Dictionary<HtmlTextWriterAttribute, string>();
            this.StyleAttributes = new Dictionary<HtmlTextWriterStyle, string>();
        }

        public HtmlTag(HtmlTextWriterTag tag)
        {
            init();
            this.TagName = tag;
        }

        public HtmlTag(HtmlTextWriterTag tag, HtmlTag inner,
            Dictionary<HtmlTextWriterAttribute, string> attributes = null,
            Dictionary<HtmlTextWriterStyle, string> styles = null)
        {
            init();
            this.TagName = tag;
            this.InnerTag = inner;
            if (attributes != null) { this.Attributes = attributes; }
            if (styles != null) { this.StyleAttributes = styles; }
        }

        public HtmlTag(HtmlTextWriterTag tag, string inner,
            Dictionary<HtmlTextWriterAttribute, string> attributes = null,
            Dictionary<HtmlTextWriterStyle, string> styles = null)
        {
            init();
            this.TagName = tag;
            this.InnerText = inner;
            if (attributes != null) { this.Attributes = attributes; }
            if (styles != null) { this.StyleAttributes = styles; }
        }

    }
}
