using System;
using System.Threading;
using JollazApiQueries.Library.Models.Errors;

public class ResourceManagerUtils
{
    public static IErrorMessages ErrorMessages
    {
        get
        {
            var ns = "JollazApiQueries.Library.Models.Errors";
            var errMsg = Type.GetType($"{ns}.ErrorMessages_{Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName}");
            errMsg = errMsg ?? Type.GetType($"{ns}.ErrorMessages_eng");
            return (IErrorMessages)Activator.CreateInstance(errMsg);
        }
    }
}