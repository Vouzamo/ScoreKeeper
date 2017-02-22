using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Vouzamo.ScoreKeeper.Web.TagHelpers
{
    [HtmlTargetElement("div", Attributes = ValidationForAttributeName)]
    public class ValidationClassTagHelper : TagHelper
    {
        private const string ValidationForAttributeName = "asp-validation-for";
        private const string ValidationSuccessAttributeName = "asp-validation-success";
        private const string ValidationWarningAttributeName = "asp-validation-warning";
        private const string ValidationErrorAttributeName = "asp-validation-error";

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(ValidationForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(ValidationSuccessAttributeName)]
        public string Success { get; set; }

        [HtmlAttributeName(ValidationWarningAttributeName)]
        public string Warning { get; set; }

        [HtmlAttributeName(ValidationErrorAttributeName)]
        public string Error { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (For != null)
            {
                var tagBuilder = new TagBuilder(output.TagName);
                var state = ViewContext.ModelState.GetFieldValidationState(For.Name);

                if (state == ModelValidationState.Valid && !string.IsNullOrEmpty(Success))
                {
                    tagBuilder.AddCssClass(Success);
                }

                if ((state == ModelValidationState.Skipped || state == ModelValidationState.Unvalidated) && !string.IsNullOrEmpty(Warning))
                {
                    tagBuilder.AddCssClass(Warning);
                }

                if (state == ModelValidationState.Invalid && !string.IsNullOrEmpty(Error))
                {
                    tagBuilder.AddCssClass(Error);
                }

                output.MergeAttributes(tagBuilder);
            }
        }
    }
}
