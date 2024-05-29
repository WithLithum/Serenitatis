using Serenitatis.Util;

namespace Serenitatis.Tests;

public class PathTests
{
    [SkippableFact(typeof(PlatformNotSupportedException))]
    public void PathToId_Windows()
    {
        if (!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException();
        
        // Arrange
        const string path = @"C:\projects\my-project\characters\test\mike.json";
        const string root = @"C:\projects\my-project\characters\";
        
        // Act
        var result = SysPaths.GetIdFromPath(path, root);
        
        // Assert
        Assert.Equal("test/mike", result);
    }
    
    [SkippableFact(typeof(PlatformNotSupportedException))]
    public void PathToId_Linux()
    {
        if (!OperatingSystem.IsLinux())
            throw new PlatformNotSupportedException();
        
        // Arrange
        const string path = "/home/test/projects/my-project/characters/test/mike.json";
        const string root = "/home/test/projects/my-project/characters/";
        
        // Act
        var result = SysPaths.GetIdFromPath(path, root);
        
        // Assert
        Assert.Equal("test/mike", result);
    }
}