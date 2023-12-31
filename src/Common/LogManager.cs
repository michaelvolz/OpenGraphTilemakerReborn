﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using Serilog;

namespace Common;

public static class LogManager
{
	/// <summary>
	///     Gets a logger for the current class. Ensure this is set to a static field on the class.
	/// </summary>
	/// <returns>An instance of <see cref="Serilog.ILogger" /></returns>
	[MethodImpl(MethodImplOptions.NoInlining)]
	public static ILogger GetCurrentClassLogger() =>
#pragma warning disable MA0003
		Log.ForContext(new StackFrame(1, false).GetMethod()?.DeclaringType ?? throw new InvalidOperationException());
}