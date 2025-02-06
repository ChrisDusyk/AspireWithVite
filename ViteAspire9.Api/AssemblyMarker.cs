using System.Reflection;

namespace ViteAspire9.Api;

internal static class AssemblyMarker
{
	public static Assembly Assembly => typeof(AssemblyMarker).Assembly;
}