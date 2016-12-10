using System.ComponentModel;

namespace WebCore.Enums
{
    public enum AlertType
    {
        [Description("alert-danger")]
        Danger,
        [Description("alert-info")]
        Info,
        [Description("alert-warning")]
        Warning,
        [Description("alert-success")]
        Success
    }
}
