using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HtmxSandbox.TagHelpers;

/// <summary>
///     Adds .is-valid or .is-invalid class to the input/select/textarea element according to its validation state. This
///     helps to display the new Bootstrap 5 validation hints.
/// </summary>
[HtmlTargetElement("input", Attributes = ValidationAttributeName)]
[HtmlTargetElement("select", Attributes = ValidationAttributeName)]
[HtmlTargetElement("textarea", Attributes = ValidationAttributeName)]
public class ValidationTagHelper : TagHelper
{
    private const string ValidationAttributeName = "asp-validation";

    [HtmlAttributeNotBound] [ViewContext] public required ViewContext ViewContext { get; set; }

    [HtmlAttributeName(ValidationAttributeName)]
    public required ModelExpression For { get; set; }

    public override int Order => 1000; // Run after InputTagHelper, SelectTagHelper and TextAreaTagHelper.

    /// <summary>
    ///     Adds .is-valid or .is-invalid class to the input/select/textarea element according to its validation state. This
    ///     helps to display the new Bootstrap 5 validation hints.
    /// </summary>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        // Initial form display: ViewContext.ViewData.ModelState is empty. → Display no validation hints at all.
        if (ViewContext.ViewData.ModelState.TryGetValue(For.Name, out var entry))
        {
            output.AddClass(entry.Errors.Any() ? "is-invalid" : "is-valid", HtmlEncoder.Default);
        }

        output.Attributes.RemoveAll(ValidationAttributeName);
    }
}