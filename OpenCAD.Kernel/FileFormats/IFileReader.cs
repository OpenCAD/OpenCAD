namespace OpenCAD.Kernel.FileFormats
{
    public interface IFileReader<T>:IFileReader where T:IFileFormat
    {
        T Read(string path);
    }
    public interface IFileReader
    {
         
    }
}