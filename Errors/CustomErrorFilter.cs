public class CustomErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        // Customize error message or logging here
        Console.WriteLine(error.Exception?.ToString());
        return error.WithMessage("An unexpected error occurred. Please try again later.");
    }
}
