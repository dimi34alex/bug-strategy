using TMPro;

namespace BugStrategy.UI.Elements
{
    public class TMP_NumberInputField : TMP_InputField
    {
        protected override bool IsValidChar(char c) 
            => char.IsNumber(c);
    }
}