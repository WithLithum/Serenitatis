namespace Serenitatis.Util;

public static class SysPaths
{
    public static string GetIdFromPath(string fullPath, string startPoint)
    {
        var relative = Path.GetRelativePath(startPoint, fullPath);

        var noExtension = Path.ChangeExtension(relative, null);
        return noExtension.Replace(Path.DirectorySeparatorChar, '/');
    }
}