using System.Reflection;

namespace ViteAspire9.Application;

public static class AssemblyMarker
{
	public static Assembly Assembly => typeof(AssemblyMarker).Assembly;
}
