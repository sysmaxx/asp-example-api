using Microsoft.AspNetCore.Mvc;

namespace Service.Tests.Extensions;
public static class ActionResultExtension
{
    public static T GetObjectResultContent<T>(ActionResult<T> result)
    {
        if (result.Result != null)
            return (T)((ObjectResult)result.Result!).Value!;

        return result.Value!;
    }
}
