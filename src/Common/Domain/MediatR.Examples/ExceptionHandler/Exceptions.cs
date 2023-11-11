// ReSharper disable once CheckNamespace
namespace MediatR.Examples.ExceptionHandler;

#pragma warning disable MA0048 // File name must match type name
#pragma warning disable RCS1194
public class ConnectionException : Exception;

public class ForbiddenException : ConnectionException;

public class ResourceNotFoundException : ConnectionException;

public class ServerException : Exception;
#pragma warning restore RCS1194
