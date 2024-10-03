namespace GameReview.Services.Exceptions;

public class ExternalAPIException(string message) : Exception(message)
{ }