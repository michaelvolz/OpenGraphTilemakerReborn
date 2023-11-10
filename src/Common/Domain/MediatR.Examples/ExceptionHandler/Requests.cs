// ReSharper disable once CheckNamespace
namespace MediatR.Examples.ExceptionHandler;

#pragma warning disable MA0048 // File name must match type name
public class PingResource : Ping;

public class PingNewResource : Ping;

public class PingResourceTimeout : PingResource;

public class PingProtectedResource : PingResource;