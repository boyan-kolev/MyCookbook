namespace MyCookbook.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class AttributesErrorMessages
    {
        public const string RequiredErrorMessage = "Полето \"{0}\" е задължително!";

        public const string StringLengthMessage = "Полето \"{0}\" трябва да бъде с дължина между {2} и {1} символа!";

        public const string MinLengthErrorMessage = "Полето \"{0}\" трябва да има поне {1} символа!";

        public const string MaxLengthErrorMessage = "Броят на допустимите символи за полето \"{0}\" е {1}!";

        public const string RangeErrorMessage = "Стойноста на полето \"{0}\" трябва да бъде между {1} и {2}!";
    }
}
